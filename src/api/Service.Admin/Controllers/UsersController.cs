using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "UsersController")]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        public UsersController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }


        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<UserReadDTO>>>> Get([FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.UserRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseHandler<UserReadDTO>>> GetById(long id)
        {
            var result = await _unitOfWork.UserRepository.GetById(id);
            return StatusCode(result.statusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long>>> Post([FromBody] UserCreateDto Payload)
        {
            var result = await _unitOfWork.UserRepository.Create(Payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseHandler<long>>> Put(long id, [FromBody] UserUpdateDto Payload)
        {
            var result = await _unitOfWork.UserRepository.Update(id, Payload, _userId);
            return StatusCode(result.statusCode, result);
        }

    }
}
