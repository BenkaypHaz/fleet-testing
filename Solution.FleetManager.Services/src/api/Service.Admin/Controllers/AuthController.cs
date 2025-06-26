using Library.Infraestructure.Common.Attributes.Swagger;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "AuthController")]
    [Route("Api/[controller]")]
    public class AuthController : BaseControllerUsers
    {
        public AuthController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor): base(logger, unitOfWork, httpContextAccessor){ }

        [SwaggerDocumentation("Obtiene el usuario por ID", 200, 400, 404, 500)]
        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "Ok!";
        }


        /// <summary>
        /// Authenticates a user and returns a token for access.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Auth/SignIn
        ///     {
        ///         "Username": "johndoe",
        ///         "Password": "yourSecurePassword123"
        ///     }
        ///
        /// </remarks>
        /// <param name="Payload">The user's login credentials.</param>
        /// <response code="200">User authenticated successfully and token returned.</response>
        /// <response code="400">Invalid username or password.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("SignIn")]
        public async Task<ActionResult<GenericHandlerResponse<string>>> SignIn([FromBody] SignInDTO payload)
        {
            var result = await _unitOfWork.AuthRepository.SignIn(payload);
            return StatusCode(result.statusCode, result);
        }


        /// <summary>
        /// Validates a user's UID and returns the associated user information.
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/Auth/{uId}/validate-user-uid
        /// 
        ///     Response:
        ///     {
        ///         "Id": 123,
        ///         "Email": "user@example.com",
        ///         "PhoneNumber": "+1234567890"
        ///     }
        /// </remarks>
        /// <param name="uId">The unique identifier (UID) of the user to validate.</param>
        /// <response code="200">User UID successfully validated, and user information returned.</response>
        /// <response code="400">Invalid UID provided.</response>
        /// <response code="404">User with the given UID was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("{uId}/validate-user-uid")]
        public async Task<ActionResult<GenericHandlerResponse<GetUserValidateInfoDTO>>> ValidateUserUid(string uId)
        {
            var result = await _unitOfWork.AuthRepository.ValidateUserUid(uId);
            return StatusCode(result.statusCode, result);
        }


        /// <summary>
        /// Generates a forgot password token for a user identified by their ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        ///     POST /api/Auth/{id}/generate-forgot-password-token
        /// 
        ///     Response:
        ///     {
        ///         "statusCode": 200,
        ///         "data": 123456
        ///     }
        /// </remarks>
        /// <param name="id">The unique identifier (ID) of the user for whom the forgot password token will be generated.</param>
        /// <response code="200">Password reset token successfully generated and returned.</response>
        /// <response code="400">Invalid user ID or invalid request.</response>
        /// <response code="404">User with the provided ID not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("{id}/generate-forgot-password-token")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericHandlerResponse<int>>> CreateUserForgotPwdToken(long id)
        {
            var result = await _unitOfWork.AuthRepository.CreateUserForgotPwdToken(id);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Validates a forgot password token for a user.
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        ///     POST /api/Auth/valite-forgot-password-token
        /// 
        ///     Request Body:
        ///     {
        ///         "Id": 123,
        ///         "Token": 987654
        ///     }
        /// 
        ///     Response:
        ///     {
        ///         "statusCode": 200,
        ///         "data": 1
        ///     }
        /// </remarks>
        /// <param name="payload">The request payload containing the user ID and the token to validate.</param>
        /// <response code="200">The token is valid, and the request has been processed successfully.</response>
        /// <response code="400">Invalid token or user ID.</response>
        /// <response code="404">User with the provided ID not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("valite-forgot-password-token")]
        public async Task<ActionResult<GenericHandlerResponse<int>>> ValidateForgotPwdToken(ValidateUserResetPasswordTokenDTO payload)
        {
            var result = await _unitOfWork.AuthRepository.ValidateForgotPwdToken(payload);
            return StatusCode(result.statusCode, result);
        }


        /// <summary>
        /// Resets the password for a user using the provided token and new password.
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        ///     PUT /api/Auth/reset-password
        /// 
        ///     Request Body:
        ///     {
        ///         "Id": 123,
        ///         "token": 987654,
        ///         "password": "newSecurePassword123"
        ///     }
        /// 
        ///     Response:
        ///     {
        ///         "statusCode": 200,
        ///         "data": 123
        ///     }
        /// </remarks>
        /// <param name="Payload">The request payload containing the user ID, reset token, and new password.</param>
        /// <response code="200">Password successfully reset.</response>
        /// <response code="400">Invalid token, user ID, or password.</response>
        /// <response code="404">User with the provided ID not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("reset-password")]
        public async Task<ActionResult<GenericHandlerResponse<long>>> ResetPassword([FromBody] ResetPasswordDTO payload)
        {
            var result = await _unitOfWork.AuthRepository.ResetPassword(payload);
            return StatusCode(result.statusCode, result);
        }

    }
}
