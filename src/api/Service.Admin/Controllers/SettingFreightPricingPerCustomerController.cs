using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.FreightPricing.Read;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "SettingFreightPricingPerCustomerController")]
    [Route("api/freight-pricing-per-customer")]
    public class SettingFreightPricingPerCustomerController : BaseController
    {
        public SettingFreightPricingPerCustomerController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet("suggested-price")]
        public async Task<ActionResult<GenericResponseHandler<FreightPricingSuggestedPriceReadDto>>> GetSuggestedPrice(
            [FromQuery] long customerId,
            [FromQuery] long warehouseId,
            [FromQuery] long dispatchBranchId)
        {
            var result = await _unitOfWork.SettingFreightPricingPerCustomerRepository.GetSuggestedPrice(customerId, warehouseId, dispatchBranchId);
            return StatusCode(result.statusCode, result);
        }
    }
}