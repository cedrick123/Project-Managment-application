using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using new_project.Data;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace new_project.Controllers
{
    public class UserManagmentController : Controller
    {
        private ApplicationDbContext _context;
        public UserManagmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            var result = _context.Users.ToList();
            return View(result);
        }
        


        //to do authorization
        [HttpGet("usermanagment/edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var result = _context.Users.Find(id);
            result.projectToChoose = new List<Project>(_context.projects.ToList());
            return View(result);
        }


        //to do authorization
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
           
            var entity = _context.Users.Find(user.Id);
            entity.UserName = user.UserName;
            entity.Email = user.Email;
            entity.role = user.role;
            entity.userProject = await _context.projects.FindAsync(user.userProject.projectId);
            _context.Entry(entity.userProject).State = EntityState.Modified;
            
            _context.SaveChanges();
            return RedirectToAction("GetUsers", "UserManagment");            
        }


        [HttpGet("UserManagment/details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _context.Users.FindAsync(id);
            Console.WriteLine(_context.Users.Find(id).userProject.name);
            return View(_context.Users.Find(id));
        }

        
    }
}
