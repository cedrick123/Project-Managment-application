using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using new_project.Models;
using System;
using System.Threading.Tasks;

namespace new_project.Authorization
{
    public class ProjectAuthorizationHandler : AuthorizationHandler<PermissionRequirement, Project>
    {
        

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement, Project resource)
        {
            if(context.User.HasClaim("Permission",requirement.permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
