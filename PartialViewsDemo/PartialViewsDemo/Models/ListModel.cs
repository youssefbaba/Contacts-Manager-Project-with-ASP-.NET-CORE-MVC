using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PartialViewsDemo.Models
{
    public class ListModel
    {
        public string ListTitle { get; set; } = string.Empty;

        public List<string> ListItems { get; set; } = new List<string>();
    }
}
