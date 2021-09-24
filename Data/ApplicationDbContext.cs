
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using new_project.Models;
using System;

namespace new_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {
       public virtual DbSet<Project> projects { get; set; }
       public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }
        public ApplicationDbContext()
        {

        }
    }

}
