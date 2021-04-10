using Asefian.Model.Context.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر دسته بندی
    /// </summary>
    public class CategoryController : BaseController
    {
        /// <summary>
        /// لیست محصولات بر اساس دسته بندی
        /// </summary>
        /// <returns>صفحه موردنظر</returns>
        [Route("category/{id}/{title}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            var category = _context.Category.Where(x => x.Id == id && x.StatusId == CategoryStatus.Active.Id && x.CategoryTypeId == CategoryType.Product.Id).SingleOrDefault();
            return BaseView(category);
        }
    }
}