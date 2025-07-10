using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ShipmentFreight.Create;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Shipments.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "ShipmentFreightController")]
    [Route("api/shipment-freight")]
    public class ShipmentFreightController : BaseController
    {
        public ShipmentFreightController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] ShipmentFreightCreateDto payload)
        {
            var result = await _unitOfWork.ShipmentFreightRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }

    }
}
