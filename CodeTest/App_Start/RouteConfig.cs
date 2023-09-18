using System.Web.Mvc;
using System.Web.Routing;

namespace PruebaIngreso
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Cambio de la accion de prueba2 a Test2 para conectar las vistas
            routes.MapRoute("Test2", "Home/Test2", new { controller = "Home", action = "Test2" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
