using Microsoft.AspNetCore.Identity;

namespace SharedLogic.IdentityServer
{
    public class Role : IdentityRole<string>
    {
        public override string Id { get => base.Id; set => base.Id = Guid.NewGuid().ToString(); }
    }
}
