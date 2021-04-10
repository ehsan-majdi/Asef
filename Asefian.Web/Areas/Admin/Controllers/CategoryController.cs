using Asefian.Model.Common;
using Asefian.Model.Context;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.FileContext;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر دسته بندی
    /// </summary>
    [Authorize(Roles = "admin, category")]
    public class CategoryController : BaseController
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
        /// صفحه لیست دسته بندی به صورت درختی
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Tree()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن دسته بندی
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add(int? id)
        {
            ViewBag.Title = "دسته بندی جدید";
            ViewBag.ParentId = id;
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
                var query = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.TranslationList.Select(y => y.Title).Contains(word));
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
                    title = x.Sku,
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
                var entity = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.Id == id).Select(x => new CategoryViewModel()
                {
                    id = x.Id,
                    parentId = x.ParentId,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new CategoryTranslationViewModel()
                    {
                        languageId = y.LanguageId,
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
                if (model.translations.Count != Language.GetList().Count())
                {
                    return Error("ترجمه های وارد شده صحیح نیست.");
                }

                foreach (var item in model.translations)
                {
                    if (string.IsNullOrEmpty(item.title))
                    {
                        return Error(string.Format("وارد کردن عنوان در زبان {0} اجباری است.", Language.GetList().Single(x => x.Id == item.languageId).PersianTitle));
                    }
                }

                if (model.id != null && model.id > 0)
                {
                    if (model.id == model.parentId)
                    {
                        return Error("دسته بندی نمی تواند با خودش رابطه داشته باشد.");
                    }

                    var entity = _context.Category.Single(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.Id == model.id);


                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var contentTypes = new List<string>() { "image/x-png", "image/gif", "image/jpeg" };
                        //if (!contentTypes.Contains(file.ContentType))
                        //{
                        //    return Error("پسوند فایل انتخاب شده معتبر نیست.");
                        //}

                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            //AsefianFileContextHelper.DeleteFilePermanently(entity.FileId, entity.FileName);
                        }

                        var fileEntity = AsefianFileContextHelper.Save(file, currentUser.id, GetCurrentIp(), "admin", "category");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else if (string.IsNullOrEmpty(model.fileId) && !string.IsNullOrEmpty(entity.FileId))
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            //AsefianFileContextHelper.DeleteFilePermanently(entity.FileId, entity.FileName);
                        }
                    }

                    entity.ParentId = model.parentId;
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
                    entity.Order = model.order;
                    entity.FileId = model.fileId;
                    entity.FileName = model.fileName;
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
                            entityItem = new CategoryTranslation()
                            {
                                CategoryId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.CategoryTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات دسته بندی با موفقیت ویرایش شد.");
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var contentTypes = new List<string>() { "image/x-png", "image/gif", "image/jpeg" };
                        if (!contentTypes.Contains(file.ContentType))
                        {
                            return Error("پسوند فایل انتخاب شده معتبر نیست.");
                        }

                        var fileEntity = AsefianFileContextHelper.Save(file, currentUser.id, GetCurrentIp(), "admin", "category");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else
                    {
                        model.fileId = null;
                        model.fileName = null;
                    }

                    var entity = new Category()
                    {
                        ParentId = model.parentId,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        Order = model.order,
                        FileId = model.fileId,
                        FileName = model.fileName,
                        StatusId = model.statusId,
                        CategoryTypeId = CategoryType.Product.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.Category.Add(entity);

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
                            entityItem = new CategoryTranslation()
                            {
                                CategoryId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.CategoryTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    return Success(string.Format("دسته بندی \"{0}\" با موفقیت ایجاد شد.", entity.Sku));
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

                var entity = _context.Category.Single(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.Id == id);

                if (entity.ChildList.Count(x => x.StatusId != CategoryStatus.Deleted.Id) > 0)
                {
                    return Error("ابتدا دسته بندی های فرزند این دسته بندی را حذف کنید.");
                }

                if (entity.ProductList.Count(x => x.StatusId != ProductStatus.Deleted.Id) > 0)
                {
                    return Error("ابتدا محصولات این دسته بندی را حذف کنید.");
                }

                if (entity.ProductFilterList.Count(x => x.StatusId != ProductFilterStatus.Deleted.Id) > 0)
                {
                    return Error("ابتدا فیلترهایی که به این دسته بندی متصل هستند را حذف کنید.");
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
                var data = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.ParentId == null).OrderBy(x => x.Order).Select(x => new SelectOption()
                {
                    id = x.Id,
                    title = x.Sku
                }).ToList();

                return Success(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست دسته بندی های سطح دوم
        /// </summary>
        /// <returns>لیست دسته بندی ها</returns>
        [HttpGet]
        [Authorize(Roles = "admin, product, category, filter")]
        public JsonResult SelectChild()
        {
            try
            {
                var data = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.ParentId != null).OrderBy(x => x.Order).Select(x => new SelectOption()
                {
                    id = x.Id,
                    title = x.Parent.Sku + ": " + x.Sku
                }).ToList();

                return Success(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست دسته بندی های سطح دوم
        /// </summary>
        /// <param name="id">ردیف دسته بندی پدر</param>
        /// <returns>لیست دسته بندی ها</returns>
        [HttpGet]
        [Authorize(Roles = "admin, product, category, filter")]
        public JsonResult LoadChild(int? id)
        {
            try
            {
                var data = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.CategoryTypeId == CategoryType.Product.Id && x.ParentId == id).OrderBy(x => x.Order).Select(x => new
                {
                    id = x.Id,
                    title = x.Sku,
                    level = x.ParentId == null ? 0 : (x.Parent.ParentId == null ? 1 : 2),
                    children = x.ChildList.Count()
                }).ToList();

                return Success(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// صفحه ویژگی های تعریف شده برای هر دسته بندی
        /// </summary>
        /// <param name="id">ردیف دسته بندی</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Feature(int id)
        {
            var category = _context.Category.Where(x => x.StatusId != CategoryStatus.Deleted.Id && x.Id == id).Single();
            return BaseView(category);
        }

        /// <summary>
        /// صفحه ویژگی جدید
        /// </summary>
        /// <param name="id">ردیف دسته بندی</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult FeatureAdd(int id)
        {
            ViewBag.CategoryId = id;
            return BaseView("FeatureEdit");
        }

        /// <summary>
        /// صفحه ویرایش ویژگی
        /// </summary>
        /// <param name="id">ردیف</param>
        /// <returns>صفحه ویژگی مورد نظر</returns>
        [HttpGet]
        public ActionResult FeatureEdit(int id)
        {
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// مقدار ویژگی های دسته بندی
        /// </summary>
        /// <param name="id">ردیف</param>
        /// <returns>صفحه ویژگی مورد نظر</returns>
        [HttpGet]
        public ActionResult FeatureValue(int id)
        {
            var feature = _context.CategoryFeature.Single(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == id);
            return BaseView(feature);
        }

        /// <summary>
        /// دریافت لیست ویژگی ها
        /// به همراه پارامتر های جستجو
        /// </summary>
        /// <param name="options">پارامتر های مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult FeatureSearch(SearchCategoryFeatureViewModel options)
        {
            try
            {
                var query = _context.CategoryFeature.Where(x => x.StatusId != CategoryFeatureStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Sku.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchCategoryFeatureViewModel()
                {
                    id = x.Id,
                    title = x.Sku,
                    typeId = x.TypeId,
                    typeTitle = x.Type.PersianTitle,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                }).ToList();

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات ویژگی دسته بندی
        /// </summary>
        /// <param name="id">ردیف ویژگی دسته بندی مورد نظر</param>
        /// <returns>نتیجه خواندن محصول</returns>
        [HttpGet]
        public JsonResult FeatureLoad(int id)
        {
            try
            {
                var entity = _context.CategoryFeature.Where(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == id).Select(x => new CategoryFeatureViewModel()
                {
                    id = x.Id,
                    categoryId = x.CategoryId,
                    title = x.Sku,
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
        /// ثبت نام ویژگی دسته بندی
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات ویژگی دسته بندی جهت ثبت</param>
        /// <returns>نتیجه ثبت ویژگی دسته بندی به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult FeatureSave(CategoryFeatureViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن نام ویژگی دسته بندی محصول اجباری است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.CategoryFeature.Single(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == model.id);

                    entity.CategoryId = model.categoryId;
                    entity.Sku = model.title.ToStandardPersian();
                    entity.TypeId = model.typeId;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    return Success("اطلاعات ویژگی دسته بندی با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new CategoryFeature()
                    {
                        CategoryId = model.categoryId,
                        Sku = model.title.ToStandardPersian(),
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

                    return Success("ویژگی دسته بندی با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف ویژگی دسته بندی
        /// </summary>
        /// <param name="id">ردیف ویژگی دسته بندی</param>
        /// <returns>نتیجه حذف ویژگی دسته بندی</returns>
        [HttpPost]
        public JsonResult FeatureDelete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.CategoryFeature.Single(x => x.StatusId != CategoryFeatureStatus.Deleted.Id && x.Id == id);

                entity.StatusId = CategoryFeatureStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("ویژگی دسته بندی با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست مقادیر ویژگی دسته بندی
        /// </summary>
        /// <param name="id">ردیف ویژگی دسته بندی</param>
        /// <returns>نتیجه لیست مقادیر ویژگی دسته بندی</returns>
        [HttpGet]
        public JsonResult ValueList(int id)
        {
            try
            {
                var data = _context.CategoryFeatureValue.Where(x => x.CategoryFeatureId == id).OrderBy(x => x.Order).Select(x => new CategoryFeatureValueViewModel()
                {
                    id = x.Id,
                    categoryFeatureId = x.CategoryFeatureId,
                    order = x.Order,
                    title = x.Sku
                }).ToList();

                return SuccessSearch(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره لیست مقادیر ویژگی دسته بندی
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات ذخیره مقدار ویژگی دسته بندی ها</param>
        /// <returns>نتیجه ذخیره مقدار ها</returns>
        [HttpPost]
        public JsonResult SaveValueList(CategoryFeatureValueViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن عنوان اجباری است.");
                }

                var currentUser = GetAuthenticatedUser();

                int order = 1;
                var lastItem = _context.CategoryFeatureValue.Where(x => x.CategoryFeatureId == model.categoryFeatureId).OrderByDescending(x => x.Order).FirstOrDefault();
                if (lastItem != null) order = lastItem.Order + 1;

                var entity = new CategoryFeatureValue()
                {
                    Order = order,
                    Sku = model.title.ToStandardPersian(),
                    CategoryFeatureId = model.categoryFeatureId,
                    CreateUserId = currentUser.id,
                    ModifyUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    ModifyDate = GetDatetime(),
                    CreateIp = GetCurrentIp(),
                    ModifyIp = GetCurrentIp()
                };

                _context.CategoryFeatureValue.Add(entity);
                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر ترتیب مقدار ویژگی ها
        /// </summary>
        /// <param name="model">اطلاعات شامل مقدار ویژگی ها و ترتیب آن ها</param>
        /// <returns>نتیجه تغییر ترتیب مقادیر</returns>
        [HttpPost]
        public JsonResult RearrangeValueList(RearrangeCategoryFeatureItem model)
        {
            try
            {
                var list = _context.CategoryFeatureValue.Where(x => x.CategoryFeatureId == model.id).ToList();
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

        /// <summary>
        /// دریافت لیست ویژگی ها برای پر کردن کومبو
        /// </summary>
        /// <param name="id">ردیف دسته بندی</param>
        /// <returns>لیست ویژگی های دسته بندی</returns>
        [HttpGet]
        public JsonResult FeatureSelect(int id)
        {
            try
            {
                var data = _context.CategoryFeature.Where(x => x.StatusId == CategoryFeatureStatus.Active.Id && (x.CategoryId == id || x.Category.ParentId == id)).Select(x => new SelectOption()
                {
                    id = x.Id,
                    title = x.Sku
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