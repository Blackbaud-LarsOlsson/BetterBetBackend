using System.Web.Http;
using System.Web.Http.Cors;

namespace BetterBetsBackend.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            EnableCorsAttribute cors = new EnableCorsAttribute("http://example.com", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}