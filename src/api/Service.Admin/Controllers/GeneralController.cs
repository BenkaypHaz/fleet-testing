using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.General.Utils;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "GeneralController")]
    [Route("Api/[controller]")]
    public class GeneralController : BaseControllerUsers
    {
        public GeneralController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }


        /// <summary>Retorna un "Ok" para comprobar el correcto funcionamiento del controlador.</summary>
        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        [HttpGet("States/GetAll")]
        public async Task<ActionResult<GenericHandlerResponse<List<GenericDropDown>>>> Get()
        {
            var result = await _unitOfWork.GeneralRepository.GetStates();
            return StatusCode(result.statusCode, result);
        }

        [HttpGet("Cities/GetByStateId/{StateId}")]
        public async Task<ActionResult<GenericHandlerResponse<List<GenericDropDown>>>> Get(long StateId)
        {
            var result = await _unitOfWork.GeneralRepository.GetCities(StateId);
            return StatusCode(result.statusCode, result);
        }

    }
}
