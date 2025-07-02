using Library.Infraestructure.Persistence.Repositories.Auth;
using Library.Infraestructure.Persistence.Repositories.BusinessPartner;
using Library.Infraestructure.Persistence.Repositories.Customer;
using Library.Infraestructure.Persistence.Repositories.General;
using Library.Infraestructure.Persistence.Repositories.shipment;
using Library.Infraestructure.Persistence.Repositories.Shipment;


namespace Library.Infraestructure.Persistence.UnitOfWorks
{
    public interface IUnitOfWork
    {
        #region Service.Admin

        LoginRepository LoginRepository { get; }
        UserRepository UserRepository { get; }
        RoleRepository RoleRepository { get; }
        AuthorizationRepository AuthorizationRepository { get; }

        CountryRepository CountryRepository { get; }
        RegionRepository RegionRepository { get; }
        CityRepository CityRepository { get; }

        #endregion

        #region BusinessPartner
        VehicleBrandRepository VehicleBrandRepository { get; }
        VehicleModelRepository VehicleModelRepository { get; }
        ProviderProfileRepository ProviderProfileRepository { get; }
        ProviderDriverRepository ProviderDriverRepository { get; }
        ProviderTransportVehicleRepository TransportVehicleRepository { get; }
        #endregion

        #region Customer
        CustomerWarehouseRepository CustomerWarehouseRepository { get; }
        #endregion


        #region Shipment
        ShipmentFreightRepository ShipmentFreightRepository { get; }
        ShipmentProjectContractRepository ShipmentProjectContractRepository { get; }
        ShipmentFreightTypeRepository ShipmentFreightTypeRepository { get; }
        #endregion
    }
}
