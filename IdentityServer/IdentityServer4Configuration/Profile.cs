﻿using IdentityServer4.Extensions;
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
using IdentityConstants = SharedLogic.IdentityServer.IdentityConstants;

namespace IdentityServer.IdentityServer4Configuration
{
    public class Profile : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public Profile(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            if (context.Client.ClientId == IdentityConstants.MobileClientId)
            {

            }
            else if (context.Client.ClientId == IdentityConstants.AngularClientId)
            {
                var user = await _userManager.FindByIdAsync(sub);
                if (user == null)
                {
                    return;
                }
                var userRoleList = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>();

                claims.Add(new Claim(IdentityConstants.UserNameClaim, user.UserName ?? string.Empty));
                claims.Add(new Claim(IdentityConstants.UserIdClaim, user.Id?.ToString() ?? string.Empty));
                claims.Add(new Claim(IdentityConstants.ClientTypeClaim, IdentityConstants.AngularClientType));
                claims.Add(new Claim(IdentityConstants.SecurityStamp, user.SecurityStamp));
                foreach (var userRole in userRoleList)
                {
                    claims.Add(new Claim(IdentityConstants.RoleClaim, userRole?.ToString() ?? string.Empty));
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
            if (context.Client.ClientId == IdentityConstants.MobileClientId)
            {
                context.IsActive = user.PhoneNumberConfirmed;
            }
            else if (context.Client.ClientId == IdentityConstants.AngularClientId) 
            {
                context.IsActive = user.EmailConfirmed;
            }
            else if (context.Client.ClientId == IdentityConstants.SwaggerClientId)
            {
                context.IsActive = user.EmailConfirmed && user.PhoneNumberConfirmed;
            }
        }
    }
}
