using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace new_project.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
       public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

      public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RegisterViewModel model)
        {
            
            var result = await signInManager.PasswordSignInAsync(model.userName, model.password,true, false);

            if (result.Succeeded)
            {
                ViewBag.Message = "Hello" + model.userName+"Welcome on my site";
                return RedirectToAction("", "Home");
            }
            return RedirectToAction("Privacy", "Home");
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("", "Home");

        }

        public IActionResult Register()
        {
            return View();
        } 



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await userManager.FindByNameAsync(model.userName);
                if(result == null)
                {
                    var newUser = new User(model.userName);
                    newUser.Email = model.email;
                    newUser.role = model.role;
                    var result1 = await userManager.CreateAsync(newUser,model.password);
                    if(result1.Succeeded)
                    {
                        userManager.AddToRoleAsync(newUser, model.role.ToString()).GetAwaiter().GetResult();
                        signInManager.SignInAsync(newUser, true).GetAwaiter().GetResult();
                    }
                    return RedirectToAction("", "Home");
                }
            }
            return View(model);
        }

    }
}
