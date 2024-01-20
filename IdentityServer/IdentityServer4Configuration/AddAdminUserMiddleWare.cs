using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using SharedLogic.IdentityServer;
using IdentityConstants = SharedLogic.IdentityServer.IdentityConstants;

namespace IdentityServer.IdentityServer4Configuration
{
    public static class AddAdminUserMiddleWare
    {
        public static void UseAdminUser(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope();
            var userManager = serviceScope?.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope?.ServiceProvider.GetService<RoleManager<Role>>();
            if (userManager != null && roleManager != null
                                    && !userManager.Users.Any(x => x.UserName == IdentityConstants.AdminUserName))
            {
                var adminUser = new User
                {
                    Id = IdentityConstants.AdminUserId,
                    UserName = IdentityConstants.AdminUserName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PhoneNumber = IdentityConstants.AdminPhoneNumber,
                    Email = IdentityConstants.AdminPhoneEmail,
                    NormalizedUserName = IdentityConstants.AdminUserName.ToUpper(),
                    NormalizedEmail = IdentityConstants.AdminPhoneEmail.ToUpper(),
                };
                var adminRole = new Role
                {
                    Id = IdentityConstants.AdminRoleId,
                    Name = IdentityConstants.AdminRoleName,
                    NormalizedName = IdentityConstants.AdminRoleName.ToUpper(),
                };

                var userResult = userManager.CreateAsync(adminUser, IdentityConstants.AdminPassword).GetAwaiter().GetResult();
                if (!userResult.Succeeded) return;
                var roleResult = roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                if (!roleResult.Succeeded) return;
                var userRoleResult = userManager.AddToRoleAsync(adminUser, IdentityConstants.AdminRoleName).GetAwaiter().GetResult();
            }
        }
    }
}
