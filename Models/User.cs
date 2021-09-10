using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace new_project.Models
{
    public class User: IdentityUser<int>
    {
        public User(string userName) : base(userName)
        {
        }
        public User()
        {

        }
        
        public Project userProject { get; set; }
        public Roles role { get; set; }

        [NotMapped]
        public List<Project> projectToChoose { get; set; }
    }
    
}
