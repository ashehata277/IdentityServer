using IdentityServer.Helper.TaskExtension;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using SharedLogic.IdentityServer;

namespace IdentityServer.IdentityServer4Configuration
{
    public static class AddAdminUserMiddleWare
    {
        public static void UseAdminUser(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var userManager = serviceScope?.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope?.ServiceProvider.GetService<RoleManager<Role>>();
                if (userManager != null && roleManager != null
                    && !userManager.Users.Any(x => x.UserName == IDentityConstants.AdminUserName))
                {
                    var adminUser = new User
                    {
                        Id = IDentityConstants.AdminUserId,
                        UserName = IDentityConstants.AdminUserName,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = IDentityConstants.AdminPhoneNumber,
                        Email = IDentityConstants.AdminPhoneEmail,
                        NormalizedUserName = IDentityConstants.AdminUserName.ToUpper(),
                        NormalizedEmail = IDentityConstants.AdminPhoneEmail.ToUpper(),
                    };
                    var adminRole = new Role
                    {
                        Id = IDentityConstants.AdminRoleId,
                        Name = IDentityConstants.AdminRoleName,
                        NormalizedName = IDentityConstants.AdminRoleName.ToUpper(),
                    };

                    var userResult = userManager.CreateAsync(adminUser, IDentityConstants.AdminPassword).GetAwaiter().GetResult();
                    if (!userResult.Succeeded) return;
                    var roleResult = roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                    if (!roleResult.Succeeded) return;
                    var userRoleResult = userManager.AddToRoleAsync(adminUser, IDentityConstants.AdminRoleName).GetAwaiter().GetResult();
                }

            }
        }
    }
}
