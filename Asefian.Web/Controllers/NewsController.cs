using Asefian.Model.Context.Blog.Enum;
using Asefian.Model.ViewModel.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    public class NewsController : BaseController
    {
        /// <summary>
        /// صفحه اصلی اخبار 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {

            var langId = GetLanguage().Id;
            var query = _context.NewsTranslation.Where(x => x.News.StatusId == NewsStatus.Published.Id && x.News.StatusId != NewsStatus.Deleted.Id && x.News.StatusId != NewsStatus.Draft.Id && x.LanguageId == langId && x.News.PublishDate <= DateTime.Today).OrderByDescending(x => x.Id).ToList();
            var newsList = query.OrderByDescending(x => x.Id).Select(x => new NewsViewModel()
            {
                id = x.Id,
                title = x.Title,
                fileId = x.News.FileId,
                fileName = x.News.FileName
            }).ToList();
            return View(newsList);
        }
        /// <summary>
        /// مشاهده هر اخبار در صفحه مربوط به خودش برای نمایش به کاربر به عبارتی جز به جز همه اخبار را در صفحات خاص به کاربر نمایش می دهد
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult FractionNews(int id)
        {
            var langId = GetLanguage().Id;
            var newsList = _context.NewsTranslation.Single(x => x.NewsId == id && x.News.StatusId == NewsStatus.Published.Id && x.News.StatusId != NewsStatus.Deleted.Id && x.LanguageId == langId);
            return View(newsList);
        }
    }
}