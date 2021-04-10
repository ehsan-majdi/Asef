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
    /// کنترلر ویژگی های دسته بندی
    /// </summary>
    public class CategoryFeatureController : BaseController
    {
        /// <summary>
        /// دریافت لیست ویژگی های دسته بندی ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchCategoryFeatureViewModel options)
        {
            try
            {
                var query = _context.CategoryFeature.Where(x => x.StatusId != CategoryFeatureStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Title.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchCategoryFeatureViewModel()
                {
                    id = x.Id,
                    title = x.Title,
                    typeId = x.TypeId,
                    typeTitle = x.Type.PersianTitle,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, options.page, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات ویژگی های دسته بندی
        /// </summary>
        /// <param name="id">ردیف ویژگی های دسته بندی مورد نظر</param>
        /// <returns>نتیجه خواندن ویژگی های دسته بندی</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.CategoryFeature.Where(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == id).Select(x => new CategoryFeatureViewModel()
                {
                    id = x.Id,
                    categoryId = x.CategoryId,
                    title = x.Title,
                    typeId = x.TypeId,
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
        /// ثبت نام ویژگی های دسته بندی
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات ویژگی های دسته بندی جهت ثبت</param>
        /// <returns>نتیجه ثبت ویژگی های دسته بندی به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(CategoryFeatureViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن نام ویژگی های دسته بندی اجباری است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.CategoryFeature.Single(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == model.id);
                    entity.CategoryId = model.categoryId;
                    entity.Title = model.title.ToStandardPersian();
                    entity.TypeId = model.typeId;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    return Success("اطلاعات ویژگی های دسته بندی با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new CategoryFeature()
                    {
                        CategoryId = model.categoryId,
                        Title = model.title.ToStandardPersian(),
                        TypeId = model.typeId,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.CategoryFeature.Add(entity);
                    _context.SaveChanges();

                    return Success("ویژگی های دسته بندی با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف ویژگی های دسته بندی
        /// </summary>
        /// <param name="id">ردیف ویژگی های دسته بندی</param>
        /// <returns>نتیجه حذف ویژگی های دسته بندی</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.CategoryFeature.Single(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == id);
                entity.StatusId = CategoryFeatureStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("ویژگی های دسته بندی با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}