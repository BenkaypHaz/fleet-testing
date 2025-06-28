using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Emails;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Infraestructure.Persistence.Repositories.Auth
{
    public class LoginRepository
    {
        private readonly DataBaseContext _context;
        public LoginRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericResponseHandler<string>> SignIn(SignInDTO payload)
        {
            try
            {
                var jwtPayload = await _context.AuthUsers
                    .Include(c => c.AuthUserRoleUsers)
                    .AsNoTracking()
                    .Where(c => (c.UserName == payload.Username || c.Email == payload.Username) && c.Password == PasswordHelper.HashPassword(payload.Password))
                    .Select(c => new JwtSessionDto
                    {
                        Id = c.Id,
                        Email = c.Email,
                        UserName = c.UserName,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        ProfilePicture = c.ProfilePicture,
                        IsActive = c.IsActive,
                    }).FirstOrDefaultAsync();

                if (jwtPayload == null)
                    return new GenericResponseHandler<string>(404, null);
                if (!jwtPayload.IsActive)
                    return new GenericResponseHandler<string>(401, null, message: "The user account is inactive.");

                var roles = await _context.AuthUserRoles
                    .AsNoTracking()
                    .Where(c => c.UserId == jwtPayload.Id)
                    .Select(c => c.Role.Id)
                    .ToListAsync();

                var authorizations = await _context.AuthRoleAuthorizations
                    .AsNoTracking()
                    .Where(c => roles.Contains(c.RoleId)).Select(c => c.Auth.RouteValue)
                    .ToListAsync();

                jwtPayload.Authorizations = authorizations;

                var secretKey = BaseHelper.GetEnvVariable("AUTH_SECRET_KEY");
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, jwtPayload.Id.ToString()),
                    new Claim(ClaimTypes.Name, jwtPayload.UserName),
                    new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(authorizations)),
                    new Claim("Session", JsonConvert.SerializeObject(jwtPayload)),
                };

                var token = new JwtSecurityToken(
                    issuer: BaseHelper.GetEnvVariable("PROJECT_SERVICES_HOST"),
                    audience: BaseHelper.GetEnvVariable("PROJECT_SERVICES_HOST"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMonths(3),
                    signingCredentials: creds
                );
                var bearerToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new GenericResponseHandler<string>(200, data: bearerToken);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<string>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<UserValidateInfoReadDto>> ValidateUserUid(string uId)
        {
            var userData = await _context.AuthUsers
                                .AsNoTracking()
                                .Where(user => (user.UserName == uId || user.Email == uId))
                                .Select(user => new UserValidateInfoReadDto
                                {
                                    Id = user.Id,
                                    Email = user.Email,
                                    PhoneNumber = user.PhoneNumber,
                                }).FirstOrDefaultAsync();
            if (userData == null)
                return new GenericResponseHandler<UserValidateInfoReadDto>(404, message: $"No se encontró la información del usuario solicitado.");

            return new GenericResponseHandler<UserValidateInfoReadDto>(200, data: userData);
        }

        public async Task<GenericResponseHandler<int>> CreateUserForgotPwdToken(long id)
        {


            var userData = await _context.AuthUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);
            if (userData == null)
                return new GenericResponseHandler<int>(404, message: $"No se encontró la información del usuario solicitado.");


            #region 1. Generar y guardar token
            var expirationDate = DateTime.UtcNow.AddHours(1);
            int token;
            string encriptedToken;

            do
            {
                token = PasswordHelper.GenerateVerificationCode(6);
                encriptedToken = PasswordHelper.HashPassword(token.ToString());
            } while (await _context.AuthUserForgotPwdTokens
                .AnyAsync(t => t.Token == encriptedToken));

            var tokenToView = token.ToString().Insert(3, " ");

            var tokenData = new AuthUserForgotPwdToken
            {
                Token = encriptedToken,
                UserId = userData.Id,
                ExpirationDate = expirationDate,
                IsActive = true
            };

            await _context.AuthUserForgotPwdTokens.AddAsync(tokenData);
            #endregion

            await _context.SaveChangesAsync();

            #region 3. Enviar correo al usuario con el token

            var template = @$"
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
            </html>";

            var toEmails = new List<ToEmailsDto>
                {
                    new ToEmailsDto { NameRecipient = $"{userData.FirstName} {userData.LastName}", EmailRecipient = userData.Email }
                };
            bool resetPasswordEmailResponse = await MailDeliveryHelper.SendNoReplyEmail(
                subject: "Reset password",
                toEmails: toEmails,
                template,
                null, 
                false
            );

            if (!resetPasswordEmailResponse)
                return new GenericResponseHandler<int>(400, message: "Error al enviar el correo al usuario");

            #endregion

            return new GenericResponseHandler<int>(201, data: 1, message: "Cambio de contraseña habilitado");

        }

        public async Task<GenericResponseHandler<int>> ValidateForgotPwdToken(ValidateUserResetPasswordTokenDto payload)
        {
            var tokenData = await _context.AuthUserForgotPwdTokens
                .Where(token => token.Token == PasswordHelper.HashPassword(payload.Token.ToString()))
                .Include(request => request.User)
                .FirstOrDefaultAsync();

            if (tokenData == null)
                return new GenericResponseHandler<int>(404, message: $"No se encontro la informacion del token solicitado.");

            if (tokenData.User.Id != payload.Id)
                return new GenericResponseHandler<int>(403, message: $"El token no corresponde al usuario solicitado.");

            if (!tokenData.IsActive)
                return new GenericResponseHandler<int>(400, message: $"El token ya ha sido utilizado.");

            if (tokenData.ExpirationDate < DateTime.UtcNow)
                return new GenericResponseHandler<int>(400, message: $"El token ha expirado.");

            return new GenericResponseHandler<int>(201, data: 1, message: "Token verificado.");
        }

        public async Task<GenericResponseHandler<long>> ResetPassword(ResetPasswordDto payload)
        {

            #region 1. Validar token
            var tokenData = await _context.AuthUserForgotPwdTokens
                .Where(token => token.Token == PasswordHelper.HashPassword(payload.Token.ToString()))
                .FirstOrDefaultAsync();

            if (tokenData == null)
                return new GenericResponseHandler<long>(404, message: $"No se encontró la información del token solicitado.");

            if (tokenData.UserId != payload.UserId)
                return new GenericResponseHandler<long>(403, message: $"El token no corresponde al usuario solicitado.");

            if (!tokenData.IsActive)
                return new GenericResponseHandler<long>(400, message: $"El token ya ha sido utilizado.");

            if (tokenData.ExpirationDate < DateTime.UtcNow)
                return new GenericResponseHandler<long>(400, message: $"El token ha expirado.");
            #endregion

            #region 2. Actualizar contraseña e inhabilitar token

            var userData = await _context.AuthUsers.FirstOrDefaultAsync(c => c.Id == tokenData.UserId);
            if (userData == null)
                return new GenericResponseHandler<long>(404, message: $"No se encontró la información del usuario solicitado.");

            // Generar nueva clave con Argon2 y salt
            var password = PasswordHelper.HashPassword(payload.Password);

            tokenData.IsActive = false;
            userData.Password = password;
            userData.ResetPassword = false;
            userData.IsActive = true;

            await _context.SaveChangesAsync();
            #endregion

            return new GenericResponseHandler<long>(201, data: userData.Id, message: "¡Contraseña actualizada con éxito!");

        }

    }
}
