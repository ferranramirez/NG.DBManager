using Microsoft.EntityFrameworkCore;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class StandardUserRepository : Repository<StandardUser>, IStandardUserRepository
    {
        DbContext _context;
        IPasswordHasher _passwordHasher;

        public StandardUserRepository(DbContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public override void Add(StandardUser entity)
        {
            if (entity == null) { return; }

            entity.Password = _passwordHasher.Hash(entity.Password);

            DbSet.Add(entity);
        }

        public StandardUser Edit(StandardUser entity)
        {
            if (entity == null) { return null; }

            var updatedUser = DbSet.Find(entity.UserId);

            if (updatedUser == null) { return null; }

            if (entity.Password != null) updatedUser.Password = _passwordHasher.Hash(entity.Password);

            DbSet.Update(updatedUser);

            return DbSet.Find(entity.UserId);
        }

        public StandardUser ConfirmEmail(Guid UserId)
        {
            var user = DbSet.Find(UserId);

            if (user == null) return null;

            user.EmailConfirmed = true;
            DbSet.Update(user);

            return DbSet.Find(UserId);
        }
    }
}
