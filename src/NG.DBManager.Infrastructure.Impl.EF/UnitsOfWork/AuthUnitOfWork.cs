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
        private IStandardUserRepository _standardUserRepository { get; set; }
        private ISocialUserRepository _socialUserRepository { get; set; }

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
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context, _passwordHasher));
                }
                return _userRepository;
            }
        }
        public IStandardUserRepository StandardUser
        {
            get
            {
                if (_standardUserRepository == null)
                {
                    return (_standardUserRepository =
                        (IStandardUserRepository)Activator.CreateInstance(typeof(StandardUserRepository), _context, _passwordHasher));
                }
                return _standardUserRepository;
            }
        }
        public ISocialUserRepository SocialUser
        {
            get
            {
                if (_socialUserRepository == null)
                {
                    return (_socialUserRepository =
                        (ISocialUserRepository)Activator.CreateInstance(typeof(SocialUserRepository), _context, _passwordHasher));
                }
                return _socialUserRepository;
            }
        }
    }
}
