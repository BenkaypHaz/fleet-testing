using Library.Infraestructure.Common.Attributes.Swagger;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Login.Update;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "AuthController")]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(ILogger<BaseController> logger, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor): base(logger, unitOfWork, httpContextAccessor){ }

        [SwaggerDocumentation("Obtiene el usuario por ID", 200, 400, 404, 500)]
        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<GenericResponseHandler<string>>> SignIn([FromBody] SignInDTO payload)
        {
            var result = await _unitOfWork.LoginRepository.SignIn(payload);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("{uId}/validate-user-uid")]
        public async Task<ActionResult<GenericResponseHandler<UserValidateInfoReadDto>>> ValidateUserUid(string uId)
        {
            var result = await _unitOfWork.LoginRepository.ValidateUserUid(uId);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost("{id}/generate-forgot-password-token")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponseHandler<int>>> CreateUserForgotPwdToken(long id)
        {
            var result = await _unitOfWork.LoginRepository.CreateUserForgotPwdToken(id);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost("valite-forgot-password-token")]
        public async Task<ActionResult<GenericResponseHandler<int>>> ValidateForgotPwdToken(ValidateUserResetPasswordTokenDto payload)
        {
            var result = await _unitOfWork.LoginRepository.ValidateForgotPwdToken(payload);
            return StatusCode(result.statusCode, result);
        }

        [HttpPut("reset-password")]
        public async Task<ActionResult<GenericResponseHandler<long>>> ResetPassword([FromBody] ResetPasswordDto payload)
        {
            var result = await _unitOfWork.LoginRepository.ResetPassword(payload);
            return StatusCode(result.statusCode, result);
        }

    }
}
