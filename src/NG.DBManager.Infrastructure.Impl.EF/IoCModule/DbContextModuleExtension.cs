using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.IoCModule
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
                builder.UseSqlServer(provider.GetService<IConfiguration>().GetConnectionString("Default"));
            })
            .AddScoped<IUnitOfWork, APIUnitOfWork>();

            return services;
        }
    }
}
