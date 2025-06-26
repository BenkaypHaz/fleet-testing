using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Library.Infraestructure.Persistence.Repositories.Admin;

namespace Library.Infraestructure.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        #region Service.Admin

        private UsersRepository? _UsersRepository;
        private AuthRepository? _AuthRepository;
        private RolesRepository? _RolesRepository;
        private AuthorizationsRepository? _AuthorizationsRepository;
        private GeneralRepository? _GeneralRepository;


        public UsersRepository UsersRepository => _UsersRepository ??= new UsersRepository(_context);
        public AuthRepository AuthRepository => _AuthRepository ??= new AuthRepository(_context);
        public RolesRepository RolesRepository => _RolesRepository ??= new RolesRepository(_context);
        public AuthorizationsRepository AuthorizationsRepository => _AuthorizationsRepository ??= new AuthorizationsRepository(_context);
        public GeneralRepository GeneralRepository => _GeneralRepository ??= new GeneralRepository(_context);

        #endregion

    }
}
