using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Application;

namespace RobotsAtWar.WebApi
{
    public class Swagger
    {
        public void Configure(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "SwaggerUI",
                routeTemplate: "api/swagger",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(RootUrlResolver, "api/swagger/ui/index"));

            configuration
                .EnableSwagger(c =>
                {
                    c.RootUrl(RootUrlResolver);
                    c.IncludeXmlComments("RobotsAtWar.WebApi.xml");
                    c.SingleApiVersion("v1", "RobotsAtWar API");

                    c.ApiKey("ticket").In("header");
                })
                .EnableSwaggerUi("api/swagger/ui/{*assetPath}");
        }

        private static string RootUrlResolver(HttpRequestMessage message)
        {
            return "http://pctombasl1:1235";
        }
    }
}