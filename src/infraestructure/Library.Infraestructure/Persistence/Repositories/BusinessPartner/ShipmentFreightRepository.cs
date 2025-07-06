using AutoMapper;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ShipmentFreight.Create;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Shipment
{
    public class ShipmentFreightRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ShipmentFreightRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<long?>> Create(ShipmentFreightCreateDto payload, long userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
           
            var contract = await _context.ShipmentProjectContracts
                .AsNoTracking()
                .AnyAsync(x => x.Id == payload.ShipmentProjectContractId && x.IsActive);

            if (!contract)
                return new GenericResponseHandler<long?>(404, null, message: "El contrato de proyecto especificado no existe o no está activo");

            var vehicle = await _context.BusinessPartnerProviderTransportVehicles
                .AsNoTracking()
                .AnyAsync(x => x.Id == payload.BusinessPartnerProviderTransportVehicleId && x.IsActive);

            if (!vehicle)
                return new GenericResponseHandler<long?>(404, null, message: "El vehículo de transporte especificado no existe o no está activo");

            var model = _mapper.Map<ShipmentFreight>(payload);
            model.ShipmentFreightStatusId = 1;
            model.CreatedBy = userId;

            await _context.ShipmentFreights.AddAsync(model);
            await _context.SaveChangesAsync();

            var statusLog = new ShipmentFreightStatusLog
            {
                ShipmentFreightId = model.Id,
                ShipmentFreightStatusId = model.ShipmentFreightStatusId,
                Comments = "Flete creado",
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            await _context.ShipmentFreightStatusLogs.AddAsync(statusLog);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new GenericResponseHandler<long?>(201, model.Id);
      
        }
    }
}