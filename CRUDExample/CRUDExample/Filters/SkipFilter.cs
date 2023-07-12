using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters
{
    public class SkipFilter : Attribute, IFilterMetadata   // Acts as an Attribute and Filter at the same time
    {
    }
}
