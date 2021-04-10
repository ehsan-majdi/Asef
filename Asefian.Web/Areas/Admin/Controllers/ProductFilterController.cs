using Asefian.Model.Context;
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
    /// کنترلر فیلتر ها
    /// </summary>
    [Authorize(Roles = "admin, filter")]
    public class ProductFilterController : BaseController
    {
        /// <summary>
        /// صفحه لیست فیلتر های ثبت شده در سیستم
        /// </summary>
        /// <returns>صحفه مورد نشر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن فیلتر
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "فیلتر جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش فیلتر
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش فیلتر";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// لیست مقادیر فیلترها
        /// </summary>
        /// <param name="id">ردیف مقادیر فیلتر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Value(int id)
        {
            ViewBag.Id = id;
            var filter = _context.ProductFilter.Single(x => x.Id == id);
            return View(filter);
        }

        /// <summary>
        /// دریافت لیست فیلتر ها
        /// به همراه پارامتر های جستجو
        /// </summary>
        /// <param name="options">پارامتر های مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchProductFilterViewModel options)
        {
            try
            {
                var query = _context.ProductFilter.Where(x => x.StatusId != ProductFilterStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Sku.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchProductFilterViewModel()
                {
                    id = x.Id,
                    categoryId = x.CategoryId,
                    categoryTitle = x.Category.Sku,
                    title = x.Sku,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    filterTypeId = x.FilterTypeId,
                    filterTypeTitle = x.FilterType.PersianTitle
                }).ToList();

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف فیلتر محصول مورد نظر</param>
        /// <returns>نتیجه خواندن محصول</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.ProductFilter.Where(x => x.StatusId != ProductFilterStatus.Deleted.Id && x.Id == id).Select(x => new ProductFilterViewModel()
                {
                    id = x.Id,
                    categoryId = x.CategoryId,
                    order = x.Order,
                    filterTypeId = x.FilterTypeId,
                    filterTypeTitle = x.FilterType.PersianTitle,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new ProductFilterTranslationViewModel()
                    {
                        languageId = y.LanguageId,
                        productFilterId = y.ProductFilterId,
                        title = y.Title,
                        description = y.Description,
                    }).ToList()
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت نام فیلتر محصول
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات فیلتر محصول جهت ثبت</param>
        /// <returns>نتیجه ثبت فیلتر محصول به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(ProductFilterViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                //if (string.IsNullOrEmpty(model.title))
                //{
                //    return Error("وارد کردن نام فیلتر محصول اجباری است.");
                //}

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.ProductFilter.Single(x => x.StatusId != ProductFilterStatus.Deleted.Id && x.Id == model.id);

                    entity.CategoryId = model.categoryId;
                    entity.Order = model.order;
                    entity.FilterTypeId = model.filterTypeId;
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
                    entity.Order = model.order;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Description = item.description?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new ProductFilterTranslation()
                            {
                                ProductFilterId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.ProductFilterTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات فیلتر محصول با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new ProductFilter()
                    {
                        CategoryId = model.categoryId,
                        Order = model.order,
                        FilterTypeId = model.filterTypeId,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.ProductFilter.Add(entity);

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Description = item.description?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new ProductFilterTranslation()
                            {
                                ProductFilterId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.ProductFilterTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    return Success("فیلتر محصول با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف فیلتر محصول</param>
        /// <returns>نتیجه حذف فیلتر محصول</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.ProductFilter.Single(x => x.StatusId != ProductFilterStatus.Deleted.Id && x.Id == id);

                entity.StatusId = ProductFilterStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("فیلتر محصول با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست مقادیر فیلتر محصولات
        /// </summary>
        /// <param name="id">ردیف فیلتر محصولات</param>
        /// <returns>نتیجه لیست مقادیر فیلتر محصولات</returns>
        [HttpGet]
        public JsonResult ValueList(int id)
        {
            try
            {
                var data = _context.ProductFilterValue.Where(x => x.StatusId != ProductFilterValueStatus.Deleted.Id && x.ProductFilterId == id).OrderBy(x => x.Order).Select(x => new ProductFilterValueViewModel()
                {
                    id = x.Id,
                    productFilterId = x.ProductFilterId,
                    order = x.Order,
                    value = x.Sku,
                    statusId = x.StatusId,
                    statusTitle = x.Status.Title,
                }).ToList();

                return SuccessSearch(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره لیست مقادیر فیلتر محصولات
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات ذخیره مقدار فیلتر ها</param>
        /// <returns>نتیجه ذخیره مقدار ها</returns>
        [HttpPost]
        public JsonResult SaveValueList(ProductFilterValueViewModel model)
        {
            try
            {
                //if (string.IsNullOrEmpty(model.value))
                //{
                //    return Error("وارد کردن عنوان اجباری است.");
                //}

                var currentUser = GetAuthenticatedUser();

                int order = 1;
                var lastItem = _context.ProductFilterValue.Where(x => x.StatusId != ProductFilterValueStatus.Deleted.Id && x.ProductFilterId == model.productFilterId).OrderByDescending(x => x.Order).FirstOrDefault();
                if (lastItem != null) order = lastItem.Order + 1;

                var entity = new ProductFilterValue()
                {
                    ProductFilterId = model.productFilterId,
                    Order = order,
                    Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                    StatusId = ProductFilterValueStatus.Active.Id,
                    CreateUserId = currentUser.id,
                    ModifyUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    ModifyDate = GetDatetime(),
                    CreateIp = GetCurrentIp(),
                    ModifyIp = GetCurrentIp()
                };

                _context.ProductFilterValue.Add(entity);

                model.translations.ForEach(item =>
                {
                    var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                    if (entityItem != null)
                    {
                        entityItem.Value = item.title;
                    }
                    else
                    {
                        entityItem = new ProductFilterValueTranslation()
                        {
                            ProductFilterValueId = entity.Id,
                            LanguageId = item.languageId,
                            Value = item.title
                        };
                        _context.ProductFilterValueTranslation.Add(entityItem);
                    }
                });

                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فعال کردن مقدار فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف مقدار فیلتر محصول</param>
        /// <returns>نتیجه فعال کردن مقدار فیلتر محصول</returns>
        [HttpPost]
        public JsonResult ActiveFilterValue(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.ProductFilterValue.Single(x => x.StatusId != ProductFilterValueStatus.Deleted.Id && x.Id == id);

                entity.StatusId = ProductFilterValueStatus.Active.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("مقدار مورد نظر با موفقیت فعال شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// غیرفعال کردن مقدار فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف مقدار فیلتر محصول</param>
        /// <returns>نتیجه غیرفعال کردن مقدار فیلتر محصول</returns>
        [HttpPost]
        public JsonResult InactiveFilterValue(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.ProductFilterValue.Single(x => x.StatusId != ProductFilterValueStatus.Deleted.Id && x.Id == id);

                entity.StatusId = ProductFilterValueStatus.Inactive.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("مقدار مورد نظر با موفقیت غیر فعال شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر ترتیب مقدار فیلتر ها
        /// </summary>
        /// <param name="model">اطلاعات شامل مقدار فیلتر ها و ترتیب آن ها</param>
        /// <returns>نتیجه تغییر ترتیب مقادیر</returns>
        [HttpPost]
        public JsonResult RearrangeValueList(RearrangeFilterItem model)
        {
            try
            {
                var list = _context.ProductFilterValue.Where(x => x.StatusId != ProductFilterValueStatus.Deleted.Id && x.ProductFilterId == model.id).ToList();
                foreach (var item in list)
                {
                    item.Order = model.valueList.Single(x => x.id == item.Id).order;
                }

                _context.SaveChanges();
                return Success("ترتیب با موفقیت تغییر کرد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}