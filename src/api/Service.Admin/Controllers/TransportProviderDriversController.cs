using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "TransportProviderDriversController")]
    [Route("api/[controller]")]
    public class TransportProviderDriversController : BaseController
    {
        public TransportProviderDriversController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<ProviderDriverReadDto>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.ProviderDriverRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }
    }
}
