using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "AuthorizationsController")]
    [Route("api/authorizations")]
    public class AuthorizationsController : BaseController
    {
        public AuthorizationsController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        /// <summary>Returns an "Ok" to check the correct functioning of the controller.</summary>
        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<ModuleReadDto>>>> Get()
        {
            var result = await _unitOfWork.AuthorizationRepository.Get();
            return StatusCode(result.statusCode, result);
        }

    }
}
