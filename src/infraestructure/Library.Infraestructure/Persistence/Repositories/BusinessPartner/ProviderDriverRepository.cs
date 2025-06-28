using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.BusinessPartner
{
    public class ProviderDriverRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ProviderDriverRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<ProviderDriverReadDto>>> Get(FilterOptionsDto filterOptions)
        {
            var query = _context.BusinessPartnerProviderDrivers
                .Where(x => x.IsActive)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(filterOptions.searchValue))
            {
                var searchLower = filterOptions.searchValue.ToLower();
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(searchLower) ||
                    x.FirstName.ToLower().Equals(searchLower) ||
                    x.LastName.ToLower().Contains(searchLower) ||
                    x.LastName.ToLower().Equals(searchLower) ||
                    (x.FirstName + " " + x.LastName).ToLower().Contains(searchLower) ||
                    (x.NationalId != null && x.NationalId.ToLower().StartsWith(searchLower)) ||
                    (x.NationalId != null && x.NationalId.ToLower().Equals(searchLower)));
            }
            if (filterOptions.enablePagination)
                query = query.Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                .Take(filterOptions.recordsPerPage);

            var data = await query
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ProjectTo<ProviderDriverReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            return new GenericResponseHandler<List<ProviderDriverReadDto>>(200, data, data.Count);
        }
    }
}