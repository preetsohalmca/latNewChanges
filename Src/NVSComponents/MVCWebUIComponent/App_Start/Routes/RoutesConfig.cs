using System.Web.Mvc;
using System.Web.Routing;

namespace Volvo.LAT.MVCWebUIComponent.Routes
{
    /// <summary>
    /// Configures all the routes registering them.
    /// </summary>
    public static class RoutesConfig
    {
        /// <summary>
        /// Registers the ASP .NET routes.
        /// </summary>
        /// <param name="routes">Collection of routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.log");

            // For learning how to add custom routing visit: http://www.asp.net/learn/mvc/tutorial-23-cs.aspx

            // Route for Bundling and Minification
            routes.MapRoute(
                "Bundling", // Route name
                "Utility/EnableBundlingAndMinification/{enable}", // URL with parameters
                new { controller = "Utility", action = "EnableBundlingAndMinification", enable = false }); // Parameter defaults

            // This is the default routing map e.g. when you type: /Order/ManageOrders you are directed
            // to the OrderController to Action method ManageOrders
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Dashboard", action = "Index", id = string.Empty }); // Parameter defaults
            routes.MapRoute(
                "poline-listing", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = string.Empty }); // Parameter defaults

            routes.MapRoute(
               "Purchase order Detail", // Route name
               "{controller}/{action}/{purchaseOrderId}", // URL with parameters
               new { controller = "PoLine", action = "GetPurchaseOrderDetail", purchaseOrderId = string.Empty }); // Parameter defaults

            // Add our route registration for MvcSiteMapProvider sitemaps
            MvcSiteMapProvider.Web.Mvc.XmlSiteMapController.RegisterRoutes(routes);
        }
    }
}