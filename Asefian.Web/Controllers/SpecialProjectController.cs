using Asefian.Model.Context.Data.Enum;
using Asefian.Model.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر پروژه های خاص
    /// </summary>
    [AllowAnonymous]
    public class SpecialProjectController : BaseController
    {
        /// <summary>
        /// صفحه اصلی پروژه های خاص
        /// </summary>
        /// <returns></returns>
        // GET: SpecialProject
        [AllowAnonymous]
        public ActionResult Index()
        {
            var langId = GetLanguage().Id;
            var query = _context.SpecialProjectTranslation.Where(x => x.SpecialProject.StatusId != SpecialProjectStatus.Deleted.Id && x.LanguageId==langId).OrderByDescending(x => x.Id).ToList();
            var specialProjectList = query.OrderByDescending(x => x.Id).Select(x => new SpecialProjectViewModel()
            {
                id=x.Id,
                title=x.Title,
                fileId=x.SpecialProject.FileId,
                fileName=x.SpecialProject.FileName
            }).ToList();
            return View(specialProjectList);
        }

        /// <summary>
        /// مشاهده هر پروژه در صفحه مربوط به خودش برای نمایش به کاربر به عبارتی جز به جز همه پروژه های خاص را در صفحات خاص به کاربر نمایش می دهد
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult FractionSpecialProject(int id)
        {
            var specialProjectFile = _context.SpecialProjectFile.Where(x => x.SpecialProjectId == id && x.StatusId != SpecialProjectFileStatus.Deleted.Id && x.StatusId != SpecialProjectFileStatus.Inactive.Id).ToList();
            return View(specialProjectFile);
        }

    }
}