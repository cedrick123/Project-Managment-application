using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string permission;
        public PermissionRequirement(string permission)
        {
            this.permission = permission;
        }
    }
}
