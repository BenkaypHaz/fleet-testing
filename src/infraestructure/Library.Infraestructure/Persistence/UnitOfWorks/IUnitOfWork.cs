using Library.Infraestructure.Persistence.Repositories.Auth;
using Library.Infraestructure.Persistence.Repositories.General;


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
    }
}
