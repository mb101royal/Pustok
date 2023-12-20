using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Helpers;
using nov30task.Models;
using nov30task.ViewModels.AuthVM;

namespace nov30task.Controllers
{
    public class AuthController : Controller
    {

        SignInManager<AppUser> _signInManager { get; }
        UserManager<AppUser> _userManager { get; }
        RoleManager<IdentityRole> _roleManager { get; }

		public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// Register:

		// Get
		public IActionResult Register()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = new AppUser
            {
                FullName = registerVM.Fullname,
                Email = registerVM.Email,
                UserName = registerVM.Username
            };

			var createUser = await _userManager.CreateAsync(user, registerVM.Password);

            if (!createUser.Succeeded)
            {
                foreach (var error in createUser.Errors) ModelState.AddModelError("", error.Description);

                return View(registerVM);
            }
            
            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            if (!roleResult.Succeeded)
            {
				ModelState.AddModelError("", "Something's gone wrong. It's not you, it's us.");
                return View(registerVM);
			}

            return View();
        }

        // Login:

        // Get
        public IActionResult Login()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginVM loginVM)
        {
            /*await _userManager.Users.FirstOrDefaultAsync(user => user.Email == loginVM.UsernameOrEmail || user.UserName == loginVM.UsernameOrEmail);*/

            /*var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail) ?? await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);*/

            AppUser user;

            if (!ModelState.IsValid) return View(loginVM);

            if (loginVM.UsernameOrEmail.Contains("@")) user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            else user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);

            // Note: Db-da register olmus user olmasa exception verir.

            if (user == null)
            {
                ModelState.AddModelError("", "Password or Username is incorrect.");
                return View(loginVM);
            }

            var userSignInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);

            if (!userSignInResult.Succeeded)
            {
                if (userSignInResult.IsLockedOut) ModelState.AddModelError("", $"Too many attempts. Wait until {DateTime.Parse(user.LockoutEnd.ToString()):HH:mm}");
                else ModelState.AddModelError("", "Password or Email is incorrect.");

                return View(loginVM);
            }

            if (returnUrl != null) return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<bool> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
					var assignRole = await _roleManager.CreateAsync(new IdentityRole
					{
						Name = item.ToString()
					});

                    if (!assignRole.Succeeded) return false;
				}
            }

            return true;
        }
    }
}
