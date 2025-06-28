using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Customer
{
    public class CustomerWarehouseRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public CustomerWarehouseRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<CustomerWarehouseReadDto>>> Get(FilterOptionsDto filterOptions)
        {         
            var query = _context.CustomerWarehouses
                .Where(x => x.IsActive)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(filterOptions.searchValue))
            {
                var searchLower = filterOptions.searchValue.ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(searchLower) ||
                    x.Name.ToLower().Equals(searchLower) ||
                    x.Code.ToString().StartsWith(filterOptions.searchValue) ||
                    x.Code.ToString().Equals(filterOptions.searchValue));
            }

            if (filterOptions.enablePagination)
                query = query.Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                .Take(filterOptions.recordsPerPage);

            var data = await query
                .OrderBy(x => x.Name)
                .ProjectTo<CustomerWarehouseReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GenericResponseHandler<List<CustomerWarehouseReadDto>>(200, data, data.Count);
        }
    }
}