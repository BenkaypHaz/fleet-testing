using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "TransportProvidersController")]
    [Route("api/[controller]")]
    public class TransportProvidersController : BaseController
    {
        public TransportProvidersController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet("profiles")]
        public async Task<ActionResult<GenericResponseHandler<List<ProviderProfileReadDto>>>> Get([FromQuery] FilterOptionsDto pagination)
        {
            var result = await _unitOfWork.ProviderProfileRepository.Get(pagination);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("profiles/{profileId}")]
        public async Task<ActionResult<GenericResponseHandler<ProviderProfileReadDto>>> GetById(long profileId)
        {
            var result = await _unitOfWork.ProviderProfileRepository.GetById(profileId);
            return StatusCode(result.statusCode, result);
        }

    }
}
