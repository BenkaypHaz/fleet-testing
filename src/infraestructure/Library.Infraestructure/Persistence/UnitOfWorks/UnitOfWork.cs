using AutoMapper;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Library.Infraestructure.Persistence.Repositories.Auth;
using Library.Infraestructure.Persistence.Repositories.BusinessPartner;
using Library.Infraestructure.Persistence.Repositories.Customer;
using Library.Infraestructure.Persistence.Repositories.General;
using Library.Infraestructure.Persistence.Repositories.Shipment;

namespace Library.Infraestructure.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Service.Admin

        private LoginRepository? _LoginRepository;
        private UserRepository? _UserRepository;
        private RoleRepository? _RoleRepository;
        private AuthorizationRepository? _AuthorizationRepository;

        private CountryRepository? _CountryRepository;
        private RegionRepository? _RegionRepository;
        private CityRepository? _CityRepository;



        public LoginRepository LoginRepository => _LoginRepository ??= new LoginRepository(_context);
        public UserRepository UserRepository => _UserRepository ??= new UserRepository(_context);
        public RoleRepository RoleRepository => _RoleRepository ??= new RoleRepository(_context, _mapper);
        public AuthorizationRepository AuthorizationRepository => _AuthorizationRepository ??= new AuthorizationRepository(_context, _mapper);

        public CountryRepository CountryRepository => _CountryRepository ??= new CountryRepository(_context, _mapper);
        public RegionRepository RegionRepository => _RegionRepository ??= new RegionRepository(_context, _mapper);
        public CityRepository CityRepository => _CityRepository ??= new CityRepository(_context, _mapper);

        #endregion


        #region BusinessPartner
        private VehicleBrandRepository? _VehicleBrandRepository;
        private VehicleModelRepository? _VehicleModelRepository;
        private ProviderProfileRepository? _ProviderProfileRepository;
        private ProviderDriverRepository? _ProviderDriverRepository;
        private TransportVehicleRepository? _TransportVehicleRepository;
        #endregion

        #region Customer
        private CustomerWarehouseRepository? _CustomerWarehouseRepository;
        #endregion


        #region BusinessPartner Properties
        public VehicleBrandRepository VehicleBrandRepository =>
            _VehicleBrandRepository ??= new VehicleBrandRepository(_context, _mapper);

        public VehicleModelRepository VehicleModelRepository =>
            _VehicleModelRepository ??= new VehicleModelRepository(_context, _mapper);

        public ProviderProfileRepository ProviderProfileRepository =>
            _ProviderProfileRepository ??= new ProviderProfileRepository(_context, _mapper);

        public ProviderDriverRepository ProviderDriverRepository =>
            _ProviderDriverRepository ??= new ProviderDriverRepository(_context, _mapper);

        public TransportVehicleRepository TransportVehicleRepository =>
            _TransportVehicleRepository ??= new TransportVehicleRepository(_context, _mapper);
        #endregion

        #region Customer Properties
        public CustomerWarehouseRepository CustomerWarehouseRepository =>
            _CustomerWarehouseRepository ??= new CustomerWarehouseRepository(_context, _mapper);
        #endregion


        #region Shipment
        private ShipmentFreightRepository? _ShipmentFreightRepository;

        #endregion

        #region Shipment Properties
        public ShipmentFreightRepository ShipmentFreightRepository =>
            _ShipmentFreightRepository ??= new ShipmentFreightRepository(_context, _mapper);

        #endregion

    }
}
