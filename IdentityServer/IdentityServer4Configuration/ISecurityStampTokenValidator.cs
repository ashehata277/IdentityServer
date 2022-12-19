using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4Configuration
{
    internal interface ISecurityStampTokenValidator
    {
        Task<bool> ValidateAsync();
    }
}
