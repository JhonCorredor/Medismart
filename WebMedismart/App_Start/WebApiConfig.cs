using Microsoft.AspNetCore.Cors;
using System.Web.Http;

namespace WebMedismart
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            var cors = new EnableCorsAttribute("*");
            //config.
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
                //defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
