using Asefian.Model.Context.Support;
using Asefian.Model.Context.Support.Enum;
using Asefian.Model.ViewModel.Support;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر دسته بندی سوالات متداول
    /// </summary>
    [Authorize(Roles = "admin, faq")]
    public class FaqCategoryController : BaseController
    {
        /// <summary>
        /// صفحه لیست دسته بندی سوالات متداول
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن دسته بندی سوالات متداول
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "دسته بندی جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش دسته بندی سوالات متداول
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش دسته بندی";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست دسته بندی ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchFaqCategoryViewModel options)
        {
            try
            {
                var query = _context.FaqCategory.Where(x => x.StatusId != FaqCategoryStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Title.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchFaqCategoryViewModel()
                {
                    id = x.Id,
                    title = x.Title,
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
        /// خواندن اطلاعات دسته بندی
        /// </summary>
        /// <param name="id">ردیف دسته بندی مورد نظر</param>
        /// <returns>نتیجه خواندن دسته بندی</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.FaqCategory.Where(x => x.StatusId != FaqCategoryStatus.Deleted.Id && x.Id == id).Select(x => new FaqCategoryViewModel()
                {
                    id = x.Id,
                    title = x.Title,
                    order = x.Order,
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
        /// ذخیره دسته بندی
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات دسته بندی جهت ثبت</param>
        /// <returns>نتیجه ثبت دسته بندی به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(FaqCategoryViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن نام دسته بندی اجباری است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.FaqCategory.Single(x => x.StatusId != FaqCategoryStatus.Deleted.Id && x.Id == model.id);
                    entity.Title = model.title.ToStandardPersian();
                    entity.Order = model.order;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    return Success("اطلاعات دسته بندی با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new FaqCategory()
                    {
                        Title = model.title.ToStandardPersian(),
                        Order = model.order,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.FaqCategory.Add(entity);
                    _context.SaveChanges();

                    return Success("دسته بندی با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف دسته بندی
        /// </summary>
        /// <param name="id">ردیف دسته بندی</param>
        /// <returns>نتیجه حذف دسته بندی</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.FaqCategory.Single(x => x.StatusId != FaqCategoryStatus.Deleted.Id && x.Id == id);

                if (entity.FaqList.Count(x => x.StatusId != FaqStatus.Deleted.Id) > 0)
                {
                    return Error("ابتدا سوالات این دسته بندی را حذف کنید.");
                }

                entity.StatusId = FaqCategoryStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("دسته بندی با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}