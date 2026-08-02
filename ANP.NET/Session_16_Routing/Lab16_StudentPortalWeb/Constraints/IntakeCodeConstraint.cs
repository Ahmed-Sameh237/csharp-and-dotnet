using System.Globalization;

namespace StudentPortalWeb.Constraints
{
    public class IntakeCodeConstraint : IRouteConstraint
    {
        public bool Match(
           HttpContext? httpContext,
           IRouter? route,
           string routeKey,
           RouteValueDictionary values,
           RouteDirection routeDirection)
        {
            
            if(!values.TryGetValue(routeKey, out var value) || value == null)
            {
                return false;
            }

            // No database access here.
            // A route constraint should only decide whether the URL matches.
            return string.Equals(value.ToString(), "itiC", StringComparison.OrdinalIgnoreCase);

        }

    }

}
