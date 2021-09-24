using Microsoft.AspNetCore.Mvc;
using new_project.Data;
using new_project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace new_project.Controllers
{

    public class UserManagmentController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserManagmentController(ApplicationDbContext context,UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            var result = _context.Users.Include(m => m.userProject).Include(m => m.role).ToList();
            return View(result);
        }
        


        //to do authorization
        [HttpGet("usermanagment/edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var result = _context.Users.Find(id);
            return View(result);
        }


        //to do authorization
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {

            var entity =  _context.Users.Include(m => m.role).First(m => m.Id == user.Id);
            if(entity.role != null)
            {
               await _userManager.RemoveFromRoleAsync(entity, _context.Roles.Find(entity.role.Id).Name);
            }
            entity.UserName = user.UserName;
            entity.Email = user.Email;
            entity.role = await _context.Roles.FindAsync(user.role.Id);
            entity.userProject = await _context.projects.FindAsync(user.userProject.projectId);
            await _userManager.AddToRoleAsync(entity, entity.role.Name);
            

            
            _context.SaveChanges();
            return RedirectToAction("GetUsers", "UserManagment");            
        }

        
        [HttpGet("UserManagment/details/{id:int}")]
        public IActionResult Details(int id)
        {
            return View(_context.Users.Include(m=>m.userProject).First(m=>m.Id == id));
        }


        
    }
}
