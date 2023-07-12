using Microsoft.AspNetCore.Mvc;


namespace Assignement_4.Controllers
{
    public class BankAccountController : Controller
    {
        [Route("/")]    
        public IActionResult Index()
        {
            return Content("Welcmoe to the Best Bank", "text/plain");    
        }

        [Route("account-details")]
        public IActionResult Details()
        {
            return Json(new { accountNumber = 1001, accountHolderName = "John Doe", currentBalance = 5000 });
        }

        [Route("account-statement")]
        public IActionResult AccountStatement()
        {
           return File("/docs.pdf", "application/pdf");
        }

        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult GetCurrentBalance()
        {
            if (!HttpContext.Request.RouteValues.ContainsKey("accountNumber"))
            {
                return NotFound("Account Number should be supplied");
            }

            var bankAccount = new { accountNumber = 1001, accountHolderName = "John Doe", currentBalance = 5000 };

            if (Convert.ToInt32(HttpContext.Request.RouteValues["accountNumber"]) == 1001)
            {
                return Content($"{bankAccount.currentBalance}");
            }
            else
            {
                return BadRequest("Account Number should be 1001");
            }
        }
    }
}
