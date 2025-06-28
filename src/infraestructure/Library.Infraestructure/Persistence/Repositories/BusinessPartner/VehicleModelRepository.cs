using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
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

        public async Task<GenericResponseHandler<List<VehicleModelReadDto>>> Get(long? brandId, FilterOptionsDto pagination)
        {
         
            var query = _context.BusinessPartnerVehicleModel
                .Where(x => x.IsActive)
                .AsNoTracking();

            if (brandId.HasValue && brandId.Value > 0)
                query = query.Where(x => x.BrandId == brandId.Value);

            if (!string.IsNullOrEmpty(pagination.searchValue))
            {
                var searchLower = pagination.searchValue.ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(searchLower) ||
                    x.Name.ToLower().Equals(searchLower));
            }

            if (pagination.enablePagination)
                query = query.Skip((pagination.page - 1) * pagination.recordsPerPage)
                .Take(pagination.recordsPerPage);

            var data = await query
                    .OrderBy(x => x.Name)
                    .ProjectTo<VehicleModelReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            return new GenericResponseHandler<List<VehicleModelReadDto>>(200, data, data.Count);
        }
    }
}