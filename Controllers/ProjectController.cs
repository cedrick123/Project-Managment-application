using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_project.Authorization;
using new_project.Data;
using new_project.Models;
using new_project.Services.CustomerContractService;
using new_project.Services.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Repository<Project> _projectRepository;
        private readonly ContractService _reportService;
        private readonly MailService _mailService;
        private readonly IAuthorizationService _authorizationService;

        public ProjectController(Repository<Project> projectRepository, ContractService reportService, MailService mailService,IAuthorizationService authorizationService)
        {
            this._projectRepository = projectRepository;
            this._reportService = reportService;
            this._mailService = mailService;
            this._authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProject()
        {
            List<Project> projectList = _projectRepository.GetAll();
            return View(projectList);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result1 = await _authorizationService.AuthorizeAsync(User, new Project(),
                new PermissionRequirement(new OperationAuthorizationRequirementGenerator("Project").CreateOperation()));
            if (result1.Succeeded)
            {
                return Forbid();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var result1 = await _authorizationService.AuthorizeAsync(User, project,
                new PermissionRequirement(new OperationAuthorizationRequirementGenerator("Project").CreateOperation()).permission);
            if(result1.Succeeded)
            {
                if (ModelState.IsValid)
                {
                    _projectRepository.Create(project);
                    return RedirectToAction("GetProject", "Project");
                }
                else
                {
                    return View(project);
                }
            }
            
            return Forbid();
        }

        [HttpGet("/project/details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var resultOfAuthentication = await _authorizationService.AuthorizeAsync(User, new Project(),
                new PermissionRequirement(new OperationAuthorizationRequirementGenerator("Project").ViewOperation()));
            if(resultOfAuthentication.Succeeded)
            {
                return View("_details", _projectRepository.Read(id));

            }
            return Forbid();
        }

        [HttpGet("/project/getContract/{id:int}")]
        public IActionResult GetContract(int id)
        {
            var project = _projectRepository.Read(id);

            return File(
                _reportService.GeneratePdfContract(project),
                "text/plain",
                "cze.pdf");
        }

        [HttpGet("project/sendcontract/{id:int}")]
        public async Task<IActionResult> SendContract(int id)
        {
                await _mailService.SendEmailAsync(new MailRequest("skonik25@gmail.com", 
                "test",
                "test",
                _reportService.GeneratePdfContract(_projectRepository.Read(id))));

            return RedirectToAction("", "Home");
        }
        
        


    }
}
