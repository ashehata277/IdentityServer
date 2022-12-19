namespace IdentityServer.IdentityServer4Configuration
{
    public class SecurityStampTokenValidator : ISecurityStampTokenValidator
    {
        public SecurityStampTokenValidator()
        {

        }
        public Task<bool> ValidateAsync()
        {
            return Task.FromResult(true);
        }
    }
}
