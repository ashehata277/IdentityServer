using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedApplication.ContextInterfaces;
using SharedLogic.IdentityServer;

namespace SharedData.SharedContext
{
    public class IdentityContext :
        IdentityDbContext<User,
            Role,
            string,
            UserClaim,
            UserRole,
            UserLogin,
            RoleClaim,
            UserToken>, IIdentityContext
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        private readonly ILoggerFactory _loggerFactory = LoggerFactory
            .Create(a =>
            {
                a.AddConsole();
                a.AddDebug();
            });

        public IdentityContext(DbContextOptions<IdentityContext> options,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration) :
            base(options)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public DbSet<User> User => this.Users;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                ConfigureDeveloping(optionsBuilder);
            }
            else
            {
                ConfigureProduction(optionsBuilder);
            }
        }

        private void ConfigureProduction(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"),
                options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        private void ConfigureDeveloping(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"),
                options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}