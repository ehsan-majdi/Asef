using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
              name: "Admin_Product_Filter",
              url: "admin/product/filter/{action}/{id}",
              defaults: new { controller = "productFilter", action = "list", id = UrlParameter.Optional },
              namespaces: new string[] { "Asefian.Web.Areas.Admin.Controllers" }
          );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}