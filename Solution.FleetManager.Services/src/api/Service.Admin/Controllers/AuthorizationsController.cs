using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Create;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Update;
using Library.Infraestructure.Persistence.DTOs.Admin.Modules.Read;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "AuthorizationsController")]
    [Route("Api/[controller]")]
    public class AuthorizationsController : BaseControllerUsers
    {
        public AuthorizationsController(ILogger<BaseControllerUsers> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, unitOfWork, httpContextAccessor) { }

        /// <summary>Returns an "Ok" to check the correct functioning of the controller.</summary>
        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "Ok!";
        }


        /// <summary>
        /// Retrieves a list of all modules with their associated authorizations.
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/modules/GetAll
        /// 
        /// Response example:
        /// 
        ///     {
        ///         "statusCode": 200,
        ///         "data": [
        ///             {
        ///                 "Id": 1,
        ///                 "Name": "Module 1",
        ///                 "Authorizations": [
        ///                     {
        ///                         "Id": 101,
        ///                         "Description": "Authorization 1",
        ///                         "RouteValue": "/route/1",
        ///                         "ModuleId": 1,
        ///                         "CreatedBy": 1001,
        ///                         "CreatedByName": "Admin",
        ///                         "CreatedDate": "2023-12-01T10:00:00",
        ///                         "ModifiedBy": 1002,
        ///                         "ModifiedByName": "Editor",
        ///                         "ModifiedDate": "2023-12-05T12:00:00",
        ///                         "IsActive": true
        ///                     },
        ///                     {
        ///                         "Id": 102,
        ///                         "Description": "Authorization 2",
        ///                         "RouteValue": "/route/2",
        ///                         "ModuleId": 1,
        ///                         "CreatedBy": 1001,
        ///                         "CreatedByName": "Admin",
        ///                         "CreatedDate": "2023-12-01T10:00:00",
        ///                         "ModifiedBy": 1002,
        ///                         "ModifiedByName": "Editor",
        ///                         "ModifiedDate": "2023-12-05T12:00:00",
        ///                         "IsActive": true
        ///                     }
        ///                 ],
        ///                 "CreatedBy": 1001,
        ///                 "CreatedByName": "Admin",
        ///                 "CreatedDate": "2023-12-01T10:00:00",
        ///                 "ModifiedBy": 1002,
        ///                 "ModifiedByName": "Editor",
        ///                 "ModifiedDate": "2023-12-05T12:00:00",
        ///                 "IsActive": true
        ///             },
        ///             ...
        ///         ],
        ///         "message": "Request processed successfully."
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved the list of modules with their authorizations.</response>
        /// <response code="400">Bad request due to invalid query parameters or other user input issues.</response>
        /// <response code="500">Internal server error, unexpected issues while processing the request.</response>

        [HttpGet("GetAll")]
        public async Task<ActionResult<GenericHandlerResponse<List<ModulesReadDTO>>>> Get()
        {
            var result = await _unitOfWork.AuthorizationsRepository.Get();
            return StatusCode(result.statusCode, result);
        }

    }
}
