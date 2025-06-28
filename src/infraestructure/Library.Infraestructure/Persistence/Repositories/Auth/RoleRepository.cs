using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Auth
{
    public class RoleRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public RoleRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<RoleReadDTO>>> Get(FilterOptionsDto filterOptions)
        {
            var query = _context.AuthRoles
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterOptions.searchValue))
            {
                string[] searchValues = filterOptions.searchValue.Split(" ");
                foreach (var value in searchValues)
                {
                    query = query.Where(a =>
                        a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                        a.Description.Equals(value) || a.Description.Contains(value));
                }
            }

            var totalRecords = await query.CountAsync();
            var data = await query
                .Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                .Take(filterOptions.recordsPerPage)
                .ProjectTo<RoleReadDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GenericResponseHandler<List<RoleReadDTO>>(200, data, totalRecords);
        }

        public async Task<GenericResponseHandler<RoleReadFirstDto>> GetById(long id)
        {
            var data = await _context.AuthRoles
                .Include(c => c.AuthRoleAuthorizations)
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<RoleReadFirstDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (data == null)
                return new GenericResponseHandler<RoleReadFirstDto>(404, null);
            var dataRecords = data != null ? 1 : 0;

            return new GenericResponseHandler<RoleReadFirstDto>(200, data, dataRecords);
        }

        public async Task<GenericResponseHandler<long?>> Create(RoleCreateDto payload, long userId)
        {
            var model = _mapper.Map<AuthRole>(payload);
            model.CreatedBy = userId;
            await _context.AuthRoles.AddAsync(model);

            var modelAuthorizations = payload.Auth_Id.Select(auth => new AuthRoleAuthorization
            {
                RoleId = model.Id,
                AuthId = auth,
                CreatedBy = userId,
                CreatedDate = model.CreatedDate,
                ModifiedBy = null,
                ModifiedDate = null,
                IsActive = true,
            });
            await _context.AuthRoleAuthorizations.AddRangeAsync(modelAuthorizations);
            await _context.SaveChangesAsync();

            return new GenericResponseHandler<long?>(201, model.Id);
        }

        public async Task<GenericResponseHandler<long?>> Update(long id, RoleUpdateDto payload, long userId)
        {
            var role = await _context.AuthRoles.FindAsync(id);
            if (role == null)
                return new GenericResponseHandler<long?>(404, null);

            role.Description = payload.Description;
            role.ModifiedBy = userId;
            role.ModifiedDate = DateTime.UtcNow;
            role.IsActive = payload.IsActive;

            var existingAuthorizationsRoles = await _context.AuthRoleAuthorizations
                .Where(auth => auth.RoleId == id)
                .ToListAsync();

            var authorizationToRemove = existingAuthorizationsRoles
                .Where(auth => !payload.Auth_Id.Contains(auth.AuthId))
                .ToList();

            if (authorizationToRemove.Any())
                _context.AuthRoleAuthorizations.RemoveRange(authorizationToRemove);

            var existingAuthIds = existingAuthorizationsRoles.Select(auth => auth.AuthId).ToHashSet();
            var modelAuthorizations = payload.Auth_Id
                .Where(authId => !existingAuthIds.Contains(authId))
                .Select(authId => new AuthRoleAuthorization
                {
                    RoleId = role.Id,
                    AuthId = authId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true,
                }).ToList();

            if (modelAuthorizations.Any())
                await _context.AuthRoleAuthorizations.AddRangeAsync(modelAuthorizations);
            await _context.SaveChangesAsync();

            return new GenericResponseHandler<long?>(200, id);
        }

    }
}
