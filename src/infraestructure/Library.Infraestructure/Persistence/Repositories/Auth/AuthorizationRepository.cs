using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Auth
{
    public class AuthorizationRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public AuthorizationRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<ModuleReadDto>>> Get()
        {
            try
            {
                var data = await _context.AuthModules
                    .Include(c => c.AuthAuthorizations)
                    .AsNoTracking()
                    .ProjectTo<ModuleReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<ModuleReadDto>>(200, data, data.Count);
            }
            catch (Exception ex)
            {
                await BaseHelper.SaveErrorLog(ex);
                return new GenericResponseHandler<List<ModuleReadDto>>(500, null, exceptionMessage: ex.Message);
            }
        }
    }
}
