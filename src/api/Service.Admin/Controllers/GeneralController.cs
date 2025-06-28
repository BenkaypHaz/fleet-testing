using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Utils.Generics;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "GeneralController")]
    [Route("api/general")]
    public class GeneralController : BaseController
    {
        public GeneralController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }


        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet("regions")]
        public async Task<ActionResult<GenericResponseHandler<List<GenericDropDown>>>> Get()
        {
            var result = await _unitOfWork.RegionRepository.Get();
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("cities/{regionId}")]
        public async Task<ActionResult<GenericResponseHandler<List<GenericDropDown>>>> Get(long regionId)
        {
            var result = await _unitOfWork.CityRepository.GetByRegionId(regionId);
            return StatusCode(result.statusCode, result);
        }

    }
}
