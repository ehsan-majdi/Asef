using Asefian.Model.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر درباره ما
    /// </summary>
    [AllowAnonymous]
    public class AboutUsController : BaseController
    {
        /// <summary>
        /// صفحه اصلی درباره ما
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var langId = GetLanguage().Id;
            var variable = new string[] {
                BaseInformationKey.AboutUs,
            };

            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key) && x.LanguageId == langId).Single();
            return View(data);
        }
    }
}