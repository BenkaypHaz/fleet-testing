using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Freights.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "ShipmentProjectContractController")]
    [Route("api/shipment-project-contract")]
    public class ShipmentProjectContractController : BaseController
    {
        public ShipmentProjectContractController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<ShipmentProjectContractReadDto>>>> Get()
        {
            var result = await _unitOfWork.ShipmentProjectContractRepository.Get();
            return StatusCode(result.statusCode, result);
        }


    }
}
