using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "SettingDispatchBranchesController")]
    [Route("api/dispatch-branches")]
    public class SettingDispatchBranchesController : BaseController
    {
        public SettingDispatchBranchesController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<SettingDispatchBranchReadDto>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.SettingDispatchBranchRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseHandler<SettingDispatchBranchReadFirstDto>>> GetById(long id)
        {
            var result = await _unitOfWork.SettingDispatchBranchRepository.GetById(id);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] SettingDispatchBranchCreateDto payload)
        {
            var result = await _unitOfWork.SettingDispatchBranchRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Put(long id, [FromBody] SettingDispatchBranchUpdateDto payload)
        {
            var result = await _unitOfWork.SettingDispatchBranchRepository.Update(id, payload, _userId);
            return StatusCode(result.statusCode, result);
        }

    }
}