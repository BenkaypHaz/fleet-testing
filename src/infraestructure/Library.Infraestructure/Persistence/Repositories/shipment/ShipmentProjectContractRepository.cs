using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.General.Country.Read;
using Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentProjectContract.Read;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.Repositories.shipment
{
    public class ShipmentProjectContractRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public ShipmentProjectContractRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GenericResponseHandler<List<ShipmentProjectContractReadDto>>> Get()
        {

            var data = await _context.ShipmentProjectContracts
                       .AsNoTracking()
                       .OrderByDescending(a => a.CreatedDate) 
                       .ProjectTo<ShipmentProjectContractReadDto>(_mapper.ConfigurationProvider)
                       .ToListAsync();

            return new GenericResponseHandler<List<ShipmentProjectContractReadDto>>(200, data, data.Count());



        }
    }
}
