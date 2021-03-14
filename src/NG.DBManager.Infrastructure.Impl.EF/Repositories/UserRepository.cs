using Microsoft.EntityFrameworkCore;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        DbContext _context;
        IPasswordHasher _passwordHasher;

        public UserRepository(DbContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public User GetByEmail(string EmailAddress)
        {
            return DbSet
                .SingleOrDefault(u => u.Email.ToLower() == EmailAddress.ToLower());
        }
        public bool? ContainsCommerce(Guid UserId, Guid CommerceId)
        {
            var commerce = _context.Set<Commerce>().SingleOrDefault(com => com.Id == CommerceId);

            if (commerce == null) return null;

            var userCommerces = DbSet
                .Include(u => u.Commerces)
                .SingleOrDefault(u => u.Id == UserId);

            return userCommerces?.Commerces.Contains(commerce);
        }

        public override void Add(User entity)
        {
            if (entity == null) { return; }

            entity.Password = _passwordHasher.Hash(entity.Password);

            DbSet.Add(entity);
        }

        public User Edit(User entity)
        {
            if (entity == null) { return null; }

            var updatedUser = DbSet.Find(entity.Id);

            if (updatedUser == null) { return null; }

            if (entity.Name != null) updatedUser.Name = entity.Name;
            if (entity.Birthdate != default) updatedUser.Birthdate = entity.Birthdate;
            if (entity.PhoneNumber != null) updatedUser.PhoneNumber = entity.PhoneNumber;
            if (entity.Email != null) updatedUser.Email = entity.Email.ToLower();
            if (entity.Password != null) updatedUser.Password = _passwordHasher.Hash(entity.Password);

            DbSet.Update(updatedUser);

            return DbSet.Find(entity.Id);
        }

        public User ConfirmEmail(Guid UserId)
        {
            var user = DbSet.Find(UserId);

            if (user == null) return null;

            user.EmailConfirmed = true;
            DbSet.Update(user);

            return DbSet.Find(UserId);
        }
    }
}
