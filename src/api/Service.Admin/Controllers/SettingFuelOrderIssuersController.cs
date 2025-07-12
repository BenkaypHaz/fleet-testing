using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "SettingFuelOrderIssuersController")]
    [Route("api/fuel-order-issuers")]
    public class SettingFuelOrderIssuersController : BaseController
    {
        public SettingFuelOrderIssuersController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<SettingFuelOrderIssuerReadDto>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.SettingFuelOrderIssuerRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseHandler<SettingFuelOrderIssuerReadFirstDto>>> GetById(long id)
        {
            var result = await _unitOfWork.SettingFuelOrderIssuerRepository.GetById(id);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] SettingFuelOrderIssuerCreateDto payload)
        {
            var result = await _unitOfWork.SettingFuelOrderIssuerRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Put(long id, [FromBody] SettingFuelOrderIssuerUpdateDto payload)
        {
            var result = await _unitOfWork.SettingFuelOrderIssuerRepository.Update(id, payload,_userId);
            return StatusCode(result.statusCode, result);
        }

    }
}