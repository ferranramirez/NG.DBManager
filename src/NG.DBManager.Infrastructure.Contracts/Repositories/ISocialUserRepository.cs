using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ISocialUserRepository : IRepository<SocialUser>
    {
        SocialUser Get(string SocialId, string Provider);
    }
}
