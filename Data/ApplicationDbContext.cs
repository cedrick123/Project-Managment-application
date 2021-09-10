
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using new_project.Models;

namespace new_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {
       public DbSet<Project> projects { get; set; }
       public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Project>()
                .Property(p => p.budget)
                .HasColumnType("decimal(18,4)");
        }

    }

}
