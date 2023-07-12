using Microsoft.AspNetCore.Mvc;
using ViewComponentsDemo.Models;

namespace ViewComponentsDemo.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync(PersonGridModel gridModel)
        {
            /*
            PersonGridModel personGridModel = new PersonGridModel()
            {
                GridTitle = "Persons List",
                People = new List<Person>()
                {
                    new Person(){ PersonName = "John", JobTitle = "Developer"},
                    new Person(){ PersonName = "Adam", JobTitle = "Designer"},
                    new Person(){ PersonName = "Sara", JobTitle = "Manager"}
                }
            };
            */

            //ViewData["Grid"] = personGridModel;

            //return View(); // invoked partial view from Views/Shared/Components/Grid/Default.cshtml
            //return View(viewName: "Sample", model: personGridModel); // invoked partial view from Views/Shared/Components/Grid/Sample.cshtml
            return View(viewName: "Sample", model: gridModel); // invoked partial view from Views/Shared/Components/Grid/Sample.cshtml
        }
    }
}
