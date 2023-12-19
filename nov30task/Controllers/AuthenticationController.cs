using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using nov30task.Models;
using nov30task.ViewModels.AuthenticationVM;

namespace nov30task.Controllers
{
    public class AuthenticationController : Controller
    {

        SignInManager<AppUser> _signInManager { get; }
        UserManager<AppUser> _userManager { get; }

        public AuthenticationController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Register:

        // Get
        public IActionResult Register()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var result = await _userManager.CreateAsync(new AppUser
            {
                FullName = registerViewModel.Fullname,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            }, registerViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
                return View(registerViewModel);
            }

            return View();
        }

        // Login:
        public IActionResult Login()
        {
            return View();
        }
    }
}
