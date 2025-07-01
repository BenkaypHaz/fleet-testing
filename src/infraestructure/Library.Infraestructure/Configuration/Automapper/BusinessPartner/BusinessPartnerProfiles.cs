using Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.ShipmentFreight.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Create;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleBrand.Read;
using Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.BusinessPartner
{
    public class BusinessPartnerProfiles : AutoMapper.Profile
    {
        public BusinessPartnerProfiles()
        {
            #region Vehicle Brand
            CreateMap<BusinessPartnerVehicleBrand, VehicleBrandReadDto>();
            #endregion
            #region Vehicle Model
            CreateMap<BusinessPartnerVehicleModel, VehicleModelReadDto>();
            #endregion

            #region Provider Profile
            CreateMap<BusinessPartnerProviderProfile, ProviderProfileReadDto>();
            #endregion

            #region Provider Driver
            CreateMap<BusinessPartnerProviderDriver, ProviderDriverReadDto>();
            #endregion

            #region Transport Vehicle
            CreateMap<BusinessPartnerProviderTransportVehicle, TransportVehicleReadDto>();
            CreateMap<TransportVehicleCreateDto, BusinessPartnerProviderTransportVehicle>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));
            #endregion

            #region Customer Warehouse
            CreateMap<CustomerWarehouse, CustomerWarehouseReadDto>();
            #endregion

            #region Shipment Freight
            CreateMap<ShipmentFreightCreateDto, ShipmentFreight>()
                   .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                   .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));
            #endregion
        }
    }
}