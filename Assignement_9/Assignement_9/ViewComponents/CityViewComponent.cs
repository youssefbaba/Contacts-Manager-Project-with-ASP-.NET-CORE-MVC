using Assignement_9.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignement_9.ViewComponents
{
    public class CityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather cityModel)
        {
            // Some C# code here ...

            ViewData["BackgroundColorClass"] = SetBackgroundColorClass(cityModel.TemperatureFahrenheit);

            return View(viewName: "_CityPartialView", model: cityModel);
        }


        private string SetBackgroundColorClass(int temperature)
        {
            if (temperature > 74)
            {
                return "orange-back";
            }
            else
            {
                if (temperature > 44)
                {
                    return "green-back";
                }
                else
                {
                    return "blue-back";
                }
            }
        }
    }
}
