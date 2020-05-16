using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.IntegrationTest.Resources;

namespace NG.DBManager.Test.IntegrationTest.Fixture
{
    public class IoCModuleFixture
    {
        public ServiceProvider _serviceProvider { get; }

        public IoCModuleFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<NgContext>((_, builder) =>
            {
                builder.UseSqlServer(DbTestResources.CONNECTIONSTRING);
            })
            .AddScoped<IUnitOfWork, UnitOfWork>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

    }
}
