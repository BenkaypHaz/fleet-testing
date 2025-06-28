using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.BusinessPartner
{
    public class ProviderProfileRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ProviderProfileRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<ProviderProfileReadDto>>> GetBySearch(string? searchValue)
        {
          
                var query = _context.BusinessPartnerProviderProfiles
                    .Where(x => x.IsActive)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchLower = searchValue.ToLower();
                    query = query.Where(x =>
                        x.BusinessName.ToLower().Contains(searchLower) ||
                        x.BusinessName.ToLower().Equals(searchLower) ||
                        x.LegalId.ToLower().StartsWith(searchLower) ||
                        x.LegalId.ToLower().Equals(searchLower));
                }

                var data = await query
                    .OrderBy(x => x.BusinessName)
                    .Take(20)
                    .ProjectTo<ProviderProfileReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<ProviderProfileReadDto>>(200, data, data.Count);
         
        }
    }
}