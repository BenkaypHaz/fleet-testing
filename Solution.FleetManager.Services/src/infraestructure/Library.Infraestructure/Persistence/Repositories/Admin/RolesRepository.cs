using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.Admin.Users.Read;
using Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read;
using Library.Infraestructure.Persistence.DTOs.Helpers;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Library.Infraestructure.Persistence.Repositories.Admin
{
    public class RolesRepository
    {
        private readonly DataBaseContext _context;
        public RolesRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericHandlerResponse<List<RolesReadDTO>>> Get(PaginationDTO paginationDTO)
        {
            var data = new List<RolesReadDTO>();
            try
            {
                var query = _context.AuthRoles.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(paginationDTO.SearchValue))
                {
                    string[] searchValues = paginationDTO.SearchValue.Split(" ");
                    foreach (var value in searchValues)
                    {
                        query = query.Where(a =>
                            a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                            a.Description.Equals(value) || a.Description.Contains(value));
                    }
                }

                var totalRecords = await query.CountAsync();
                data = await query
                    .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                    .Take(paginationDTO.RecordsPerPage)
                    .OrderBy(c => c.Description)
                    .Select(c => new RolesReadDTO
                    {
                        Id = c.Id,
                        Description = c.Description,
                        CreatedBy = c.CreatedBy,
                        CreatedDate = c.CreatedDate,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedDate = c.ModifiedDate,
                        IsActive = c.IsActive,
                    }).ToListAsync();

                // Retorna 200 OK con la lista paginada
                return new GenericHandlerResponse<List<RolesReadDTO>>(200, data: data, totalRecords);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<List<RolesReadDTO>>(500, ExceptionMessage: ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<RolesReadFirstDTO>> GetById(long id)
        {
            try
            {
                #region 1. Obtener informacion del rol
                var rol = await _context.AuthRoles
                    .Where(role => role.Id == id)
                     .Join(_context.AuthUsers,
                           role => role.CreatedBy,
                           user => user.Id,
                           (role, createdBy) => new { role, createdBy })
                     .GroupJoin(_context.AuthUsers,
                                combined => combined.role.ModifiedBy,
                                user => user.Id,
                                (combined, modifierGroup) => new { combined.role, combined.createdBy, modifierGroup })
                     .SelectMany(x => x.modifierGroup.DefaultIfEmpty(),
                                 (x, modifier) => new RolesReadFirstDTO
                                 {
                                     Id = x.role.Id,
                                     Description = x.role.Description,
                                     CreatedBy = x.role.CreatedBy,
                                     CreatedByName = x.createdBy.UserName,
                                     CreatedDate = x.role.CreatedDate,
                                     ModifiedBy = x.role.ModifiedBy,
                                     ModifiedByName = modifier != null ? modifier.UserName : null,
                                     ModifiedDate = x.role.ModifiedDate,
                                     IsActive = x.role.IsActive,
                                 })
                     .FirstOrDefaultAsync();
                #endregion

                if (rol == null)
                    return new GenericHandlerResponse<RolesReadFirstDTO>(404, CustomMessage: "No se encontró información del rol solicitado.");

                #region 2. Obtener Authorizations del rol
                var authorizations = await _context.AuthRoleAuthorizations
                    .Where(role => role.RoleId == id)
                    .GroupJoin(
                        _context.AuthAuthorizations,
                        authorizationRole => authorizationRole.AuthId,
                        authorization => authorization.Id,
                        (authorizationRole, authGroup) => new { authorizationRole, authGroup }
                    )
                    .SelectMany(
                        auth => auth.authGroup.DefaultIfEmpty(),
                        (auth, authorization) => new AuthorizationsReadDTO
                        {
                            Id = authorization != null ? authorization.Id : 0,
                            Description = authorization.Description,
                            RouteValue = authorization.RouteValue ?? string.Empty,
                            CreatedBy = authorization.CreatedBy,
                            CreatedDate = authorization.CreatedDate,
                            ModifiedBy = authorization.ModifiedBy,
                            ModifiedDate = authorization.ModifiedDate,
                            IsActive = authorization.IsActive
                        }
                    )
                    .ToListAsync();


                rol.Authorizations = authorizations;

                #endregion

                // Retorna 200 OK con la lista paginada
                return new GenericHandlerResponse<RolesReadFirstDTO>(200, data: rol);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<RolesReadFirstDTO>(500, ExceptionMessage: ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<long>> Create(RolesCreateDTO payload,long userId)
        {
            try
            {
                var model = new AuthRole
                {
                    Description = payload.Description,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true
                };

                // Agregar el modelo al contexto
                await _context.AuthRoles.AddAsync(model);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                return new GenericHandlerResponse<long>(200, CustomMessage: "¡Rol creado con éxito!", data: model.Id);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<long>(500, ExceptionMessage: ex.Message);
            }
        }

        //public async Task<GenericHandlerResponse<long>> Update(RolesUpdateDTO payload)
        //{
        //    try
        //    {

        //        var roleData = await _context.Roles.FindAsync(payload.Id);

        //        if (roleData == null)
        //            return new GenericHandlerResponse<long>(404, CustomMessage: "No se encontró información del rol solicitado.");


        //        roleData.Description = payload.Description;
        //        roleData.ModifiedBy = payload.ModifiedBy;
        //        roleData.ModifiedDate = DateTime.UtcNow;
        //        roleData.IsActive = payload.IsActive;

        //        await _context.SaveChangesAsync();


        //        return new GenericHandlerResponse<long>(200, CustomMessage: "¡Rol actualizado con éxito!", data: payload.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        await BaseHelper.SaveErrorLog(ex);
        //        return new GenericHandlerResponse<long>(500, ExceptionMessage: ex.Message);
        //    }
        //}

        public async Task<GenericHandlerResponse<long>> AddAuthorizations(AddAuthorizationDTO payload)
        {
            try
            {
                var roleData = await _context.AuthRoles.FindAsync(payload.RoleId);

                if (roleData == null)
                    return new GenericHandlerResponse<long>(404, CustomMessage: "No se encontró información del rol solicitado.");

                if (payload.Auths == null)
                    return new GenericHandlerResponse<long>(201, data: payload.RoleId);

                var model = payload.Auths.Select(auth => new AuthRoleAuthorization
                {
                    RoleId = payload.RoleId,
                    AuthId = auth.Id,
                    Read = auth.Read,
                    Create = auth.Create,
                    Update = auth.Update,
                    Delete = auth.Delete,
                    CreatedBy = payload.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true,
                });

                await _context.AuthRoleAuthorizations.AddRangeAsync(model);
                await _context.SaveChangesAsync();

                return new GenericHandlerResponse<long>(201, data: payload.RoleId);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericHandlerResponse<long>(500, ExceptionMessage: ex.Message);
            }
        }

        //public async Task<GenericHandlerResponse<long>> RemoveAuthorizations(RemoveAuthorizationDTO payload)
        //{
        //    try
        //    {
        //        var roleData = await _context.Roles.FindAsync(payload.RoleId);

        //        if (roleData == null)
        //            return new GenericHandlerResponse<long>(404, CustomMessage: "No se encontró información del rol solicitado.");

        //        // Buscar las authorizaciones registradas para el rol
        //        var existingAuthorizationsRoles = await _context.AuthorizationsRoles
        //            .AsNoTracking()
        //            .Where(auth => auth.RoleId == payload.RoleId)
        //            .ToListAsync();

        //        if (payload.AuthsId == null)
        //            return new GenericHandlerResponse<long>(200, data: payload.RoleId);

        //        // Eliminar las authorizaciones que ya no están en la lista
        //        var authorizationToRemove = existingAuthorizationsRoles
        //            .Where(auth => payload.AuthsId.Contains(auth.AuthId))
        //            .ToList();

        //        _context.AuthorizationsRoles.RemoveRange(authorizationToRemove);
        //        await _context.SaveChangesAsync();

        //        return new GenericHandlerResponse<long>(200, data: payload.RoleId);
        //    }
        //    catch (Exception ex)
        //    {
        //        await BaseHelper.SaveErrorLog(ex);
        //        return new GenericHandlerResponse<long>(500, ExceptionMessage: ex.Message);
        //    }
        //}

    }
}
