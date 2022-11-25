using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        UserToken>,IDisposable
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
