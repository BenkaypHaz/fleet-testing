using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleBrand.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.BusinessPartner
{
    public class VehicleBrandRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public VehicleBrandRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<VehicleBrandReadDto>>> GetBySearch(string? searchValue)
        {

          
                var query = _context.BusinessPartnerVehicleBrand
                    .Where(x => x.IsActive)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchLower = searchValue.ToLower();
                    query = query.Where(x =>
                        x.Name.ToLower().Contains(searchLower) ||
                        x.Name.ToLower().Equals(searchLower));
                }

                var data = await query
                    .OrderBy(x => x.Name)
                    .Take(20) // Limitar resultados para mejor performance
                    .ProjectTo<VehicleBrandReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<VehicleBrandReadDto>>(200, data, data.Count);
        

        }
    }
}