using Asefian.Model.Common;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر دسته بندی
    /// </summary>
    [Authorize(Roles = "admin, article")]
    public class ArticleCategoryController : BaseController
    {
        /// <summary>
        /// صفحه لیست دسته بندی
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن دسته بندی
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "دسته بندی جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش دسته بندی
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
        public JsonResult Search(SearchCategoryViewModel options)
        {
            try
            {
                var query = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Article.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Title.Contains(word) || x.EnglishTitle.Contains(word));
                }

                if (options.parentId != null && options.parentId > 0)
                {
                    query = query.Where(x => x.ParentId == options.parentId);
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchCategoryViewModel()
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
                var entity = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Article.Id && x.Id == id).Select(x => new CategoryViewModel()
                {
                    id = x.Id,
                    parentId = x.ParentId,
                    title = x.Title,
                    englishTitle = x.EnglishTitle,
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
        public JsonResult Save(CategoryViewModel model)
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
                    if (model.id == model.parentId)
                    {
                        return Error("دسته بندی نمی تواند با خودش رابطه داشته باشد.");
                    }

                    var entity = _context.Category.Single(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Article.Id && x.Id == model.id);
                    entity.ParentId = model.parentId;
                    entity.Title = model.title.ToStandardPersian();
                    entity.EnglishTitle = model.englishTitle.ToStandardPersian();
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
                    var entity = new Category()
                    {
                        ParentId = model.parentId,
                        Title = model.title.ToStandardPersian(),
                        EnglishTitle = model.englishTitle,
                        Order = model.order,
                        StatusId = model.statusId,
                        CategoryTypeId = CategoryType.Article.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.Category.Add(entity);
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

                var entity = _context.Category.Single(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Article.Id && x.Id == id);

                if (entity.ChildList.Count(x => x.StatusId != CategoryStatus.Deleted.Id) > 0)
                {
                    return Error("ابتدا دسته بندی های فرزند این دسته بندی را حذف کنید.");
                }

                entity.StatusId = CategoryStatus.Deleted.Id;
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

        /// <summary>
        /// دریافت لیست دسته بندی های سطح اول
        /// </summary>
        /// <returns>لیست دسته بندی ها</returns>
        [HttpGet]
        public JsonResult Select()
        {
            try
            {
                var data = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Article.Id && x.ParentId == null).OrderBy(x => x.Order).Select(x => new SelectOption()
                {
                    id = x.Id,
                    title = x.Title
                }).ToList();

                return Success(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}