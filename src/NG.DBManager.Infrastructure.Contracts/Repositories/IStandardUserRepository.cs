using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IStandardUserRepository : IRepository<StandardUser>
    {
        StandardUser Edit(StandardUser user);
        StandardUser ConfirmEmail(Guid UserId);
    }
}
