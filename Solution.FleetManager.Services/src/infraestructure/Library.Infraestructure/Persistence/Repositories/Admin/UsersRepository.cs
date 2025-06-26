using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Create;
using Library.Infraestructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Update;
using Microsoft.IdentityModel.Tokens;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Read;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Persistence.Repositories.Admin
{
    public class UsersRepository
    {
        private readonly DataBaseContext _context;
        public UsersRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericHandlerResponse<List<UserReadDTO>>> Get(PaginationDTO paginationDTO)
        {
            var data = new List<UserReadDTO>();
            try
            {
                var query = _context.AuthUsers.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(paginationDTO.SearchValue))
                {
                    string[] searchValues = paginationDTO.SearchValue.Split(" ");
                    foreach (var value in searchValues)
                    {
                        query = query.Where(a =>
                            a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                            //a.Dni.Equals(value) || a.Dni.Contains(value) ||
                            a.FirstName.Equals(value) || a.FirstName.Contains(value) ||
                            a.LastName.Equals(value) || a.LastName.Contains(value) ||
                            a.Email.Equals(value) || a.Email.Contains(value) ||
                            a.PhoneNumber.Equals(value) || a.PhoneNumber.Contains(value));
                    }
                }

                var totalRecords = await query.CountAsync();
                data = await query
                    .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                    .Take(paginationDTO.RecordsPerPage)
                    .OrderBy(c => c.FirstName)
                    .Select(c => new UserReadDTO
                    {
                        Id = c.Id,
                        UserName = c.UserName,
                        Email = c.Email,
                        //Dni = c.Dni,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        ProfilePicture = c.ProfilePicture,
                        PhoneNumber = c.PhoneNumber,
                        BirthDate = c.BirthDate, //se cambio el tipo de dato
                        //Gender = c.Gender,
                        CreatedBy = c.CreatedBy,
                        CreatedDate = c.CreatedDate,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedDate = c.ModifiedDate,
                        IsActive = c.IsActive,
                    }).ToListAsync();

                // Retorna 200 OK con la lista paginada
                return new GenericHandlerResponse<List<UserReadDTO>>(200, data: data, totalRecords);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<List<UserReadDTO>>(500, ExceptionMessage: ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<UserReadFirstDTO>> GetById(long Id)
        {
            var user = new UserReadFirstDTO();
            try
            {
                #region 1. Obtener la informacion del usuario
                user = await _context.AuthUsers
                    .Where(u => u.Id == Id)
                    .Select(user => new UserReadFirstDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        //Dni = user.Dni,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePicture = user.ProfilePicture,
                        PhoneNumber = user.PhoneNumber,
                        DateOfBirth = user.BirthDate,
                        //Gender = user.Gender,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate,
                        ModifiedBy = user.ModifiedBy,
                        ModifiedDate = user.ModifiedDate,
                        IsActive = user.IsActive,
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                    return new GenericHandlerResponse<UserReadFirstDTO>(200, data: user, CustomMessage: "No se encontró información del usuario solicitado.");
                #endregion

                #region 2. Obtener los roles del usuario
                var userRoles = await _context.AuthUserRoles
                   .Where(r => r.UserId == Id)
                   .Select(r => r.RoleId)
                   .ToListAsync();

                if (userRoles.Count > 0)
                {
                    var roles = await _context.AuthRoles
                    .Where(role => userRoles.Any(ur => ur == role.Id))
                    .Select(role => new RolesReadDTO
                    {
                        Id = role.Id,
                        Description = role.Description,
                        CreatedBy = role.CreatedBy,
                        CreatedDate = role.CreatedDate,
                        ModifiedBy = role.ModifiedBy,
                        ModifiedDate = role.ModifiedDate
                    })
                    .ToListAsync();

                    user.Roles = roles;
                }
                #endregion

                // Retorna 200 OK con la lista paginada
                return new GenericHandlerResponse<UserReadFirstDTO>(200, data: user);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<UserReadFirstDTO>(500, ExceptionMessage: ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<long>> Create(UsersCreateDTO payload, long usuId)
        {
            try
            {
                //if (await _context.AuthUsers.AnyAsync(x => x.Dni == payload.Dni))
                //    return new GenericHandlerResponse<long>(409, CustomMessage: $"El NDI: {payload.Dni} ya existe en el sistema.");
                if (await _context.AuthUsers.AnyAsync(x => x.UserName == payload.UserName))
                    return new GenericHandlerResponse<long>(409, CustomMessage: $"El nombre de usuario: {payload.UserName} ya existe en el sistema.");
                if (await _context.AuthUsers.AnyAsync(x => x.UserName == payload.UserName))
                    return new GenericHandlerResponse<long>(409, CustomMessage: $"El correo electrónico: {payload.Email} ya existe en el sistema.");

                var mediaUrl = "";
                DateTime currentDate = DateTime.UtcNow;

                #region 3. Subir imagen de perfil
                if (!payload.ProfilePicture.IsNullOrEmpty())
                {
                    var picture = BaseHelper.GetFileType(payload.ProfilePicture ?? "");
                    var folderPath = Path.Combine("media", "files", "profiles");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = $"profileImage-{payload.UserName}.{picture.Extension}";
                    var filePath = Path.Combine(folderPath, fileName);

                    await File.WriteAllBytesAsync(filePath, picture.File);

                    var domain = BaseHelper.GetEnvVariable("CDN_HOST");
                    mediaUrl = $"{domain}/files/profiles/{fileName}";
                }
                #endregion

                #region 4. Guardar registro en la base de datos
                var model = new AuthUser
                {
                    UserName = payload.UserName,
                    Email = payload.Email,
                    //Dni = payload.Dni,
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    ProfilePicture = mediaUrl.IsNullOrEmpty()?null: mediaUrl,
                    PhoneNumber = payload.PhoneNumber,
                    BirthDate = payload.BirthDate.HasValue ? payload.BirthDate.Value : null,
                    //Gender = payload.Gender,
                    Password = PasswordHelper.HashPassword(payload.Password),
                    ResetPassword = false,
                    CreatedBy = usuId,
                    CreatedDate = currentDate,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true,
                };
                await _context.AuthUsers.AddAsync(model);
                await _context.SaveChangesAsync();
                #endregion

                #region 5. Registrar roles
                var userRoles = payload.Roles?.Select(roleId => new AuthUserRole
                {
                    UserId = model.Id,
                    RoleId = roleId,
                    CreatedBy = payload.CreatedBy,
                    CreatedDate = currentDate,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true
                }).ToList();

                if (userRoles != null)
                    await _context.AuthUserRoles.AddRangeAsync(userRoles);
                await _context.SaveChangesAsync();
                #endregion

                #region 6. Enviar correo de confirmacion al usuario
                var confirmationEmailUserTemplate = @$"
                            <html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>
                            <head>
                            <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
                            <meta charset='latin-1'>
                            <meta http-equiv='X-UA-Compatible' content='Chrome'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <meta name='description'>
                            <title>CoreExpress</title>
                            <style>
                                .link-button {{
                                    display: block;
                                    width: 200px;
                                    height: 40px;
                                    text-align: center;
                                    text-decoration: none;
                                    background: #002b6f;
                                    color: #ffffff !important;
                                    font-family: Arial;
                                    padding: 10px 5px;
                                    box-sizing: border-box;
                                    margin: auto;
                                    border: none;
                                    font-size: 16px;
                                }}
                            </style>
                            </head>
                            <body style='margin: 0; padding: 0; background: #f8f8f8;'>
                                <div style=""width: 500px; height: 600px; background: transparent; margin: auto; margin-top: 10px;"">
                                    <table style=""width: 100%; border-collapse: collapse;"">
                                        <thead style=""background: #002b6f; width: 100%;"">
                                            <tr>
                                                <th style=""text-align: center;"">
                                                    <img src=""https://res.cloudinary.com/doi9kcogv/image/upload/v1730520705/Branding/frhm1qygazhgcv9ifwxe.png"" width=""120"" alt="""">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style=""width: 100%; height: 400px; background: #fff;"">
                                                    <table style=""width: 100%; padding: 10px;"">
                                                        <tbody>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style=""height: 60px; text-align: center; margin-top: 10px"">
                                                                    <img src=""https://res.cloudinary.com/doi9kcogv/image/upload/v1730520231/App/emails/lqcln07vqydrchavgelc.png"" height=""60px"" alt="""">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=""height: 25px; text-align: center;"">
                                                                    <h1 style='font-family: Arial; font-size: 25px; color: #4a4a4a; font-weight: bold;'>¡Usuario creado con éxito!</h1>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=""height: 20px; text-align: center;"">
                                                                    <h2 style='font-family: Arial; font-size: 15px; color: #4a4a4a;'>Hola {payload.FirstName} {payload.LastName},</h2>
                                                                    <p style='font-family: Arial; font-size: 13px; color: #4a4a4a;'>Tu cuenta ha sido creada exitosamente. Ahora puedes acceder al portal utilizando el siguiente nombre de usuario:</p>
                                                                    <p style='font-family: Arial; font-size: 15px; color: #4a4a4a; font-weight: bold;'>{payload.UserName}</p>
                                                                </td>
                                                            </tr>
                                                            <tr >
                                                                <td style=""height: 15px;"">

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=""height: 100px;"">
                                                                    <a class=""link-button"" href=""https://coreexpresshn.com/"">Ingresa ahora</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </body>
                        </html>
                        ";

                //Send confirmation email to user
                bool confirmationEmailResponse = await BaseHelper.SendEmail($"{payload.FirstName} {payload.LastName}", payload.Email, confirmationEmailUserTemplate);

                if (!confirmationEmailResponse)
                    return new GenericHandlerResponse<long>(400, CustomMessage: "Error al enviar el correo de confirmacion al usuario");

                #endregion

                //Return 201 Created
                return new GenericHandlerResponse<long>(201, data: model.Id);

            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<long>(500, ExceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<long>> Update(UsersUpdateDTO payload, long usuId)
        {
            try
            {
                var UserData = await _context.AuthUsers.FindAsync(payload.Id);
                if (UserData == null)
                    return new GenericHandlerResponse<long>(404, CustomMessage: $"No se encontró la información del usuario solicitado.");

                var mediaUrl = "";
                DateTime currentDate = DateTime.UtcNow;

                #region 3. Actualizar imagen de perfil
                if (!payload.ProfilePicture.IsNullOrEmpty())
                {
                    var picture = BaseHelper.GetFileType(payload.ProfilePicture ?? "");

                    if (picture.File == null || picture.File.Length == 0)
                    {
                        return new GenericHandlerResponse<long>(400, CustomMessage: "La imagen proporcionada no es válida.");
                    }

                    var folderPath = Path.Combine("media", "files", "profiles");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = $"profileImage-{UserData.UserName}.{picture.Extension}";
                    var filePath = Path.Combine(folderPath, fileName);

                    await File.WriteAllBytesAsync(filePath, picture.File);

                    var domain = BaseHelper.GetEnvVariable("CDN_HOST");
                    mediaUrl = $"{domain}/files/profiles/{fileName}";
                }
                #endregion

                #region 4. Actualizar registro en la base de datos

                UserData.Email = payload.Email;
                //UserData.Dni = payload.Dni;
                UserData.FirstName = payload.FirstName;
                UserData.LastName = payload.LastName;
                UserData.ProfilePicture = string.IsNullOrEmpty(mediaUrl) ? UserData.ProfilePicture : mediaUrl;
                UserData.PhoneNumber = payload.PhoneNumber;
                UserData.BirthDate = payload.BirthDate.HasValue ? payload.BirthDate.Value : null;
                //UserData.Gender = payload.Gender;
                UserData.ModifiedBy = usuId;
                UserData.ModifiedDate = currentDate;
                UserData.IsActive = payload.IsActive;
                await _context.SaveChangesAsync();

                #endregion

                #region 5. Actualizar roles

                // Buscar las relaciones existentes para el usuario
                var existingUserRoles = await _context.AuthUserRoles
                    .Where(ur => ur.UserId == payload.Id)
                    .ToListAsync();

                // Eliminar los roles que ya no están en la lista
                var rolesToRemove = existingUserRoles
                    .Where(ur => !payload.Roles.Contains(ur.RoleId))
                    .ToList();

                _context.AuthUserRoles.RemoveRange(rolesToRemove);

                // Agregar nuevos roles que no existen
                var rolesToAdd = payload.Roles
                    .Where(roleId => !existingUserRoles.Any(ur => ur.RoleId == roleId))
                    .Select(roleId => new AuthUserRole
                    {
                        UserId = UserData.Id,
                        RoleId = roleId,
                        CreatedBy = payload.ModifiedBy,
                        CreatedDate = currentDate,
                        ModifiedBy = null,
                        ModifiedDate = null,
                        IsActive = true
                    })
                    .ToList();

                // Agregar las relaciones a la base de datos
                await _context.AuthUserRoles.AddRangeAsync(rolesToAdd);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();
                #endregion

                return new GenericHandlerResponse<long>(201, payload.Id);

            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<long>(500, ExceptionMessage: Ex.Message);
            }
        }

    }
}
