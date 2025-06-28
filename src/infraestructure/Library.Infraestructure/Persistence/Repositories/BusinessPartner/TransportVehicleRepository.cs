using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.BusinessPartner
{
    public class TransportVehicleRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public TransportVehicleRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<TransportVehicleReadDto>>> GetBySearch(string? searchValue)
        {
         
                var query = _context.BusinessPartnerProviderTransportVehicles
                    .Where(x => x.IsActive)
                    .AsNoTracking();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchLower = searchValue.ToLower();
                    query = query.Where(x =>
                        x.PlateNumber.ToLower().Contains(searchLower) ||
                        x.PlateNumber.ToLower().Equals(searchLower) ||
                        x.Brand.ToLower().Contains(searchLower) ||
                        x.Model.ToLower().Contains(searchLower));
                }

                var data = await query
                    .OrderBy(x => x.PlateNumber)
                    .Take(20)
                    .ProjectTo<TransportVehicleReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<TransportVehicleReadDto>>(200, data, data.Count);
         
        }

        public async Task<GenericResponseHandler<long?>> Create(TransportVehicleCreateDto payload, long userId)
        {
           
                var existingVehicle = await _context.BusinessPartnerProviderTransportVehicles
                    .FirstOrDefaultAsync(x => x.PlateNumber.ToLower() == payload.PlateNumber.ToLower());

                if (existingVehicle != null)
                {
                    return new GenericResponseHandler<long?>(400,null,
                        message: $"Ya existe un vehículo registrado con la placa {payload.PlateNumber}");
                }

                var model = _mapper.Map<BusinessPartnerProviderTransportVehicle>(payload);
                model.CreatedBy = userId;
                model.CreatedDate = DateTime.UtcNow;
                model.IsActive = true;

                await _context.BusinessPartnerProviderTransportVehicles.AddAsync(model);
                await _context.SaveChangesAsync();


                return new GenericResponseHandler<long?>(201, model.Id);
        
        }
    }
}