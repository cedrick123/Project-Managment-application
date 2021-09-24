using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_project.Data;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanel : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        public AdminPanel(RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Role> listOfRoles = _roleManager.Roles.ToList();         
            return View(listOfRoles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NewRoleViewModel(_context.Model.GetEntityTypes()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoleViewModel newRoleViewModel)
        {

            var newRole = new Role(newRoleViewModel.roleName);
            await _roleManager.CreateAsync(newRole);
            foreach (var newRoleClaimValue in newRoleViewModel.listOfPermissions)
            {
                if (newRoleClaimValue.selected)
                {
                    await _roleManager.AddClaimAsync(newRole, new System.Security.Claims.Claim("Permission", newRoleClaimValue.name));
                }
            }
            return RedirectToAction("","adminpanel");
        }
    }
}
