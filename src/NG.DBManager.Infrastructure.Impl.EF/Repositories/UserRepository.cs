using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public User GetByEmail(string emailAddress)
        {
            return DbSet
                .SingleOrDefault(u => u.Email == emailAddress);
        }

        public User Edit(User entity)
        {
            if (entity == null) { return null; }

            var updatedUser = DbSet.Find(entity.Id);

            if (updatedUser == null) { return null; }

            if (entity.Name != null) updatedUser.Name = entity.Name;
            if (entity.Birthdate != default) updatedUser.Birthdate = entity.Birthdate;
            if (entity.PhoneNumber != null) updatedUser.PhoneNumber = entity.PhoneNumber;
            if (entity.Email != null) updatedUser.Email = entity.Email;
            if (entity.Password != null) updatedUser.Password = entity.Password;

            DbSet.Update(updatedUser);

            return DbSet.Find(entity.Id);
        }

        public User ConfirmEmail(Guid UserId)
        {
            var user = DbSet.Find(UserId);

            if (user == null)
                return null;

            user.EmailConfirmed = true;
            DbSet.Update(user);

            return DbSet.Find(UserId);
        }
    }
}
