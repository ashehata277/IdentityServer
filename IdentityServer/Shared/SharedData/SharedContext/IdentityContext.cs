using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedApplication.ContextInterfaces;
using SharedLogic.IdentityServer;

namespace SharedData
{
    public class IdentityContext :
        IdentityDbContext<User,
        Role,
        string,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>,IDisposable , IIDentityContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        public DbSet<User> User { get => this.Users; }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
