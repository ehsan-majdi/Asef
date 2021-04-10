using Asefian.Web.Controllers;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر داشبورد
    /// </summary>
    public class DashboardController : BaseController
    {
        /// <summary>
        /// صفحه اصلی داشبورد و آمار
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return BaseView();
        }
    }
}