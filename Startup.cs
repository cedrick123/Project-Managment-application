using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using new_project.Data;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("AppData")));

            services.AddControllersWithViews();
            services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        }
        private async Task CreateRoles(RoleManager<Role> roleManager)
        {

            string[] roles = Enum.GetNames(typeof(Roles));

            foreach (var role in roles) 
            {
                var result = await roleManager.RoleExistsAsync(role);
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role(role));
                }
            }
        }
        private async Task CreateRoot(UserManager<User> userManager)
        {

            string rootName = "root";
            string rootpswd = "Root123!@#";
            //string rootEmail = "root@root.com";
            var root = new User(rootName);
            if(await userManager.FindByNameAsync(rootName)==null)
            {
                var result = await userManager.CreateAsync(root, rootpswd);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(root, "Admin");
                   // Console.WriteLine("CZe chyba git");
                }
            }
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
                var roleManager = (RoleManager<Role>)scope.ServiceProvider.GetService(typeof(RoleManager<Role>));

                CreateRoles(roleManager).GetAwaiter().GetResult();
                CreateRoot(userManager).GetAwaiter().GetResult();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
