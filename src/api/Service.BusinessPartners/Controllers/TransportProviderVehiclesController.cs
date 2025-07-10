using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.BusinessPartners.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "TransportProviderVehiclesController")]
    [Route("api/transport-provider-vehicles")]
    public class TransportProviderVehiclesController : BaseController
    {
        public TransportProviderVehiclesController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<TransportVehicleReadDto>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.TransportVehicleRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] TransportVehicleCreateDto payload)
        {
            var result = await _unitOfWork.TransportVehicleRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }
    }
}
