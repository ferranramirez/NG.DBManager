using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string emailAddress);
        bool? ContainsCommerce(Guid UserId, Guid CommerceId);
        User Edit(User user);
        User ConfirmEmail(Guid UserId);
    }
}
