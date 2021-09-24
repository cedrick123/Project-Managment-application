using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace new_project.Controllers
{
    public class AdminPanelController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
       public AdminPanelController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }



      public IActionResult Login(string returnUrl)
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RegisterViewModel? model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.userName, model.password, true, false);


                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Privacy", "Home");
            }
            return View(model);
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
