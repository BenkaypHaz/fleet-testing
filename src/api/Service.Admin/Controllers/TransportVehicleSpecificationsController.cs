using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleBrand.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "TransportVehicleSpecificationsController")]
    [Route("api/transport-vehicle-specifications")]
    public class TransportVehicleSpecificationsController : BaseController
    {
        public TransportVehicleSpecificationsController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet("brands")]
        public async Task<ActionResult<GenericResponseHandler<List<VehicleBrandReadDto>>>> GetBrands([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.VehicleBrandRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("models")]
        public async Task<ActionResult<GenericResponseHandler<List<VehicleModelReadDto>>>> GetModels([FromQuery] long? brandId, [FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.VehicleModelRepository.Get(brandId, filterOptions);
            return StatusCode(result.statusCode, result);
        }

    }
}
