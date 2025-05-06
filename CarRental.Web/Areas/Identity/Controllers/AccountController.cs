using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
using CarRental.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRental.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
           
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = userViewModel.Name;
                user.Email = userViewModel.Email;

                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    string role = HttpContext.Request.Form["Role"].ToString();

                    if (role==Roles.CustomerRole)
                    {
                        await userManager.AddToRoleAsync(user, Roles.CustomerRole);
                        return RedirectToAction("Login", "Account");
                    }
                    else if(role == Roles.AdminRole)
                    {
                        await userManager.AddToRoleAsync(user, role);
                        return RedirectToAction("Index", "Car", new { area = "Admin" });
                    }
                  
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginUser.Name);

                if (user != null)
                {
                    var finedUser = await userManager.CheckPasswordAsync(user, loginUser.Password);
                    if (finedUser == true)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Rent", new { area = "Customer" });
                    }
                }

                ModelState.AddModelError("", "UserName or Password is wrong");
            }
            return View("Login", loginUser);
        }
     
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
