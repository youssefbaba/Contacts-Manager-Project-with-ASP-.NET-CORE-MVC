using Assignement_11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Assignement_11.Controllers
{
    public class SocialMediaLinksController : Controller
    {
        // Fields
        private readonly SocialMediaLinksOptions _socialMediaLinksOptions;

        // Constructors
        public SocialMediaLinksController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions)
        {
            _socialMediaLinksOptions = socialMediaLinksOptions.Value;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SocialMediaLinks = _socialMediaLinksOptions;
            return View();
        }
    }
}
