using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.ViewModel.Data;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر جستجو
    /// </summary>
    [AllowAnonymous]
    public class SearchController : BaseController
    {
        /// <summary>
        /// صفحه اصلی کنترلر جستجو
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(string term)
        {
            var langId = GetLanguage().Id;
            var query = _context.Product.Where(x => x.Code.Contains(term) || x.TranslationList.Select(z => z.Title).Contains(term) && x.StatusId != ProductStatus.Deleted.Id && x.StatusId != ProductStatus.Inactive.Id).Select(x => new ProductViewModel()
            {
                id = x.Id,
                sku = x.TranslationList.FirstOrDefault(z => z.LanguageId == langId).Title,
                code = x.Code,
                price = x.Price,
                fileId = x.FileId,
                fileName = x.FileName,
                statusId = x.StatusId,
            }).ToList();
            if (query.Count > 0)
            {
                ViewBag.Term = term;
            }
            else
            {
                ViewBag.Term = "موردی یافت نشد";
            }
            return View(query);
        }
    }
}