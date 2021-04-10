using Asefian.Web.Controllers;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر تنظیمات صفحه اصلی
    /// </summary>
    public class MainPageController : BaseController
    {
        public ActionResult Index()
        {
            return BaseView();
        }
    }
}