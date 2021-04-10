using Asefian.Model.Context.Support;
using Asefian.Model.Context.Support.Enum;
using Asefian.Model.FileContext;
using Asefian.Model.ViewModel.Support;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر سوالات متداول
    /// </summary>
    [Authorize(Roles = "admin, faq")]
    public class FaqController : BaseController
    {
        /// <summary>
        /// صفحه لیست سوالات متداول
        /// </summary>
        /// <param name="id">ردیف دسته بندی سوالات مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List(int id)
        {
            ViewBag.FaqCategoryId = id;
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن سوال
        /// </summary>
        /// <param name="id">ردیف دسته بندی سوالات مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add(int id)
        {
            ViewBag.Title = "سوال جدید";
            ViewBag.FaqCategoryId = id;
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش سوال
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش سوال";
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
                AsefianFileContextHelper.DeleteFile(id, fileName, currentUser.id, "admin", "faq");
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست سوالات
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchFaqViewModel options)
        {
            try
            {
                var query = _context.Faq.Where(x => x.StatusId != FaqStatus.Deleted.Id && x.FaqCategoryId == options.faqCategoryId);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Question.Contains(word) || x.Answer.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchFaqViewModel()
                {
                    id = x.Id,
                    question = x.Question,
                    order = x.Order,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات سوال
        /// </summary>
        /// <param name="id">ردیف سوال مورد نظر</param>
        /// <returns>نتیجه خواندن سوال</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Faq.Where(x => x.StatusId != FaqStatus.Deleted.Id && x.Id == id).Select(x => new FaqViewModel()
                {
                    id = x.Id,
                    faqCategoryId = x.FaqCategoryId,
                    order = x.Order,
                    question = x.Question,
                    answer = x.Answer,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره سوال
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات سوال جهت ثبت</param>
        /// <returns>نتیجه ثبت سوال به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(FaqViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                if (string.IsNullOrEmpty(model.question))
                {
                    return Error("وارد کردن سوال اجباری است.");
                }

                if (string.IsNullOrEmpty(model.answer))
                {
                    return Error("وارد کردن پاسخ اجباری است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.Faq.Single(x => x.StatusId != FaqStatus.Deleted.Id && x.Id == model.id);
                    entity.FaqCategoryId = model.faqCategoryId;
                    entity.Order = model.order;
                    entity.Question = model.question.ToStandardPersian();
                    entity.Answer = model.answer.ToStandardPersian();
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    VerifyHtmlImageFile(model.answer);

                    return Success("اطلاعات سوال با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new Faq()
                    {
                        FaqCategoryId = model.faqCategoryId,
                        Order = model.order,
                        Question = model.question.ToStandardPersian(),
                        Answer = model.answer.ToStandardPersian(),
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.Faq.Add(entity);
                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    VerifyHtmlImageFile(model.answer);

                    return Success("سوال با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id">ردیف سوال</param>
        /// <returns>نتیجه حذف سوال</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Faq.Single(x => x.StatusId != FaqStatus.Deleted.Id && x.Id == id);

                DeleteHtmlImageFile(entity.Answer);

                entity.StatusId = FaqStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("سوال با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}