using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Update;
using Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "RolesController")]
    [Route("Api/[controller]")]
    public class RolesController : BaseControllerUsers
    {
        public RolesController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }


        /// <summary>Returns an "Ok" to check the correct functioning of the controller.</summary>
        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        /// <summary>
        /// Retrieves a paginated list of roles from the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/Roles/GetAll?page=1?recordsPerPage=10
        ///
        /// This endpoint returns a list of roles based on the specified pagination parameters.
        /// </remarks>
        /// <param name="paginationDTO">The pagination details including the page number and number of records per page.</param>
        /// <response code="200">A list of roles retrieved successfully.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAll")]
        public async Task<ActionResult<GenericHandlerResponse<List<RolesReadDTO>>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var result = await _unitOfWork.RolesRepository.Get(paginationDTO);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Retrieves a role by its unique identifier.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/Roles/GetById?id=1
        ///
        /// This endpoint returns the details of a specific role based on the provided ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the role to retrieve.</param>
        /// <response code="200">The role details retrieved successfully.</response>
        /// <response code="404">The role with the specified ID was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetById")]
        public async Task<ActionResult<GenericHandlerResponse<List<RolesReadFirstDTO>>>> GetById(long id)
        {
            var result = await _unitOfWork.RolesRepository.GetById(id);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Adds a new role to the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Roles/Add
        ///     {
        ///         "description": "New Role Description",
        ///         "createdBy": 1
        ///     }
        ///
        /// This endpoint creates a new role using the provided details.
        /// </remarks>
        /// <param name="payload">The details of the role to create, including description and creator ID.</param>
        /// <response code="201">The role was created successfully, and the ID of the new role is returned.</response>
        /// <response code="400">The request data is invalid.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("Add")]
        public async Task<ActionResult<GenericHandlerResponse<long>>> Post([FromBody] RolesCreateDTO payload)
        {
            var result = await _unitOfWork.RolesRepository.Create(payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Updates an existing role in the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Roles/Update
        ///     {
        ///         "id": 1,
        ///         "description": "Updated Role Description",
        ///         "modifiedBy": 2,
        ///         "isActive": true
        ///     }
        ///
        /// This endpoint updates the details of an existing role based on the provided information.
        /// </remarks>
        /// <param name="Payload">The details of the role to update, including ID, description, modifier ID, and active status.</param>
        /// <response code="200">The role was updated successfully.</response>
        /// <response code="404">The role with the specified ID was not found.</response>
        /// <response code="400">The request data is invalid.</response>
        /// <response code="500">An internal server error occurred.</response>
        //[HttpPut("Update")]
        //public async Task<ActionResult<GenericHandlerResponse<long>>> Put([FromBody] RolesUpdateDTO Payload)
        //{
        //    var result = await _unitOfWork.RolesRepository.Update(Payload);
        //    return StatusCode(result.statusCode, result);
        //}

        /// <summary>
        /// Adds authorizations to a specified role.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Roles/AddAuthorizations
        ///     {
        ///         "roleId": 1,
        ///         "auths": [
        ///             {
        ///                 "id": 101,
        ///                 "read": true,
        ///                 "add": false,
        ///                 "update": true,
        ///                 "remove": false
        ///             },
        ///             {
        ///                 "id": 102,
        ///                 "read": true,
        ///                 "add": true,
        ///                 "update": false,
        ///                 "remove": false
        ///             }
        ///         ],
        ///         "createdBy": 2
        ///     }
        ///
        /// This endpoint adds a list of authorizations to the specified role with the given permissions.
        /// </remarks>
        /// <param name="payload">The details for adding authorizations, including the role ID, list of authorizations, and creator ID.</param>
        /// <response code="201">The authorizations were added successfully, and the ID of the new authorization record is returned.</response>
        /// <response code="400">The request data is invalid.</response>
        /// <response code="404">The specified role was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("AddAuthorizations")]
        public async Task<ActionResult<GenericHandlerResponse<long>>> AddAuthorizations([FromBody] AddAuthorizationDTO payload)
        {
            var result = await _unitOfWork.RolesRepository.AddAuthorizations(payload);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Removes authorizations from a specified role.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Roles/RemoveAuthorizations
        ///     {
        ///         "roleId": 1,
        ///         "authsId": [
        ///             101,
        ///             102
        ///         ]
        ///     }
        ///
        /// This endpoint removes a list of authorizations from the specified role.
        /// </remarks>
        /// <param name="payload">The details for removing authorizations, including the role ID and a list of authorization IDs to remove.</param>
        /// <response code="200">The authorizations were removed successfully.</response>
        /// <response code="400">The request data is invalid.</response>
        /// <response code="404">The specified role or authorizations were not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        //[HttpPost("RemoveAuthorizations")]
        //public async Task<ActionResult<GenericHandlerResponse<long>>> RemoveAuthorizations([FromBody] RemoveAuthorizationDTO payload)
        //{
        //    var result = await _unitOfWork.RolesRepository.RemoveAuthorizations(payload);
        //    return StatusCode(result.statusCode, result);
        //}

    }
}
