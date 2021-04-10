using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Asefian.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
               name: "Login",
               url: "{lang}/login",
               defaults: new { lang = "fa", controller = "user", action = "login" },
               namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Export",
               url: "{lang}/export",
               defaults: new { lang = "fa", controller = "home", action = "export" },
               namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
              name: "Solicitorship",
              url: "{lang}/solicitorship",
              defaults: new { lang = "fa", controller = "home", action = "solicitorship" },
              namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
              name: "SpecialProject",
              url: "{lang}/specialProject",
              defaults: new { lang = "fa", controller = "specialProject", action = "index" },
              namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
              name: "FractionSpecialProject",
              url: "{lang}/fractionspecialproject/{id}",
              defaults: new { lang = "fa", controller = "specialProject", action = "fractionSpecialProject" },
              namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
              name: "FractionNews",
              url: "{lang}/fractionNews/{id}",
              defaults: new { lang = "fa", controller = "news", action = "fractionNews" },
              namespaces: new string[] { "Asefian.Web.Controllers" }
            );

            routes.MapRoute(
             name: "ContactUs",
             url: "{lang}/contactus",
             defaults: new { lang = "fa", controller = "contactus", action = "index" },
             namespaces: new string[] { "Asefian.Web.Controllers" }
           );

            routes.MapRoute(
            name: "Shop",
            url: "{lang}/shop",
            defaults: new { lang = "fa", controller = "shop", action = "index" },
            namespaces: new string[] { "Asefian.Web.Controllers" }
          );

            routes.MapRoute(
                name: "ResizeImage",
                url: "upload/image/resize/{size}/{fileId}/{fileName}",
                defaults: new { controller = "file", action = "resizeImage" },
                namespaces: new string[] { "Farkom.Web.Controllers" }
            );
            routes.MapRoute(
                name: "CropImage",
                url: "upload/image/crop/{size}/{fileId}/{fileName}",
                defaults: new { controller = "file", action = "cropImage" },
                namespaces: new string[] { "Farkom.Web.Controllers" }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "Asefian.Web.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },
                defaults: new { lang = "fa", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Asefian.Web.Controllers" }
            );
        }
    }
}
