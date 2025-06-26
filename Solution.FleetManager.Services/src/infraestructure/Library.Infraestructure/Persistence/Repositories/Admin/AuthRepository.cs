using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Library.Infraestructure.Persistence.Repositories.Admin
{
    public class AuthRepository
    {
        private readonly DataBaseContext _context;
        public AuthRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericHandlerResponse<string>> SignIn(SignInDTO payload)
        {
            try
            {
                var userEntity = await _context.AuthUsers
                    .Where(user => user.UserName == payload.Username)
                    .FirstOrDefaultAsync();

                if (userEntity == null || !PasswordHelper.VerifyPassword(payload.Password, userEntity.Password))
                    return new GenericHandlerResponse<string>(401, CustomMessage: $"Credenciales inválidas.");

                if (!userEntity.IsActive)
                    return new GenericHandlerResponse<string>(403, CustomMessage: $"El usuario solicitado está inactivo.");

                var JwtPayload = new SessionDTO
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    ProfilePicture = userEntity.ProfilePicture,
                    PhoneNumber = userEntity.PhoneNumber,
                    DateOfBirth = userEntity.BirthDate.ToString(),
                    IsActive = userEntity.IsActive,
                };

                var userRoles = await _context.AuthUserRoles
                   .Where(r => r.UserId == JwtPayload.Id)
                   .Select(r => r.RoleId)
                   .ToListAsync();

                if (userRoles.Count > 0)
                {
                    var roles = await _context.AuthRoles
                        .Where(role => userRoles.Contains(role.Id))
                        .Select(role => new RolesReadDTO
                        {
                            Id = role.Id,
                            Description = role.Description,
                            CreatedBy = role.CreatedBy,
                            CreatedDate = role.CreatedDate,
                            ModifiedBy = role.ModifiedBy,
                            ModifiedDate = role.ModifiedDate
                        }).ToListAsync();

                    JwtPayload.Roles = roles;
                }

                var routeValues = await (from userRole in _context.AuthUserRoles
                                         join role in _context.AuthRoles on userRole.RoleId equals role.Id
                                         join authorizationRole in _context.AuthRoleAuthorizations on role.Id equals authorizationRole.RoleId
                                         join authorization in _context.AuthAuthorizations on authorizationRole.AuthId equals authorization.Id
                                         where userRole.UserId == JwtPayload.Id
                                         select authorization.RouteValue).ToListAsync();

                JwtPayload.Authorizations = routeValues;

                //var logData = new UsersActivityLog
                //{
                //    UserId = JwtPayload.Id,
                //    CreatedDate = DateTime.UtcNow
                //};

                //await _context.UsersActivityLogs.AddAsync(logData);
                await _context.SaveChangesAsync();

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BaseHelper.GetSha256(BaseHelper.GetConnectionString())));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, JwtPayload.Id.ToString()),
                        new Claim(ClaimTypes.Name, JwtPayload.UserName),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("Session", JsonConvert.SerializeObject(JwtPayload)),
                    };

                var token = new JwtSecurityToken(
                    issuer: BaseHelper.GetEnvVariable("SERVICES_HOST"),
                    audience: BaseHelper.GetEnvVariable("SERVICES_HOST"),
                    claims: claims,
                    expires: DateTime.Now.AddMonths(3),
                    signingCredentials: creds
                );

                var bearerToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new GenericHandlerResponse<string>(201, CustomMessage: "¡Inicio de sesión realizado con éxito!", data: bearerToken);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<string>(500, ExceptionMessage: Ex.Message);
            }
        }


        public async Task<GenericHandlerResponse<GetUserValidateInfoDTO>> ValidateUserUid(string uId)
        {
            try
            {

                var userData = await _context.AuthUsers
                                    .AsNoTracking()
                                    .Where(user => (user.UserName == uId || user.Email == uId))
                                    .Select(user => new GetUserValidateInfoDTO
                                    {
                                        Id = user.Id,
                                        Email = user.Email,
                                        PhoneNumber = user.PhoneNumber,
                                    }).FirstOrDefaultAsync();

                if (userData == null)
                    return new GenericHandlerResponse<GetUserValidateInfoDTO>(404, CustomMessage: $"No se encontró la información del usuario solicitado.");

                return new GenericHandlerResponse<GetUserValidateInfoDTO>(200, data: userData);

            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<GetUserValidateInfoDTO>(500, ExceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<int>> CreateUserForgotPwdToken(long id)
        {
            try
            {

                var userData = await _context.AuthUsers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Id == id);
                if (userData == null)
                    return new GenericHandlerResponse<int>(404, CustomMessage: $"No se encontró la información del usuario solicitado.");


                #region 1. Generar y guardar token
                var expirationDate = DateTime.UtcNow.AddHours(1);
                int token;
                string encriptedToken;

                do
                {
                    token = BaseHelper.GenerateRandomNum(6);
                    encriptedToken = BaseHelper.GetSha256(token.ToString());
                } while (await _context.AuthUserForgotPwdTokens
                    .AnyAsync(t => t.Token == encriptedToken));

                // Formatear token con espacio después del tercer dígito
                var tokenToView = token.ToString().Insert(3, " ");

                // Crear modelo para guardar en la base de datos
                var tokenData = new AuthUserForgotPwdToken
                {
                    Token = encriptedToken,
                    UserId = userData.Id,
                    ExpirationDate = expirationDate,
                    //IsActive = true
                };

                // Agregar token al contexto
                await _context.AuthUserForgotPwdTokens.AddAsync(tokenData);
                #endregion

                #region 2. Desactivar usuario y activar reseteo de contraseña
                //userData.ResetPassword = true;
                //userData.IsActive = false;
                #endregion

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                #region 3. Enviar correo al usuario con el token
                var resetPasswordEmailTemplate = @$"
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
                                                        <h1 style='font-family: Arial; font-size: 25px; color: #4a4a4a; font-weight: bold;'>¡Cambio de contraseña aprobado!</h1>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style=""height: 20px; text-align: center;"">
                                                        <h2 style='font-family: Arial; font-size: 15px; color: #4a4a4a;'>Hola {userData.FirstName} {userData.LastName},</h2>
                                                        <p style='font-family: Arial; font-size: 13px; color: #4a4a4a;'>Hemos recibido una solicitud para cambiar la contraseña de tu cuenta. Tu código de verificación es:</p>
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td style=""height: 15px;"">

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style=""height: 100px;"">
                                                        <p style='font-family: Arial; font-size: 28px; color: #4a4a4a; font-weight: bold; text-align: center;'>{tokenToView}</p>
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
                bool resetPasswordEmailResponse = await BaseHelper.SendEmail($"{userData.FirstName} {userData.LastName}", userData.Email, resetPasswordEmailTemplate, "CoreExpress - Cambio de contraseña aprobado");

                if (!resetPasswordEmailResponse)
                    return new GenericHandlerResponse<int>(400, CustomMessage: "Error al enviar el correo al usuario");

                #endregion

                //Return 201 Created
                return new GenericHandlerResponse<int>(201, data: 1, CustomMessage: "Cambio de contraseña habilitado");

            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<int>(500, ExceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<int>> ValidateForgotPwdToken(ValidateUserResetPasswordTokenDTO payload)
        {
            try
            {

                var tokenData = await _context.AuthUserForgotPwdTokens
                    .Where(token => token.Token == BaseHelper.GetSha256(payload.Token.ToString()))
                    .Include(request => request.User)
                    .FirstOrDefaultAsync();

                if (tokenData == null)
                    return new GenericHandlerResponse<int>(404, CustomMessage: $"No se encontro la informacion del token solicitado.");

                if (tokenData.User.Id != payload.Id)
                    return new GenericHandlerResponse<int>(403, CustomMessage: $"El token no corresponde al usuario solicitado.");

                //if (!tokenData.IsActive)
                //    return new GenericHandlerResponse<int>(400, CustomMessage: $"El token ya ha sido utilizado.");

                if (tokenData.ExpirationDate < DateTime.UtcNow)
                    return new GenericHandlerResponse<int>(400, CustomMessage: $"El token ha expirado.");

                //Return 201 Created
                return new GenericHandlerResponse<int>(201, data: 1, CustomMessage: "Token verificado.");

            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<int>(500, ExceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<long>> ResetPassword(ResetPasswordDTO payload)
        {
            try
            {
                #region 1. Validar token
                var tokenData = await _context.AuthUserForgotPwdTokens
                    .Where(token => token.Token == BaseHelper.GetSha256(payload.token.ToString()))
                    .FirstOrDefaultAsync();

                if (tokenData == null)
                    return new GenericHandlerResponse<long>(404, CustomMessage: $"No se encontró la información del token solicitado.");

                if (tokenData.UserId != payload.Id)
                    return new GenericHandlerResponse<long>(403, CustomMessage: $"El token no corresponde al usuario solicitado.");

                //if (!tokenData.IsActive)
                //    return new GenericHandlerResponse<long>(400, CustomMessage: $"El token ya ha sido utilizado.");

                if (tokenData.ExpirationDate < DateTime.UtcNow)
                    return new GenericHandlerResponse<long>(400, CustomMessage: $"El token ha expirado.");
                #endregion

                #region 2. Actualizar contraseña e inhabilitar token

                var userData = await _context.AuthUsers.FirstOrDefaultAsync(c => c.Id == tokenData.UserId);
                if (userData == null)
                    return new GenericHandlerResponse<long>(404, CustomMessage: $"No se encontró la información del usuario solicitado.");

                // Generar nueva clave con Argon2 y salt
                var password = PasswordHelper.HashPassword(payload.password);

                //tokenData.IsActive = false;
                userData.Password = password;
                userData.ResetPassword = false;
                userData.IsActive = true;

                await _context.SaveChangesAsync();
                #endregion

                return new GenericHandlerResponse<long>(201, data: userData.Id, CustomMessage: "¡Contraseña actualizada con éxito!");
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<long>(500, ExceptionMessage: Ex.Message);
            }
        }


    }
}
