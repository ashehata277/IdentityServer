using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedApplication.ContextInterfaces;
using SharedData;
using SharedData.SharedContext;

namespace IdentityServer.DataBaseConfiguration
{
    public static class DbConfigs
    {
        public static IServiceCollection AddIdentityDataBaseConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(configuration.GetConnectionString("Default"))
                       .EnableSensitiveDataLogging();
            });
            services.AddScoped<IIdentityContext, IdentityContext>();
            return services;
        }


        public static void MigrateContexts(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            serviceScope.ServiceProvider.GetRequiredService<IdentityContext>().Database.Migrate();
        }
    }
}
