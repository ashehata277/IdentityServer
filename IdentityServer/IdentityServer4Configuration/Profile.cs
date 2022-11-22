using IdentityServer4.Models;
using IdentityServer4.Services;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4Configuration
{
    public class Profile : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.Client.ClientId == IDentityConstants.MobileClientId)
            {

            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}
