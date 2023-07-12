using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
    //[Route("[controller]/[action]")]
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        // Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        // Constructors
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Methods
        // Conventional Routing : /Account/Register
        
        [HttpGet]
        [Authorize("NotAuthorized")]  // False => Not accessible , True => Accessible
        public IActionResult Register()
        {
            return View();
        }

        // Conventional Routing : /Account/Register
        [HttpPost]
        [Authorize("NotAuthorized")]
        //[ValidateAntiForgeryToken]  // To secure from XCRF attack
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                return View(model: registerDTO);
            }

            // Store user registration details into Indentity database
            ApplicationUser user = new ApplicationUser()
            {
                // Id will be generated automatically
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                PersonName = registerDTO.PersonName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)  // If insertion of new user Succeeded
            {
                // Check status of UserType radio button
                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    // Create 'Admin' role
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) == null)
                    {
                        ApplicationRole role = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(role);
                    }

                    // Add the new user into 'Admin' role
                    await _userManager.AddToRolesAsync(user, new List<string>() { UserTypeOptions.Admin.ToString() });
                }
                else
                {
                    // Create 'User' role
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) == null)
                    {
                        ApplicationRole role = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(role);
                    }

                    // Add the new user into 'User' role
                    await _userManager.AddToRolesAsync(user, new List<string>() { UserTypeOptions.User.ToString() });
                }

                // Sign in 
                await _signInManager.SignInAsync(user, isPersistent: registerDTO.RememberMe);

                // Admin
                if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Home", routeValues: new { area = UserTypeOptions.Admin.ToString() });
                }
                // Regular User
                return RedirectToAction(actionName: nameof(PersonsController.Index), controllerName: "Persons");
            }
            else // If insertion of new user Failed
            {
                AddErrorsToModelState(result);
                return View(model: registerDTO);
            }
        }

        // Conventional Routing : /Account/Login
        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login(string? returnUrl)
        {
            LoginDTO loginDTO = new LoginDTO() { ReturnUrl = returnUrl };
            return View(loginDTO);
        }

        // Conventional Routing : /Account/Login
        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email,
                loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(loginDTO.ReturnUrl) && Url.IsLocalUrl(loginDTO.ReturnUrl))
                {
                    return LocalRedirect(loginDTO.ReturnUrl); // Redirection within the same domain
                }

                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    // Admin
                    if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Home", routeValues: new { area = UserTypeOptions.Admin.ToString() });
                    }
                    // Regular User
                    return RedirectToAction(nameof(PersonsController.Index), "Persons");
                }
            }

            ModelState.AddModelError("Login", "Invalid email or password.");
            return View(loginDTO);
        }

        // Conventional Routing : /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // Removes Identity cookie
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }

        // Conventional Routing : /Account/IsEmailAlreadyRegistered
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                return Json(true); // Valid email address
            }
            return Json(false);  // Invalid email address
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("Register", error.Description);  // To add the errors into ModelState in validation-summary
            }
        }

        // Conventional Routing : /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
