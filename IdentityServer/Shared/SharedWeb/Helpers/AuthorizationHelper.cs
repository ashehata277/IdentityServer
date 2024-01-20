using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace SharedWeb.Helpers;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}

public static class AuthorizationHelper
{
    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PermissionsEnum.AllowView.ToString(),
                policy =>
                    policy.Requirements.Add(new PermissionRequirement(PermissionsEnum.AllowView.ToString())));
            
            options.AddPolicy(PermissionsEnum.AllowEdit.ToString(),
                policy =>
                    policy.Requirements.Add(new PermissionRequirement(PermissionsEnum.AllowEdit.ToString())));
            
            
            options.AddPolicy(PermissionsEnum.AllowDelete.ToString(),
                policy =>
                    policy.Requirements.Add(new PermissionRequirement(PermissionsEnum.AllowDelete.ToString())));
            
            options.AddPolicy(PermissionsEnum.AllowAdd.ToString(),
                policy =>
                    policy.Requirements.Add(new PermissionRequirement(PermissionsEnum.AllowAdd.ToString())));
            
            options.AddPolicy(PermissionsEnum.AllowPrint.ToString(),
                policy =>
                    policy.Requirements.Add(new PermissionRequirement(PermissionsEnum.AllowPrint.ToString())));
        });
        return services;
    }
}