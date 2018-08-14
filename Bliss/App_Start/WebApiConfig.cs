using Bliss.Handlers;
using Bliss.Negotiator;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Bliss
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(new JsonMediaTypeFormatter()));
            config.MessageHandlers.Add(new EncodingDelegateHandler());

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
