using Microsoft.AspNetCore.Identity;

namespace SharedLogic.IdentityServer
{
    public class User : IdentityUser<string>
    {
        public override string Id { get => base.Id ; set => base.Id = Guid.NewGuid().ToString(); }
    }
}
