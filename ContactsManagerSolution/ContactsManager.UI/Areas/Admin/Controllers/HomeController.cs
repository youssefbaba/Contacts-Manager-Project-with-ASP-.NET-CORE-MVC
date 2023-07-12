using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Admin")]  // Authentication + Role Based Access (Admin, User, ....)
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("/Admin")]  // Attribute Routing : /Admin
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")] // Attribute Routing : /Admin/Home/Details
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            ApplicationUser? currentUser = await _userManager.GetUserAsync(User);
            IList<string>? roles = await _userManager.GetRolesAsync(currentUser);
            var model = new UserRolesViewModel() { CurrentUser = currentUser, Roles = roles };
            return View(model);
        }
    }
}
