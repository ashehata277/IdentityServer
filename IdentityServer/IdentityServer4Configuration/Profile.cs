using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4Configuration
{
    public class Profile : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Profile(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            if (context.Client.ClientId == IDentityConstants.MobileClientId)
            {

            }
            else if (context.Client.ClientId == IDentityConstants.AngularClientId)
            {
                var user = await _userManager.FindByIdAsync(sub);
                if (user == null)
                {
                    return;
                }
                var userRoleList = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>();

                claims.Add(new Claim(IDentityConstants.UserNameClaim, user.UserName ?? string.Empty));
                claims.Add(new Claim(IDentityConstants.UserIdClaim, user.Id?.ToString() ?? string.Empty));
                claims.Add(new Claim(IDentityConstants.ClientTypeClaim, IDentityConstants.AngularClientType));
                foreach (var userRole in userRoleList)
                {
                    claims.Add(new Claim(IDentityConstants.RoleClaim, userRole?.ToString() ?? string.Empty));
                }
                context.IssuedClaims = claims;

            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                context.IsActive = false;
                return;
            }
            if (context.Client.ClientId == IDentityConstants.MobileClientId)
            {
                context.IsActive = user.PhoneNumberConfirmed;
            }
            else if (context.Client.ClientId == IDentityConstants.AngularClientId) 
            {
                context.IsActive = user.EmailConfirmed;
            }
        }
    }
}
