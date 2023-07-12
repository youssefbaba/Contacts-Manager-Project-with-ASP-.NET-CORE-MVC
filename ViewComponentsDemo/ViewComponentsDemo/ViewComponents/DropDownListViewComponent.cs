
using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsDemo.ViewComponents
{
    public class DropDownListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Some C# code here ...

            return View(); // invoked partial view from  Views/Shared/Components/DropDownList\Default.cshtml
        }
    }
}
