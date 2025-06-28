using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
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

        public async Task<GenericResponseHandler<List<ProviderProfileReadDto>>> Get(FilterOptionsDto filterOptions)
        {
            var query = _context.BusinessPartnerProviderProfiles
                .AsNoTracking()
                .Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(filterOptions.searchValue))
            {
                var searchLower = filterOptions.searchValue.ToLower();
                query = query.Where(x =>
                    x.BusinessName.ToLower().Contains(searchLower) ||
                    x.BusinessName.ToLower().Equals(searchLower) ||
                    x.LegalId.ToLower().StartsWith(searchLower) ||
                    x.LegalId.ToLower().Equals(searchLower));
            }

            if (filterOptions.enablePagination)
                query = query.Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                .Take(filterOptions.recordsPerPage);

            var data = await query
                .OrderBy(x => x.BusinessName)
                .ProjectTo<ProviderProfileReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GenericResponseHandler<List<ProviderProfileReadDto>>(200, data, data.Count);
        }

        public async Task<GenericResponseHandler<ProviderProfileReadDto>> GetById(long profileId)
        {
            var data = await _context.BusinessPartnerProviderProfiles
                .AsNoTracking()
                .Where(x => x.Id == profileId)
                .ProjectTo<ProviderProfileReadDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return new GenericResponseHandler<ProviderProfileReadDto>(200, data);
        }

    }
}