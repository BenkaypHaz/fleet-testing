using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.BusinessPartner
{
    public class VehicleModelRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public VehicleModelRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<VehicleModelReadDto>>> GetByBrandAndSearch(long? brandId, string? searchValue)
        {
         
                var query = _context.BusinessPartnerVehicleModel
                    .Where(x => x.IsActive)
                    .AsNoTracking();

                if (brandId.HasValue && brandId.Value > 0)
                {
                    query = query.Where(x => x.BrandId == brandId.Value);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchLower = searchValue.ToLower();
                    query = query.Where(x =>
                        x.Name.ToLower().Contains(searchLower) ||
                        x.Name.ToLower().Equals(searchLower));
                }

                var data = await query
                    .OrderBy(x => x.Name)
                    .Take(20)
                    .ProjectTo<VehicleModelReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<VehicleModelReadDto>>(200, data, data.Count);

        }
    }
}