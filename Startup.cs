using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using new_project.Authorization;
using new_project.Data;
using new_project.Models;
using new_project.Services.CustomerContractService;
using new_project.Services.EmailService;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            services.AddSingleton(typeof(IConverter),
                new SynchronizedConverter(new PdfTools()));

            //services.AddScoped(typeof(Repository<>),typeof(RepositoryFor<>));
            //services.AddScoped<Repository<Project>, RepositoryFor<Project>>();
            services.AddScoped<ContractService, ContractServiceForPdf>();
            services.AddTransient(typeof(Repository<>), typeof(RepositoryFor<>));





            services.Configure<EmailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<MailService, MailServiceImplementation>();
            services.AddScoped<IAuthorizationHandler, ProjectAuthorizationHandler>();

            //services.AddControllers();

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
