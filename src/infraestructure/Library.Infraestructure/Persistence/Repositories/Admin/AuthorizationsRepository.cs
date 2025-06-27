using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Create;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Update;
using Library.Infraestructure.Persistence.DTOs.Admin.Modules.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Infraestructure.Persistence.Repositories.Admin
{
    public class AuthorizationsRepository
    {
        private readonly DataBaseContext _context;
        public AuthorizationsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericHandlerResponse<List<ModulesReadDTO>>> Get()
        {

                //obtener todos los modulos
                var modules = await _context.AuthModules
                    .Include(module => module.CreatedByNavigation)
                    .Include(module => module.ModifiedByNavigation)
                    .Select(module => new ModulesReadDTO
                    {
                        Id = module.Id,
                        Name = module.Name,
                        Authorizations = null,
                        CreatedBy = module.CreatedBy,
                        CreatedByName = module.CreatedByNavigation != null ? module.CreatedByNavigation.UserName : "",
                        CreatedDate = module.CreatedDate,
                        ModifiedBy = module.ModifiedBy,
                        ModifiedByName = module.ModifiedByNavigation != null ? module.ModifiedByNavigation.UserName : "",
                        ModifiedDate = module.ModifiedDate,
                        IsActive = module.IsActive
                    })
                    .OrderByDescending(x => x.CreatedDate)
                    .ToListAsync();


                //obtener todas las autorizaciones
                var authorizations = await _context.AuthAuthorizations
                    .Include(module => module.CreatedByNavigation)
                    .Include(module => module.ModifiedByNavigation)
                    .Select(authorization => new AuthorizationsReadDTO
                    {
                        Id = authorization.Id,
                        Description = authorization.Description,
                        RouteValue = authorization.RouteValue,
                        //ModuleId = authorization.ModuleId,
                        CreatedBy = authorization.CreatedBy,
                        CreatedByName = authorization.CreatedByNavigation != null ? authorization.CreatedByNavigation.UserName : "",
                        CreatedDate = authorization.CreatedDate,
                        ModifiedBy = authorization.ModifiedBy,
                        ModifiedByName = authorization.ModifiedByNavigation != null ? authorization.ModifiedByNavigation.UserName : "",
                        ModifiedDate = authorization.ModifiedDate,
                        IsActive = authorization.IsActive
                    }).ToListAsync();

                //agregar autorizaciones a cada modulo
                var data = modules.Select(module => new ModulesReadDTO
                {
                    Id = module.Id,
                    Name = module.Name,
                    Authorizations = authorizations
                    .Where(auth => auth.ModuleId == module.Id)
                    .Select(auth => new AuthorizationsReadDTO
                    {
                        Id = auth.Id,
                        Description = auth.Description,
                        RouteValue = auth.RouteValue,
                        ModuleId = auth.ModuleId,
                        CreatedBy = auth.CreatedBy,
                        CreatedByName = auth.CreatedByName,
                        CreatedDate = auth.CreatedDate,
                        ModifiedBy = auth.ModifiedBy,
                        ModifiedByName = auth.ModifiedByName,
                        ModifiedDate = auth.ModifiedDate,
                        IsActive = module.IsActive
                    })
                    .ToList(),
                    CreatedBy = module.CreatedBy,
                    CreatedByName = module.CreatedByName,
                    CreatedDate = module.CreatedDate,
                    ModifiedBy = module.ModifiedBy,
                    ModifiedByName = module.ModifiedByName,
                    ModifiedDate = module.ModifiedDate,
                    IsActive = module.IsActive
                }).ToList();

                var allRegistersCount = await _context.AuthAuthorizations
                    .AsNoTracking()
                    .CountAsync();

                return new GenericHandlerResponse<List<ModulesReadDTO>>(200, data, allRegistersCount);

        }

        public async Task<GenericHandlerResponse<AuthorizationsReadDTO>> GetById(long authorizationId)
        {

                var data = await _context.AuthAuthorizations
                    .Where(x => x.Id == authorizationId)
                    .Include(authorization => authorization.CreatedByNavigation)
                    .Include(authorization => authorization.ModifiedByNavigation)
                    .Select(authorization => new AuthorizationsReadDTO
                    {
                        Id = authorization.Id,
                        Description = authorization.Description,
                        RouteValue = authorization.RouteValue,
                        CreatedBy = authorization.CreatedBy,
                        CreatedByName = authorization.CreatedByNavigation != null ? authorization.CreatedByNavigation.UserName : "",
                        CreatedDate = authorization.CreatedDate,
                        ModifiedBy = authorization.ModifiedBy,
                        ModifiedByName = authorization.ModifiedByNavigation != null ? authorization.ModifiedByNavigation.UserName : "",
                        ModifiedDate = authorization.ModifiedDate,
                        IsActive = authorization.IsActive
                    })
                    .FirstOrDefaultAsync();

                return new GenericHandlerResponse<AuthorizationsReadDTO>(200, data);

        }

    }
}
