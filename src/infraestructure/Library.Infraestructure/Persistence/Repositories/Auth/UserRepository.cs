using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Library.Infraestructure.Persistence.Repositories.Auth
{
    public class UserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericResponseHandler<List<UserReadDTO>>> Get(FilterOptionsDto filterOptions)
        {
            var data = new List<UserReadDTO>();
          
                var query = _context.AuthUsers.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(filterOptions.searchValue))
                {
                    string[] searchValues = filterOptions.searchValue.Split(" ");
                    foreach (var value in searchValues)
                    {
                        query = query.Where(a =>
                            a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                            a.FirstName.Equals(value) || a.FirstName.Contains(value) ||
                            a.LastName.Equals(value) || a.LastName.Contains(value) ||
                            a.Email.Equals(value) || a.Email.Contains(value) ||
                            a.UserName.Equals(value) || a.UserName.Contains(value));
                    }
                }

                var totalRecords = await query.CountAsync();
                data = await query
                    .Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                    .Take(filterOptions.recordsPerPage)
                    .OrderBy(c => c.FirstName)
                    .Select(c => new UserReadDTO
                    {
                        Id = c.Id,
                        UserName = c.UserName,
                        Email = c.Email,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        ProfilePicture = c.ProfilePicture,
                        IsActive = c.IsActive,
                    }).ToListAsync();

                return new GenericResponseHandler<List<UserReadDTO>>(200, data, totalRecords);
         
        }

        public async Task<GenericResponseHandler<JwtSessionDto>> GetSessionContext(long id)
        {
           
                var data = await _context.AuthUsers
                    .Where(c => c.Id.Equals(id))
                    .Select(c => new JwtSessionDto
                    {
                        Id = c.Id,
                        UserName = c.UserName,
                        Email = c.Email,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        ProfilePicture = c.ProfilePicture,
                        Roles = c.AuthUserRoleUsers.Select(x => x.Role.Description).ToList(),
                        IsActive = c.IsActive,
                    }).FirstOrDefaultAsync();

                if (data == null)
                    return new GenericResponseHandler<JwtSessionDto>(404, null);

                var roles = await _context.AuthUserRoles
                    .AsNoTracking()
                    .Where(c => c.UserId == data.Id)
                    .Select(c => c.Role.Id)
                    .ToListAsync();

                var authorizations = await _context.AuthRoleAuthorizations
                    .Where(c => roles.Contains(c.RoleId))
                    .Select(c => c.Auth.RouteValue)
                    .ToListAsync();

                data.Authorizations = authorizations;

                return new GenericResponseHandler<JwtSessionDto>(200, data);
           
        }

        public async Task<GenericResponseHandler<UserReadFirstDTO>> GetById(long id)
        {
            
                var data = await _context.AuthUsers
                    .Where(c => c.Id == id)
                    .Select(c => new UserReadFirstDTO
                    {
                        Id = c.Id,
                        UserName = c.UserName,
                        Email = c.Email,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        ProfilePicture = c.ProfilePicture,
                        PhoneNumber = c.PhoneNumber,
                        BirthDate = c.BirthDate != null ? Convert.ToDateTime(c.BirthDate) : null,
                        CreatedBy = c.CreatedBy,
                        Roles = c.AuthUserRoleUsers.Select(x => new RoleReadDTO
                        {
                            Id = x.Role.Id,
                            Description = x.Role.Description,
                            IsActive = x.Role.IsActive,
                        }).ToList(),
                        ResetPassword = false,
                        CreatedDate = c.CreatedDate,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedDate = c.ModifiedDate,
                        IsActive = c.IsActive,
                    }).FirstOrDefaultAsync();

                if (data == null)
                    return new GenericResponseHandler<UserReadFirstDTO>(404, null);
                var dataRecords = data != null ? 1 : 0;

                return new GenericResponseHandler<UserReadFirstDTO>(200, data, dataRecords);
          
        }

        public async Task<GenericResponseHandler<long?>> Create(UserCreateDto payload, long createdBy)
        {
           
                var user = await _context.AuthUsers.FirstOrDefaultAsync(x => x.UserName == payload.UserName);
                if (user != null)
                    return new GenericResponseHandler<long?>(404, null, message: $"The username: {user.UserName} already exists in the database.");

                DateTime currentDate = DateTime.UtcNow;

                var mediaUrl = string.Empty;
                if (!payload.ProfilePicture.IsNullOrEmpty())
                {
                    var profilePicture = BaseHelper.GetFileType(payload.ProfilePicture ?? "");
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "media", "content", "profiles");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, $"{payload.UserName}.{profilePicture.Extension}");
                    await File.WriteAllBytesAsync(filePath, profilePicture.File);

                    var domain = BaseHelper.GetEnvVariable("PROJECT_CDN_HOST");
                    mediaUrl = $"{domain}/content/uploads/profiles/{payload.UserName}.{profilePicture.Extension}";
                }

                var model = new AuthUser
                {
                    UserName = payload.UserName,
                    Email = payload.Email,
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    ProfilePicture = mediaUrl.IsNullOrEmpty() ? null : mediaUrl,
                    PhoneNumber = payload.PhoneNumber,
                    BirthDate = payload.BirthDate.HasValue ? DateOnly.FromDateTime(payload.BirthDate.Value) : null,
                    Password = PasswordHelper.HashPassword(payload.Password ?? DateTime.UtcNow.ToString().Replace(" ", "")),
                    ResetPassword = false,
                    CreatedBy = createdBy,
                    CreatedDate = currentDate,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true,
                };

                await _context.AuthUsers.AddAsync(model);
                await _context.SaveChangesAsync();

                if (payload.Role_Id.Length > 0)
                {
                    var userRoles = payload.Role_Id.Select(roleId => new AuthUserRole
                    {
                        UserId = model.Id,
                        RoleId = roleId,
                        CreatedBy = createdBy,
                        CreatedDate = currentDate,
                        ModifiedBy = null,
                        ModifiedDate = null,
                        IsActive = true
                    }).ToList();
                    await _context.AuthUserRoles.AddRangeAsync(userRoles);
                    await _context.SaveChangesAsync();
                }

                return new GenericResponseHandler<long?>(201, model.Id);
           
        }

        public async Task<GenericResponseHandler<long?>> Update(long id, UserUpdateDto payload, long modifiedBy)
        {
           
                var user = await _context.AuthUsers.FindAsync(id);
                if (user == null)
                    return new GenericResponseHandler<long?>(404, null);

                DateTime currentDate = DateTime.UtcNow;
                var mediaUrl = string.Empty;

                if (!payload.ProfilePicture.IsNullOrEmpty())
                {
                    var profilePicture = BaseHelper.GetFileType(payload.ProfilePicture ?? "");
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "media", "content", "profiles");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, $"{payload.UserName}.{profilePicture.Extension}");
                    await File.WriteAllBytesAsync(filePath, profilePicture.File);

                    var domain = BaseHelper.GetEnvVariable("PROJECT_CDN_HOST");
                    mediaUrl = $"{domain}/content/uploads/profiles/{payload.UserName}.{profilePicture.Extension}";
                }

                user.Email = payload.Email;
                user.FirstName = payload.FirstName;
                user.LastName = payload.LastName;
                user.ProfilePicture = mediaUrl;
                user.PhoneNumber = payload.PhoneNumber;
                user.BirthDate = payload.BirthDate.HasValue ? DateOnly.FromDateTime(payload.BirthDate.Value) : null;
                user.ModifiedBy = payload.ModifiedBy;
                user.ModifiedDate = currentDate;
                user.IsActive = payload.IsActive;
                await _context.SaveChangesAsync();


                var existingUserRoles = await _context.AuthUserRoles
                    .Where(ur => ur.UserId == id)
                    .ToListAsync();
                var rolesToRemove = existingUserRoles
                    .Where(ur => !payload.Role_Id.Contains(ur.RoleId))
                    .ToList();
                if (rolesToRemove.Any())
                    _context.AuthUserRoles.RemoveRange(rolesToRemove);

                var existingRoleIds = existingUserRoles.Select(role => role.Id).ToHashSet();
                var rolesToAdd = payload.Role_Id
                    .Where(roleId => !existingRoleIds.Any(existRoleId => existRoleId == roleId))
                    .Select(roleId => new AuthUserRole
                    {
                        UserId = user.Id,
                        RoleId = roleId,
                        CreatedBy = modifiedBy,
                        CreatedDate = currentDate,
                        ModifiedBy = null,
                        ModifiedDate = null,
                        IsActive = true
                    }).ToList();
                await _context.AuthUserRoles.AddRangeAsync(rolesToAdd);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(200, id);

          
        }

        public async Task<GenericResponseHandler<string>> CreateForgotPwdToken(long id, long modifiedBy)
        {
           
                var user = await _context.AuthUsers.FindAsync(id);
                if (user == null)
                    return new GenericResponseHandler<string>(404, message: "The user you are trying to reset the password for does not exist.");

                DateTime currentDate = DateTime.UtcNow;
                user.Password = PasswordHelper.HashPassword(DateTime.UtcNow.ToString().Replace(" ", ""));
                user.ResetPassword = true;
                user.IsActive = false;
                user.ModifiedBy = modifiedBy;
                user.ModifiedDate = currentDate;
                await _context.SaveChangesAsync();

                var forgotPwdTokenId = await _context.AuthUserForgotPwdTokens.MaxAsync(x => x.Id);
                forgotPwdTokenId++;
                string token = PasswordHelper.HashPassword(DateTime.UtcNow.ToString().Replace(" ", ""));

                var model = new AuthUserForgotPwdToken
                {
                    Id = forgotPwdTokenId,
                    ExpirationDate = currentDate.AddHours(2),
                    UserId = id,
                    Token = token,
                    CreatedBy = modifiedBy,
                    CreatedDate = currentDate,
                };
                await _context.AuthUserForgotPwdTokens.AddAsync(model);
                await _context.SaveChangesAsync();

                var userPayload = new UserReadDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                };
                return new GenericResponseHandler<string>(200);

                //var submitEmailState = await BaseHelper.SendEmailRecoveryPassword(userPayload, token);

                //if (submitEmailState)
                //    return new GenericResponseHandler<string>(200);
                //else
                //    return new GenericResponseHandler<string>(500);
         
        }

        public async Task<GenericResponseHandler<long?>> UpdateStatus(long id, bool status, long modifiedBy)
        {
               var user = await _context.AuthUsers.FindAsync(id);
                if (user == null)
                    return new GenericResponseHandler<long?>(404, null);

                DateTime currentDate = DateTime.UtcNow;
                user.ModifiedBy = modifiedBy;
                user.ModifiedDate = currentDate;
                user.IsActive = status;
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(200, id);

        }

    }
}
