using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StockPrice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // attribute based routing
            config.MapHttpAttributeRoutes();

            // convention based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{ticker}",
                defaults: new { ticker = RouteParameter.Optional }
            );
        }
    }
}
