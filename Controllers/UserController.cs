using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using webdb.Database;

namespace webdb.Controllers
{
    public class UserController : Controller
    {
        private readonly WebDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(
            WebDbContext db,
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager) 
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        public class RegisterRequestModel
        {
            [Required]
            [RegularExpression("[A-Za-z0-9_]+", ErrorMessage = "Username can contain only Letters Digits and Undrscors")]
            public string UserName { get; set; } = default!;
            [Required]
            public string Password { get; set; } = default!;
            [Required]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = default!;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var userToCheck = _db.Users.FirstOrDefault(user => user.UserName == model.UserName);
                if (userToCheck != null) {
                    ModelState.AddModelError(string.Empty, "Something is wrong.");
                    return View(model);
                }

                var user = new IdentityUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        public class LoginRequestModel
        {
            [Required]
            public string UserName { get; set; } = default!;
            [Required]
            public string Password { get; set; } = default!;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(user => user.UserName == model.UserName);
                if (user == null) { return View(model); }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
