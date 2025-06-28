using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleBrand.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "DropDownsController")]
    [Route("api/[controller]")]
    public class BusinessPartnerController : BaseControllerUsers
    {
        public BusinessPartnerController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(logger, unitOfWork, httpContextAccessor) { }

        [HttpGet("health-check")]
        public string HealthCheck()
        {
            return "Ok!";
        }


        /// <summary>
        /// Obtiene las marcas de vehículos filtradas por búsqueda
        /// </summary>
        [HttpGet("vehicle-brands")]
        public async Task<ActionResult<GenericResponseHandler<List<VehicleBrandReadDto>>>> GetVehicleBrands([FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.VehicleBrandRepository.GetBySearch(searchValue);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Obtiene los modelos de vehículos filtrados por marca y búsqueda
        /// </summary>
        [HttpGet("vehicle-models")]
        public async Task<ActionResult<GenericResponseHandler<List<VehicleModelReadDto>>>> GetVehicleModels(
            [FromQuery] long? brandId,
            [FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.VehicleModelRepository.GetByBrandAndSearch(brandId, searchValue);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Obtiene los proveedores filtrados por búsqueda
        /// </summary>
        [HttpGet("provider-profiles")]
        public async Task<ActionResult<GenericResponseHandler<List<ProviderProfileReadDto>>>> GetProviderProfiles([FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.ProviderProfileRepository.GetBySearch(searchValue);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Obtiene los conductores filtrados por búsqueda
        /// </summary>
        [HttpGet("provider-drivers")]
        public async Task<ActionResult<GenericResponseHandler<List<ProviderDriverReadDto>>>> GetProviderDrivers([FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.ProviderDriverRepository.GetBySearch(searchValue);
            return StatusCode(result.statusCode, result);
        }



        /// <summary>
        /// Obtiene las unidades de transporte filtradas por búsqueda
        /// </summary>
        [HttpGet("transport-vehicles")]
        public async Task<ActionResult<GenericResponseHandler<List<TransportVehicleReadDto>>>> GetTransportVehicles([FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.TransportVehicleRepository.GetBySearch(searchValue);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Obtiene los almacenes de clientes filtrados por búsqueda
        /// </summary>
        [HttpGet("customer-warehouses")]
        public async Task<ActionResult<GenericResponseHandler<List<CustomerWarehouseReadDto>>>> GetCustomerWarehouses([FromQuery] string? searchValue)
        {
            var result = await _unitOfWork.CustomerWarehouseRepository.GetBySearch(searchValue);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Agregamos un nuevo transport vehicle
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GenericResponseHandler<long?>>> Post([FromBody] TransportVehicleCreateDto payload)
        {
            var result = await _unitOfWork.TransportVehicleRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }
    }
}