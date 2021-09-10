using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Models
{
    public class Role : IdentityRole<int>

    {
        public Role(string roleName):base(roleName)
        {
        }
        public Role() { }
        
    }
}
