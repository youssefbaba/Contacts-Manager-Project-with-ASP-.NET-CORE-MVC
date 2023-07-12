using System.Text.RegularExpressions;

namespace RoutingDemo.CustomConstraints
{
    public class MonthsCustomConstraints : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // check wether the monthValue exists
            if (!values.ContainsKey(routeKey))  // routeKey => month
            {
                return false;  // not a match
            }

            Regex regex = new Regex("^(january|april|july|october)$");
            string? monthValue = Convert.ToString(values[routeKey]);
            if (regex.IsMatch(monthValue!))
            {
                return true; // it is a match
            }
            return false; // not a match
        }
    }
}
