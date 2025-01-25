using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRental.Helpers // Pass på at namespace stemmer overens med prosjektet ditt!
{
    public static class HtmlHelperExtensions
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller)
        {
            var currentController = htmlHelper.ViewContext.RouteData.Values["Controller"]?.ToString();
            return currentController == controller ? "active" : "";
        }
    }
}
