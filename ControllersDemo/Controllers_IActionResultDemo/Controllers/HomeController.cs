using Controllers_IActionResultDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers_IActionResultDemo.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        //[Route("[action]")]
        [Route("/")]            // attribute routing
        [Route("/home")]        // attribute routing
        public ContentResult Index()
        {
            //return new ContentResult(){ Content = "Hello from Index" , ContentType = "text/plain" };
            //return Content("Hello from Index", "text/plain");
            return Content("<p>Hello from Index</p>", "text/html");
        }

        [Route("/person")]       // attribute routing
        public JsonResult Person()
        {
            List<Person> persons = new List<Person>()
            {
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LatsName = "Doe",
                    Age = 26
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Adam",
                    LatsName = "Scott",
                    Age = 30
                }
            };
            //return new JsonResult(persons);
            return Json(persons);
        }

        [Route("/file-download-pdf")]  // attribute routing
        public VirtualFileResult FileDownloadOne()
        {
            //return new VirtualFileResult("/docs.pdf", "application/pdf");
            return File("/docs.pdf", "application/pdf");
        }

        [Route("/file-download-excel")]  // attribute routing
        public PhysicalFileResult FileDownloadTwo()
        {
            var physicalPath = @"C:\Users\YoussefBaba\Desktop\My_Computer\My-Projects\Cours\Asp_Dot_Net_Core\Controllers_IActionResultDemo\Controllers_IActionResultDemo\docs.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //return new PhysicalFileResult(physicalPath, contentType);
            return PhysicalFile(physicalPath, contentType);
        }

        [Route("/file-download-image")]  // attribute routing
        public FileContentResult FileDownloadThree()
        {
            string physicalPath = @"C:\Users\YoussefBaba\Desktop\My_Computer\My-Projects\Cours\Asp_Dot_Net_Core\Controllers_IActionResultDemo\Controllers_IActionResultDemo\wwwroot\img.jpg";
            byte[] bytes = System.IO.File.ReadAllBytes(physicalPath);
            //return new FileContentResult(bytes, "image/jpeg");
            return File(bytes, "image/jpeg");
        }
    }
}
