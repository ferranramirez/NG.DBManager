using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NG.Common.Library.Extensions;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.IoCModule;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System.Reflection;

namespace NG.DBManager.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            });

            services.AddHealthCheckMiddleware(Configuration);

            services.AddControllers();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            services.AddSwaggerDocumentation(Configuration.GetSection("Documentation"), xmlFile);

            services.AddJwtAuthentication(Configuration.GetSection("Secrets"));

            services.AddScoped<IFullUnitOfWork, FullUnitOfWork>();
            services.AddSingleton<DatabaseUtilities>();

            services.AddInfrastructureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorDisplayMiddleware();

            app.UseHealthCheckMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwaggerDocumentation(Configuration.GetSection("Documentation"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
