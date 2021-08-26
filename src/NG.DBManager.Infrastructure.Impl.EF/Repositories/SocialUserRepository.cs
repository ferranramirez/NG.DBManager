using Microsoft.EntityFrameworkCore;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class SocialUserRepository : Repository<SocialUser>, ISocialUserRepository
    {
        DbContext _context;

        public SocialUserRepository(DbContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
        }

        public SocialUser Get(string SocialId, string Provider)
        {
            if (SocialId == default || string.IsNullOrEmpty(Provider)) return null;

            return DbSet
                .Include(su => su.User)
                .SingleOrDefault(su => su.SocialId == SocialId && string.Equals(su.Provider, Provider));
        }
    }
}
