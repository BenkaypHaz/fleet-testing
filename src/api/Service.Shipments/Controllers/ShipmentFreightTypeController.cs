using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Shipments.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "ShipmentFreightTypeController")]
    [Route("api/shipment-freight-type")]
    public class ShipmentFreightTypeController : BaseController
    {
        public ShipmentFreightTypeController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<ShipmentProjectContractReadDto>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.ShipmentFreightTypeRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }


    }
}
