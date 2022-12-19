﻿using CSharpFunctionalExtensions;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using SharedLogic.IdentityServer;
using SharedLogic.Resources;
using System.Security.Claims;

namespace IdentityServer.IdentityServer4Configuration
{
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<User> _userManager;

        public ResourceOwnerValidator(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            var userName = context.UserName;
            Maybe<User> mayebeUser = await _userManager.FindByNameAsync(userName);
            if (mayebeUser.HasNoValue)
            {
                context.Result.Error = StatusCodes.Status404NotFound.ToString();
                context.Result.ErrorDescription = SharedResource.UserNotFound;
                context.Result.IsError = true;
                return;
            }
            var user = mayebeUser.Value;
            if (!(await _userManager.CheckPasswordAsync(user, context.Password)))
            {
                context.Result.Error = StatusCodes.Status401Unauthorized.ToString();
                context.Result.ErrorDescription = SharedResource.PasswordNotCorrect;
                context.Result.IsError = true;
                return;

            }
            if (!(await _userManager.IsEmailConfirmedAsync(user)))
            {
                context.Result.Error = StatusCodes.Status423Locked.ToString();
                context.Result.ErrorDescription = SharedResource.UserNotActive;
                context.Result.IsError = true;
                return;
            }
            Dictionary<string, Object> additionalInfo = new Dictionary<string, object>();

            additionalInfo.Add(IDentityConstants.TokenInfo_Name, user.UserName);

            context.Result = new GrantValidationResult(
                             subject: user.Id.ToString(),
                             authenticationMethod: IDentityConstants.TokenInfo_AuthenticationMethod,
                             claims: new Claim[]
                             {
                                     new Claim(ClaimTypes.NameIdentifier, context.UserName)
                             },
                             identityProvider: IDentityConstants.TokenInfo_IdentityProvider,
                             additionalInfo);


        }
    }
}
