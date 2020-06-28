using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.Extensions
{
    public static class DbContextModuleExtension
    {
        public static IServiceCollection AddInfrastructureServices(
           this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<NgContext>((provider, builder) =>
            {
                builder.UseNpgsql(provider.GetService<IConfiguration>().GetConnectionString("NotGuiriDb"));
            });

            return services;
        }
    }
}
