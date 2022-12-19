using Microsoft.EntityFrameworkCore;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.ContextInterfaces
{
    public interface IIDentityContext
    {
        public DbSet<User> User { get;}
    }
}
