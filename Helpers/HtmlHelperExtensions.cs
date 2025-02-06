using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRental.Helpers 
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
