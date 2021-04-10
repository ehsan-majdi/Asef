using Asefian.Web.Controllers;
using Asefian.Common.Utility;
using Asefian.Model.Context.Blog.Enum;
using Asefian.Model.ViewModel.Blog;
using System;
using System.Linq;
using System.Web.Mvc;
using Asefian.Model.FileContext;
using Nig.Model.Context.Blog;
using Asefian.Model.Context.Blog;
using Asefian.Model.Context;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر اخبار
    /// </summary>
    [Authorize(Roles = "admin, news")]
    public class NewsController : BaseController
    {
        /// <summary>
        /// لیست اخبار
        /// </summary>
        /// <returns>لیست اخبار</returns>
        public ActionResult List()
        {
            return BaseView();
        }


        /// <summary>
        /// صفحه اضافه کردن خبر
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "خبر جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش خبر
        /// </summary>
        /// <param name="id">ردیف خبر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش خبر";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// ذخیره فایل در سرور
        /// </summary>
        /// <returns>نتیجه ذخیره فایل</returns>
        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    var data = AsefianFileContextHelper.Save(file, currentUser.id, GetCurrentIp());

                    return Success("فایل با موفقیت ذخیره شد.", data);
                }
                else
                {
                    return Error("فایلی برای ذخیره یافت نشد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف یک فایل
        /// </summary>
        /// <param name="id">ردیف فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns>نتیجه حذف</returns>
        [HttpPost]
        public JsonResult RemoveFile(string id, string fileName)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                AsefianFileContextHelper.DeleteFile(id, fileName, currentUser.id, "admin", "news");
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست خبرها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchNewsViewModel options)
        {
            try
            {
                var query = _context.News.Where(x => x.StatusId != NewsStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.TranslationList.Select(y => y.Title).Contains(word) || x.TranslationList.Select(y => y.AbstractText).Contains(word) || x.TranslationList.Select(y => y.Text).Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchNewsViewModel()
                {
                    id = x.Id,
                    title = x.Sku,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    view = x.View.Value,
                    publishDate = x.PublishDate,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                data.ForEach(x => x.PersianPublishDate = DateUtility.GetPersianDate(x.publishDate));

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات خبر
        /// </summary>
        /// <param name="id">ردیف خبر مورد نظر</param>
        /// <returns>نتیجه خواندن خبر</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.News.Where(x => x.StatusId != NewsStatus.Deleted.Id && x.Id == id).Select(x => new NewsViewModel()
                {
                    id = x.Id,

                    fileId = x.FileId,
                    fileName = x.FileName,
                    view = x.View.Value,
                    publishDate = x.PublishDate,
                    expiredDate = x.ExpiredDate,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new NewsTranslationViewModel()
                    {
                        languageId = y.LanguageId,
                        title = y.Title,
                        abstractText = y.AbstractText,
                        text = y.Text,
                        keywords = y.Keywords,
                    }).ToList()
                }).Single();

                entity.PersianPublishDate = DateUtility.GetPersianDate(entity.publishDate);
                entity.PersianExpiredDate = DateUtility.GetPersianDate(entity.expiredDate);

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره خبر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات خبر جهت ثبت</param>
        /// <returns>نتیجه ثبت خبر به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(NewsViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                DateTime? expiredDate = null;
                if (!string.IsNullOrEmpty(model.PersianExpiredDate))
                    expiredDate = DateUtility.GetDateTime(model.PersianExpiredDate);

                if (model.id != null && model.id > 0)
                {
                    if (string.IsNullOrEmpty(model.fileId) && Request.Files.Count == 0)
                    {
                        return Error("وارد کردن تصویر الزامی است.");
                    }

                    var entity = _context.News.Single(x => x.StatusId != NewsStatus.Deleted.Id && x.Id == model.id);

                    if (Request.Files.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "news");
                        }

                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "news");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else if (string.IsNullOrEmpty(model.fileId) && !string.IsNullOrEmpty(entity.FileId))
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "news");
                        }
                    }

                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
                    entity.FileId = model.fileId;
                    entity.FileName = model.fileName;
                    entity.PublishDate = string.IsNullOrEmpty(model.PersianPublishDate) ? GetDatetime() : DateUtility.GetDateTime(model.PersianPublishDate);
                    entity.ExpiredDate = expiredDate;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title?.ToStandardPersian();
                            entityItem.AbstractText = item.abstractText?.ToStandardPersian();
                            entityItem.Text = item.text?.ToStandardPersian();
                            entityItem.Keywords = item.keywords?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new NewsTranslation()
                            {
                                NewsId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                AbstractText = item.abstractText?.ToStandardPersian(),
                                Text = item.text?.ToStandardPersian(),
                                Keywords = item.keywords?.ToStandardPersian(),
                            };
                            _context.NewsTranslation.Add(entityItem);
                        }
                    });


                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    //VerifyHtmlImageFile(entity.Text);

                    return Success("اطلاعات خبر با موفقیت ویرایش شد.");
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {
                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "service");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else
                    {
                        return Error("وارد کردن تصویر خبر الزامی است.");
                    }

                    var entity = new News()
                    {
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        FileId = model.fileId,
                        FileName = model.fileName,
                        View = model.view,
                        PublishDate = string.IsNullOrEmpty(model.PersianPublishDate) ? GetDatetime() : DateUtility.GetDateTime(model.PersianPublishDate),
                        ExpiredDate = expiredDate,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.News.Add(entity);
                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title?.ToStandardPersian();
                            entityItem.AbstractText = item.abstractText?.ToStandardPersian();
                            entityItem.Text = item.text?.ToStandardPersian();
                            entityItem.Keywords = item.keywords?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new NewsTranslation()
                            {
                                NewsId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                AbstractText = item.abstractText?.ToStandardPersian(),
                                Text = item.text?.ToStandardPersian(),
                                Keywords = item.keywords?.ToStandardPersian(),
                            };
                            _context.NewsTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    //VerifyHtmlImageFile(entity.Text);

                    return Success("خبر با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف خبر
        /// </summary>
        /// <param name="id">ردیف خبر</param>
        /// <returns>نتیجه حذف خبر</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.News.Single(x => x.StatusId != NewsStatus.Deleted.Id && x.Id == id);

                //DeleteHtmlImageFile(entity.Text);

                entity.StatusId = NewsStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("خبر با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}