using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class AuthUnitOfWork : UnitOfWork, IAuthUnitOfWork
    {
        private readonly NgContext _context;
        private readonly IPasswordHasher _passwordHasher;

        private IUserRepository _userRepository { get; set; }

        public AuthUnitOfWork(NgContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                {
                    return (_userRepository =
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context));
                }
                return _userRepository;
            }
        }
    }
}
