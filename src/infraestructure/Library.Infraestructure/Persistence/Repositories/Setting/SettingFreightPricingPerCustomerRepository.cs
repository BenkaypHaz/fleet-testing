using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.FreightPricing.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.Repositories.Setting
{
    public class SettingFreightPricingPerCustomerRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public SettingFreightPricingPerCustomerRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<FreightPricingSuggestedPriceReadDto>> GetSuggestedPrice(long customerId, long warehouseId, long dispatchBranchId)
        {
            // Verificar que el warehouse pertenece al customer
            var warehouseExists = await _context.CustomerWarehouses
                .Where(w => w.Id == warehouseId &&
                           w.CustomerProfileId == customerId &&
                           w.IsActive)
                .AnyAsync();

            if (!warehouseExists)
            {
                return new GenericResponseHandler<FreightPricingSuggestedPriceReadDto>(
                    400,
                    null,
                    0,
                    "El warehouse especificado no pertenece al customer o no está activo");
            }

            var query = _context.SettingFreightPricingPerCustomers
                .Include(p => p.CustomerWarehouse)
                    .ThenInclude(w => w.CustomerProfile)
                .Include(p => p.SettingDispatchBranch)
                .Where(p => p.CustomerWarehouse.CustomerProfileId == customerId &&
                           p.CustomerWarehouseId == warehouseId &&
                           p.SettingDispatchBranchId == dispatchBranchId &&
                           p.IsActive &&
                           p.CustomerWarehouse.IsActive &&
                           p.CustomerWarehouse.CustomerProfile.IsActive &&
                           p.SettingDispatchBranch.IsActive)
                .AsNoTracking();

            var data = await query
                .ProjectTo<FreightPricingSuggestedPriceReadDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();


            return new GenericResponseHandler<FreightPricingSuggestedPriceReadDto>(200, data, 1);
        }
    }
}