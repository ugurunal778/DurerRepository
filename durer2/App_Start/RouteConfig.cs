using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace durer2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                 name: "Contact",
                 url: "iletisim",
                 defaults: new { controller = "Home", action = "iletisim" }
             );

            routes.MapRoute(
                 name: "Contact2",
                 url: "contact",
                 defaults: new { controller = "Home", action = "contact" }
             );

            routes.MapRoute(
                 name: "News",
                 url: "haberler",
                 defaults: new { controller = "Home", action = "haberler" }
             );


            routes.MapRoute(
                 name: "News2",
                 url: "news",
                 defaults: new { controller = "Home", action = "news" }
             );

            routes.MapRoute(
                 name: "Press",
                 url: "basin",
                 defaults: new { controller = "Home", action = "basin" }
             );

            routes.MapRoute(
                 name: "Press2",
                 url: "press",
                 defaults: new { controller = "Home", action = "press" }
             );

            routes.MapRoute(
                 name: "Catalogs",
                 url: "katalog",
                 defaults: new { controller = "Home", action = "katalog" }
             );

            routes.MapRoute(
                 name: "Catalogs2",
                 url: "catalog",
                 defaults: new { controller = "Home", action = "catalog" }
             );

            routes.MapRoute(
                 name: "Services",
                 url: "hizmetler",
                 defaults: new { controller = "Home", action = "hizmetler" }
             );

            routes.MapRoute(
                 name: "Services2",
                 url: "services",
                 defaults: new { controller = "Home", action = "services" }
             );
            routes.MapRoute(
                 name: "Application",
                 url: "basvuru",
                 defaults: new { controller = "Home", action = "basvuru" }
             );

            routes.MapRoute(
                 name: "MSDS",
                 url: "msds",
                 defaults: new { controller = "Home", action = "msds" }
             );
            routes.MapRoute(
                 name: "Supplier",
                 url: "supplier",
                 defaults: new { controller = "Home", action = "supplier" }
             );

            routes.MapRoute(
                name: "Page",
                url: "{permalink}",
                defaults: new { controller = "Home", action = "Page", permalink = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
