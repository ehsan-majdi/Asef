using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر مشاهده محصولات به عبارتی گالری
    /// </summary>
    [AllowAnonymous]
    public class ShopController : BaseController
    {
        /// <summary>
        /// صفحه اصلی فروشگاه
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return BaseView();
        }
    }
}