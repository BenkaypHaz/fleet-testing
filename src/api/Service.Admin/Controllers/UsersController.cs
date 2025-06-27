using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Create;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Update;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "UsersController")]
    [Route("Api/[controller]")]
    public class UsersController : BaseControllerUsers
    {
        public UsersController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }



        /// <summary>Returns an "Ok" to check the correct functioning of the controller.</summary>
        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "Ok!";
        }

        /// <summary>
        /// Retrieves a paginated list of users from the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/Users/GetAll?page=1?recordsPerPage=10
        ///
        /// This endpoint returns a list of users based on the specified pagination parameters.
        /// </remarks>
        /// <param name="paginationDTO">The pagination details including the page number and number of records per page.</param>
        /// <response code="200">A list of users retrieved successfully.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAll")]
        public async Task<ActionResult<GenericHandlerResponse<List<UserReadDTO>>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var result = await _unitOfWork.UsersRepository.Get(paginationDTO);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Retrieves the user information by the specified user ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/Users/GetById/1
        ///
        /// This endpoint returns the details of a user based on the provided ID.
        /// </remarks>
        /// <param name="Id">The unique identifier of the user.</param>
        /// <response code="200">User information retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<GenericHandlerResponse<UserReadDTO>>> GetById(long Id)
        {
            var result = await _unitOfWork.UsersRepository.GetById(Id);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Users/Add
        ///     {
        ///         "FirstName": "Malcom",
        ///         "LastName": "Medina",
        ///         "Email": "mmedina@gmail.com",
        ///         "Dni": "05012002748",
        ///         "PhoneNumber": "00000000",
        ///         "ProfilePicture": null,
        ///         "BirthDate": "2002-03-15",
        ///         "Gender": "M",
        ///         "Password": "yourSecurePassword123",
        ///         "Roles": [1, 2],
        ///         "CreatedBy": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="Payload">The user registration details.</param>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("Add")]
        public async Task<ActionResult<GenericHandlerResponse<long>>> Post([FromBody] UsersCreateDTO Payload)
        {
            var result = await _unitOfWork.UsersRepository.Create(Payload, _userId);
            return StatusCode(result.statusCode, result);
        }

        /// <summary>
        /// Updates an existing user's details in the system.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Users/Update
        ///     {
        ///         "Id": 1,
        ///         "FirstName": "Malcom",
        ///         "LastName": "Medina",
        ///         "Email": "mmedina@gmail.com",
        ///         "Dni": "05012002748",
        ///         "PhoneNumber": "00000000",
        ///         "ProfilePicture": null,
        ///         "BirthDate": "2002-03-15",
        ///         "Gender": "M",
        ///         "Roles": [1, 2],
        ///         "ModifiedBy": 1,
        ///         "IsActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="Payload">The user update details.</param>
        /// <response code="200">User updated successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">An internal server error occurred.</response>

        [HttpPut("Update")]
        public async Task<ActionResult<GenericHandlerResponse<long>>> Put([FromBody] UsersUpdateDTO Payload)
        {
            var result = await _unitOfWork.UsersRepository.Update(Payload, _userId);
            return StatusCode(result.statusCode, result);
        }


    }
}
