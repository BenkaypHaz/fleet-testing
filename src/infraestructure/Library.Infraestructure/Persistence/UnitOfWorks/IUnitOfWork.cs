using Library.Infraestructure.Persistence.Repositories.Admin;


namespace Library.Infraestructure.Persistence.UnitOfWorks
{
    public interface IUnitOfWork
    {
        #region Service.Admin
        UsersRepository UsersRepository { get; }
        AuthRepository AuthRepository { get; }
        RolesRepository RolesRepository { get; }
        AuthorizationsRepository AuthorizationsRepository { get; }
        GeneralRepository GeneralRepository { get; }
        #endregion
    }
}
