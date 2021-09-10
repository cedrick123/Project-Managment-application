using Microsoft.AspNetCore.Mvc;
using new_project.Data;
using new_project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProjectController(ApplicationDbContext db)
        {
            this._context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProject()
        {
            List<Project> projectList = _context.projects.ToList();
            return View(projectList);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            Project project = new Project();
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Project project)
        {
            if(ModelState.IsValid)
            {

                var result = await _context.projects.AddAsync(project);
                _context.SaveChanges();
                return RedirectToAction("GetProject", "Project");
            }
            return View(project);
        }

        [HttpGet("/project/details/{id:int}")]
        public IActionResult Details(int id)
        {
            return View("_details",_context.projects.Find(id));
        }
    }
}
