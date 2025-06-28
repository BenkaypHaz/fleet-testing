using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
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

        public async Task<GenericResponseHandler<List<ProviderDriverReadDto>>> GetBySearch(string? searchValue)
        {
          
                var query = _context.BusinessPartnerProviderDrivers
                    .Where(x => x.IsActive)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchLower = searchValue.ToLower();
                    query = query.Where(x =>
                        x.FirstName.ToLower().Contains(searchLower) ||
                        x.FirstName.ToLower().Equals(searchLower) ||
                        x.LastName.ToLower().Contains(searchLower) ||
                        x.LastName.ToLower().Equals(searchLower) ||
                        (x.FirstName + " " + x.LastName).ToLower().Contains(searchLower) ||
                        (x.NationalId != null && x.NationalId.ToLower().StartsWith(searchLower)) ||
                        (x.NationalId != null && x.NationalId.ToLower().Equals(searchLower)));
                }

                var data = await query
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .Take(20)
                    .ProjectTo<ProviderDriverReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<ProviderDriverReadDto>>(200, data, data.Count);
         
        }
    }
}