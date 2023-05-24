using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Utils.CrossCuttingConcerns.Constants;
using Utils.CrossCuttingConcerns.Extensions;

namespace Utils.WebAPI.Configurations
{
    public class CustomIAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomIAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RolesAuthorizationRequirement requirement)
        {
            var allowedRoles = requirement.AllowedRoles.ToList();

            var currentRole = _httpContextAccessor.HttpContext.GetIdentityValueByTypeName(ClaimTypes.Role);

            if (currentRole == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            bool isAuthorized = false;

            foreach (string allowedRole in allowedRoles)
            {
                if (HasPermission(currentRole, allowedRole))
                {
                    isAuthorized = true;
                    break;
                }
            }

            if (isAuthorized)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

        private bool HasPermission(string currentRole, string allowedRole)
        {
            switch (allowedRole)
            {
                case RoleConstant.SuperAdmin:
                    return RoleConstant.SuperAdmin.Equals(currentRole);
                case RoleConstant.Admin:
                    return RoleConstant.SuperAdmin.Equals(currentRole) || RoleConstant.Admin.Equals(currentRole);
                case null:
                    // No role was specified which means the user has a permission to access to the resource.
                    return true;

                default:
                    return false;
            }
        }
    }
}
