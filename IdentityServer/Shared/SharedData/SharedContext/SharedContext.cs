using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLogic.IdentityServer;

namespace SharedData
{
    public class SharedContext :
        IdentityDbContext<User,
        Role,
        string,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>
    {
        public SharedContext(DbContextOptions<SharedContext> options) : base(options)
        {

        }
    }
}
