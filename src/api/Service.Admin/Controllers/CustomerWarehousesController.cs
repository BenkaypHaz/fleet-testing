using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "DropDownsController")]
    [Route("api/customer/{customerId}/warehouses")]
    public class CustomerWarehousesController : BaseController
    {
        public CustomerWarehousesController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet]
        public async Task<ActionResult<GenericResponseHandler<List<CustomerWarehouseReadDto>>>> GetCustomerWarehouses(long customerId, [FromQuery] FilterOptionsDto filterOptions)
        {
            var result = await _unitOfWork.CustomerWarehouseRepository.Get(filterOptions);
            return StatusCode(result.statusCode, result);
        }

    }
}