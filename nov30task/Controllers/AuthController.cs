using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.ExternalServices.Implements;
using nov30task.ExternalServices.Interfaces;
using nov30task.Hellpers;
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
        IEmailService _emailService { get; }

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        /*public IActionResult SendMail()
        {
            _emailService.Send();
        }*/

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

            await _sendConfirmationEmail(user);

            return View();
        }

        // Email:

        public async Task<IActionResult> SendConfirmationEmail(string username)
        {
            await _sendConfirmationEmail(await _userManager.FindByNameAsync(username));
            return Content("Email sent.");
        }

        async Task _sendConfirmationEmail(AppUser user)
        {
            string userToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("EmailConfirmation", "Auth", new
            {
                userToken = userToken,
                username = user.UserName
            }, Request.Scheme);

            string emailConfirmationPage = Directory.GetCurrentDirectory() + "/wwwroot/emailconfirmtemplate.html";

            using StreamReader reader = new(emailConfirmationPage);
            string template = reader.ReadToEnd()
                .Replace("[[[FullName]]]", user.FullName)
                .Replace("[[[link]]]", link);
            /*template = template.Replace("[[[FullName]]]", user.FullName);
            template = template.Replace("[[[link]]]", link);*/

            _emailService.Send(user.Email, "Confirm Email", template);
        }

        public async Task<IActionResult> EmailConfirmation(string userToken, string username)
        {
            var emailConfirmationResult = await _userManager.ConfirmEmailAsync(await _userManager.FindByNameAsync(username), userToken);

            if (emailConfirmationResult.Succeeded) return Content("Well Done! Bye C:");

            return Problem();

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

            if (user == null)
            {
                ModelState.AddModelError("", "Password or Username is incorrect.");
                return View(loginVM);
            }

            var userSignInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);

            if (!userSignInResult.Succeeded)
            {
                var param = new { username = user.UserName };


                if (userSignInResult.IsLockedOut) ModelState.AddModelError("", $"Too many attempts. Wait until {DateTime.Parse(user.LockoutEnd.ToString()):HH:mm}");
                else if (!user.EmailConfirmed) ViewBag.Link = $"Confirm your email first -> <a href='{Url.Action("SendConfirmationEmail", "Auth", param)}'>Confirm email</a>";
                else ModelState.AddModelError("", "Password or Username is incorrect.");

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
