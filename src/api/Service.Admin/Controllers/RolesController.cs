using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "RolesController")]
    [Route("api/[controller]")]
    public class RolesController : BaseController
    {
        public RolesController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<RoleReadDTO>>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var result = await _unitOfWork.RoleRepository.Get(paginationDTO);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseHandler<List<RoleReadFirstDto>>>> GetById(long id)
        {
            var result = await _unitOfWork.RoleRepository.GetById(id);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] RoleCreateDto payload)
        {
            var result = await _unitOfWork.RoleRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Put(long id, [FromBody] RoleUpdateDto payload)
        {
            var result = await _unitOfWork.RoleRepository.Update(id, payload, _userId);
            return StatusCode(result.statusCode, result);
        }

    }
}
