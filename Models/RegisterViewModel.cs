using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Models
{
    public class RegisterViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string userName { get; set; }

        public Roles role { get; set; }
        
    }
}
