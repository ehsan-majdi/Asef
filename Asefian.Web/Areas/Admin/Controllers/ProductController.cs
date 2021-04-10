using Asefian.Common.Web;
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
    /// کنترلر محصولات
    /// </summary>
    [Authorize(Roles = "admin, product")]
    public class ProductController : BaseController
    {
        /// <summary>
        /// صفحه لیست محصول
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن محصول
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "محصول جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش محصول
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش محصول";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Filters(int id)
        {
            ViewBag.Id = id;
            var product = _context.Product.Single(x => x.Id == id);
            var filterList = _context.ProductFilter.Where(x => x.StatusId != ProductFilterStatus.Deleted.Id && x.CategoryId == product.CategoryId).OrderBy(x => x.Order).Select(x => new ProductFilterViewModel()
            {
                id = x.Id,
                categoryId = x.CategoryId,
                order = x.Order,
                filterTypeId = x.FilterTypeId,
                title = x.Sku,
                statusId = x.StatusId,
                statusTitle = x.Status.PersianTitle,
                valueList = x.ValueList.Where(y => y.StatusId != ProductFilterValueStatus.Deleted.Id).OrderBy(y => y.Order).Select(y => new ProductFilterValueViewModel()
                {
                    id = y.Id,
                    productFilterId = y.ProductFilterId,
                    order = y.Order,
                    value = y.Sku,
                    statusId = y.StatusId,
                    statusTitle = y.Status.PersianTitle
                }).ToList()
            }).ToList();
            ViewBag.FilterList = filterList;
            return View(product);
        }

        /// <summary>
        /// تصاویر محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Gallery(int id)
        {
            ViewBag.Id = id;
            var product = _context.Product.Single(x => x.Id == id && x.StatusId != ProductStatus.Deleted.Id);
            return View(product);
        }

        /// <summary>
        /// نقد و بررسی تخصصی
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Review(int id)
        {
            ViewBag.Id = id;
            var product = _context.Product.Single(x => x.Id == id);
            return View(product);
        }

        /// <summary>
        /// فیلتر محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه مورد نظر</returns>
        //[HttpGet]
        //public ActionResult Filters(int id)
        //{
        //    ViewBag.Id = id;
        //    var product = _context.Product.Single(x => x.Id == id);
        //    var filterList = _context.ProductFilter.Where(x => x.StatusId != ProductFilterStatus.Deleted.Id && x.CategoryId == product.Category.ParentId).OrderBy(x => x.Order).Select(x => new ProductFilterViewModel()
        //    {
        //        id = x.Id,
        //        categoryId = x.CategoryId,
        //        order = x.Order,
        //        filterTypeId = x.FilterTypeId,
        //        title = x.Sku,
        //        statusId = x.StatusId,
        //        statusTitle = x.Status.PersianTitle,
        //        valueList = x.ValueList.Where(y => y.StatusId != ProductFilterValueStatus.Deleted.Id).OrderBy(y => y.Order).Select(y => new ProductFilterValueViewModel()
        //        {
        //            id = y.Id,
        //            productFilterId = y.ProductFilterId,
        //            order = y.Order,
        //            value = y.Value,
        //            statusId = y.StatusId,
        //            statusTitle = y.Status.PersianTitle
        //        }).ToList()
        //    }).ToList();
        //    ViewBag.FilterList = filterList;
        //    return View(product);
        //}

        /// <summary>
        /// دریافت لیست محصول ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchProductViewModel options)
        {
            try
            {
                var query = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id);
                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();

                    query = query.Where(x => x.Sku.Contains(word) || x.Code.Contains(word));
                }
                if (options.hasPhoto != null && options.hasPhoto.Value == true)
                {
                    query = query.Where(x => x.FileId != null && x.FileName != null);
                }
                if (options.hasPhoto != null && options.hasPhoto.Value == false)
                {
                    query = query.Where(x => x.FileId == null && x.FileName == null);
                }


                if (options.hasFilter != null && options.hasFilter.Value == true)
                {
                    query = query.Where(x => x.ProductFilterDataList.Count > 0);
                }
                if (options.hasFilter != null && options.hasFilter.Value == false)
                {
                    query = query.Where(x => x.ProductFilterDataList.Count == 0);
                }
                if (options.statusId != null && options.statusId > 0)
                {
                    query = query.Where(x => x.StatusId == options.statusId);
                }

                var count = query.Count();
                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchProductViewModel()
                {
                    id = x.Id,
                    title=x.Sku,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    count = x.Count + (((int?)x.ProductFeatureList.Sum(y => y.Count)) ?? 0),
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    filterCount = x.ProductFilterDataList.Count,
                    galleryCount = x.ProductFileList.Where(y => y.StatusId != ProductFileStatus.Deleted.Id && y.TypeId == ProductFileType.Picture.Id).Count(),
                    discount = x.Discount,
                    price = x.Price
                }).ToList();
                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات محصول
        /// </summary>
        /// <param name="id">ردیف محصول مورد نظر</param>
        /// <returns>نتیجه خواندن محصول</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var aa = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == id).SingleOrDefault();
                var entity = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == id).Select(x => new ProductViewModel()
                {
                    id = x.Id,
                    categoryId = x.CategoryId,
                    brandId = x.BrandId,
                    code = x.Code,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    price = x.Price,
                    width=x.Width,
                    height=x.Height,
                    discount = x.Discount,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new ProductTranslationViewModel()
                    {
                        languageId = y.LanguageId,
                        productId = y.ProductId,
                        title = y.Title,
                        description = y.Description,
                        review = y.Review
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
        /// خواندن نقد و بررسی محصول
        /// </summary>
        /// <param name="id">ردیف محصول مورد نظر</param>
        /// <returns>متن نقد و بررسی</returns>
        [HttpGet]
        public JsonResult LoadReview(int id)
        {
            try
            {
                var entity = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == id).Select(x => new ProductReviewViewModel()
                {
                    id = x.Id,
                    review = x.TranslationList.Where(y => y.LanguageId == Language.Persian.Id).Select(y => y.Review).FirstOrDefault(),
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت نام محصول
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات محصول جهت ثبت</param>
        /// <returns>نتیجه ثبت محصول به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(ProductViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
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
                    var entity = _context.Product.Single(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == model.id);

                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var contentTypes = new List<string>() { "image/x-png", "image/gif", "image/jpeg" };
                        if (!contentTypes.Contains(file.ContentType))
                        {
                            return Error("پسوند فایل انتخاب شده معتبر نیست.");
                        }

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

                    entity.CategoryId = model.categoryId;
                    entity.CategoryFeatureId = model.categoryFeatureId;
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
                    entity.BrandId = model.brandId;
                    entity.Code = model.code.ToStandardPersian();
                    entity.FileId = model.fileId;
                    entity.FileName = model.fileName;
                    entity.Price = model.price;
                    entity.Width = model.width;
                    entity.Height = model.height;
                    entity.Discount = model.discount;
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
                            entityItem = new ProductTranslation()
                            {
                                ProductId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian(),
                                Review = item.review
                            };
                            _context.ProductTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات محصول با موفقیت ویرایش شد.");
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

                    var entity = new Product()
                    {
                        CategoryId = model.categoryId,
                        CategoryFeatureId = model.categoryFeatureId,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        BrandId = model.brandId,
                        Code = model.code.ToStandardPersian(),
                        FileId = model.fileId,
                        FileName = model.fileName,
                        Price = model.price,
                        Width=model.width,
                        Height=model.height,
                        Discount = model.discount,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.Product.Add(entity);

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Description = item.description?.ToStandardPersian();
                            entityItem.Review = item.review;
                        }
                        else
                        {
                            entityItem = new ProductTranslation()
                            {
                                ProductId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian(),
                                Review = item.review
                            };
                            _context.ProductTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("محصول با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>نتیجه حذف محصول</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Product.Single(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == id);

                if (!string.IsNullOrEmpty(entity.FileId))
                {
                    AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "product");
                }

                entity.StatusId = ProductStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("محصول با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// لیست تصاویر محصولات
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>لیست تصاویر ثبت شده برای محصول</returns>
        [HttpGet]
        public JsonResult ProductImageList(int id)
        {
            try
            {
                var imageList = _context.ProductFile.Where(x => x.ProductId == id && x.StatusId != ProductFileStatus.Deleted.Id && x.TypeId == ProductFileType.Picture.Id)
                    .Select(x => new ProductFileViewModel()
                    {
                        id = x.Id,
                        productId = x.ProductId,
                        fileName = x.FileName,
                        fileId = x.FileId,
                        link = "/upload/file/" + x.FileId + "/" + x.FileName
                    }).ToList();

                var searchResponse = new SearchResponse()
                {
                    list = imageList,
                    productId = id,
                    page = 1,
                    count = imageList.Count,
                    total = imageList.Count,
                    totalPage = 1,
                    orderBy = "id",
                    orderType = "desc"
                };

                return Success(searchResponse);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره فایل محصول در سرور
        /// </summary>
        /// <returns>نتیجه ذخیره فایل</returns>
        [HttpPost]
        public JsonResult AddImage(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var data = AsefianFileContextHelper.Save(file, currentUser.id, GetCurrentIp());
                    if (data != null)
                    {
                        _context.ProductFile.Add(new ProductFile()
                        {
                            ProductId = id,
                            FileName = data.fileName,
                            FileId = data.id,
                            TypeId = ProductFileType.Picture.Id,
                            StatusId = ProductFileStatus.Active.Id,

                            CreateUserId = currentUser.id,
                            ModifyUserId = currentUser.id,
                            CreateDate = GetDatetime(),
                            ModifyDate = GetDatetime(),
                            CreateIp = GetCurrentIp(),
                            ModifyIp = GetCurrentIp()
                        });

                        AsefianFileContextHelper.VerifyFile(data.id, data.fileName);
                        _context.SaveChanges();

                        return Success("فایل با موفقیت ذخیره شد.", data);
                    }
                    else
                    {
                        return Error("فایلی جهت ذخیره یافت نشد.");
                    }
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


        public JsonResult AddProductImage(AddProductImageViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                var entity = _context.Product.Single(x => x.Id == model.id);

                if (Request.Files.Count > 0)
                {
                    if (!string.IsNullOrEmpty(entity.FileId))
                    {
                        AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "product");
                    }

                    var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "product");

                    model.productFileId = fileEntity.id;
                    model.productFileName = fileEntity.fileName;
                }
                else if (string.IsNullOrEmpty(model.productFileId) && !string.IsNullOrEmpty(entity.FileId))
                {
                    if (!string.IsNullOrEmpty(entity.FileId))
                    {
                        AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "Product");
                    }
                }
                entity.FileId = model.productFileId;
                entity.FileName = model.productFileName;
                _context.SaveChanges();
                var data = new
                {
                    id = entity.Id,
                    fileName = entity.FileName,
                    fileId = entity.FileId
                };
                return Success("Done", data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف یک فایل محصول
        /// </summary>
        /// <param name="id">ردیف فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns>نتیجه حذف</returns>
        [HttpPost]
        public JsonResult RemoveGalleryFile(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var file = _context.ProductFile.Single(x => x.Id == id);
                if (file != null)
                {
                    AsefianFileContextHelper.DeleteFile(file.FileId, file.FileName, currentUser.id);

                    file.StatusId = ProductFileStatus.Deleted.Id;
                    file.ModifyUserId = currentUser.id;
                    file.ModifyDate = GetDatetime();
                    file.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();
                }
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت مقدار های ثبت شده برای محصولات
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>مقدار های ثبت شده برای هر محصول به تفکیک فیلترهای محصول</returns>
        [HttpGet]
        public JsonResult GetFilterValue(int id)
        {
            try
            {
                var data = _context.ProductFilterData.Where(x => x.ProductId == id).GroupBy(x => x.ProductFilterId).Select(x => new ProductFilterDataViewModel()
                {
                    productFilterId = x.Key,
                    valueList = x.Select(y => new ProductFilterDataValueViewModel()
                    {
                        productFilterValueId = y.ProductFilterValueId,
                        value = y.Value,
                        valueBoolean = y.ValueBoolean
                    }).ToList()
                }).ToList();

                return Success(data);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره مقدارهای فیلترهای محصولات
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات ذخیره مقدار های فیلتر ها برای محصولات</param>
        /// <returns>نتیجه ذخیره مقدار های فیلتر ها</returns>
        [HttpPost]
        public JsonResult SetFilterValue(int id, List<ProductFilterDataViewModel> model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var filterList = _context.ProductFilterData.Where(x => x.ProductId == id).ToList();
                foreach (var item in model)
                {
                    var entity = filterList.Where(x => x.ProductFilterId == item.productFilterId).ToList();
                    if (entity.Count() > 0)
                    {
                        if (item.valueList != null && item.valueList.Count > 0)
                        {
                            foreach (var itemValue in item.valueList)
                            {
                                if (entity.Count(x => x.ProductFilterValueId == itemValue.productFilterValueId) > 0)
                                {
                                    var entityItem = entity.Single(x => x.ProductFilterValueId == itemValue.productFilterValueId);
                                    entityItem.ProductFilterValueId = itemValue.productFilterValueId;
                                    entityItem.Value = itemValue.value;
                                    entityItem.ValueBoolean = itemValue.valueBoolean;

                                    entityItem.ModifyUserId = currentUser.id;
                                    entityItem.ModifyDate = GetDatetime();
                                    entityItem.ModifyIp = GetCurrentIp();
                                }
                                else
                                {
                                    _context.ProductFilterData.Add(new ProductFilterData()
                                    {
                                        ProductId = id,
                                        ProductFilterId = item.productFilterId,
                                        ProductFilterValueId = itemValue.productFilterValueId,
                                        Value = itemValue.value,
                                        ValueBoolean = itemValue.valueBoolean,
                                        CreateUserId = currentUser.id,
                                        ModifyUserId = currentUser.id,
                                        CreateDate = GetDatetime(),
                                        ModifyDate = GetDatetime(),
                                        CreateIp = GetCurrentIp(),
                                        ModifyIp = GetCurrentIp()
                                    });
                                }
                            }

                            var removeList = new List<ProductFilterData>();
                            foreach (var itemValue in entity)
                            {
                                if (item.valueList.Count(x => x.productFilterValueId == itemValue.ProductFilterValueId) <= 0)
                                {
                                    removeList.Add(itemValue);
                                }
                            }
                            if (removeList.Count > 0)
                                _context.ProductFilterData.RemoveRange(removeList);
                        }
                        else
                        {
                            _context.ProductFilterData.RemoveRange(entity);
                        }
                    }
                    else
                    {
                        if (item.valueList != null && item.valueList.Count > 0)
                        {
                            foreach (var itemValue in item.valueList)
                            {
                                _context.ProductFilterData.Add(new ProductFilterData()
                                {
                                    ProductId = id,
                                    ProductFilterId = item.productFilterId,
                                    ProductFilterValueId = itemValue.productFilterValueId,
                                    Value = itemValue.value,
                                    ValueBoolean = itemValue.valueBoolean,
                                    CreateUserId = currentUser.id,
                                    ModifyUserId = currentUser.id,
                                    CreateDate = GetDatetime(),
                                    ModifyDate = GetDatetime(),
                                    CreateIp = GetCurrentIp(),
                                    ModifyIp = GetCurrentIp()
                                });
                            }
                        }
                    }
                }

                _context.SaveChanges();
                return Success("اطلاعات با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن نقد و بررسی محصول
        /// </summary>
        /// <param name="id">ردیف محصول مورد نظر</param>
        /// <returns>متن نقد و بررسی</returns>
        //[HttpGet]
        //public JsonResult LoadReview(int id)
        //{
        //    try
        //    {
        //        var entity = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == id).Select(x => new ProductReviewViewModel()
        //        {
        //            id = x.Id,
        //            review = x.Review
        //        }).Single();

        //        return Success(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}

        /// <summary>
        /// ذخیره نقد و بررسی محصول
        /// </summary>
        /// <param name="model">ردیف محصول مورد نظر</param>
        /// <returns>نتیجه ذخیره نقد و بررسی</returns>
        //[HttpPost]
        //public JsonResult SaveReview(ProductReviewViewModel model)
        //{
        //    try
        //    {
        //        var currentUser = GetAuthenticatedUser();
        //        var entity = _context.Product.Single(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == model.id);

        //        entity.Review = model.review.ToStandardPersian();
        //        entity.ModifyUserId = currentUser.id;
        //        entity.ModifyDate = GetDatetime();
        //        entity.ModifyIp = GetCurrentIp();

        //        // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
        //        VerifyHtmlImageFile(model.review);

        //        _context.SaveChanges();

        //        return Success("اطلاعات با موفقیت ذخیره شد.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}

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
                AsefianFileContextHelper.DeleteFile(id, fileName, currentUser.id);
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست محصولات برای انتخاب
        /// </summary>
        /// <param name="word">عبارتی که جستجو بر اساس آن انجام شود</param>
        /// <returns>نتیجه لیست محصولات</returns>
        //[HttpGet]
        //[Authorize(Roles = "admin, product, documentEntry, documentExit")]
        //public JsonResult GetProductAutoComplete(string word)
        //{
        //    try
        //    {
        //        var query = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id);

        //        if (!string.IsNullOrEmpty(word))
        //        {
        //            word = word.ToStandardPersian();
        //            query = query.Where(x => x.Title.Contains(word) || x.AlternativeTitle.Contains(word) || x.Code.Contains(word));
        //        }

        //        var data = query.OrderByDescending(x => x.Id)
        //        .Take(10)
        //        .Select(x => new ProductAutoCompleteViewModel
        //        {
        //            id = x.Id,
        //            title = x.Title,
        //            code = x.Code,
        //            price = x.Price,
        //            statusId = x.StatusId,
        //            statusTitle = x.Status.PersianTitle,
        //            featureList = x.CategoryFeature.ValueList.OrderBy(y => y.Order).Select(y => new ProductFeatureAutoCompleteViewModel
        //            {
        //                id = y.Id,
        //                productId = x.Id,
        //                title = y.Title,
        //                order = y.Order,
        //                price = x.ProductFeatureList.FirstOrDefault(z => z.CategoryFeatureValueId == y.Id).Price
        //            }).ToList()
        //        }).ToList();

        //        return Success(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}

        /// <summary>
        /// دریافت لیست محصولات انبار برای انتخاب
        /// </summary>
        /// <param name="word">عبارتی که جستجو بر اساس آن انجام شود</param>
        /// <returns>نتیجه لیست محصولات</returns>
        //[HttpGet]
        //[Authorize(Roles = "admin, product, documentEntry, documentExit")]
        //public JsonResult GetProductInventoryAutoComplete(string word)
        //{
        //    try
        //    {
        //        var query = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && (x.Count > 0 || x.ProductFeatureList.Any(y => y.Count > 0)));

        //        if (!string.IsNullOrEmpty(word))
        //        {
        //            word = word.ToStandardPersian();
        //            query = query.Where(x => x.Title.Contains(word) || x.AlternativeTitle.Contains(word) || x.Code.Contains(word));
        //        }

        //        var data = query.OrderByDescending(x => x.Id)
        //        .Take(10)
        //        .Select(x => new ProductAutoCompleteViewModel
        //        {
        //            id = x.Id,
        //            title = x.Title,
        //            code = x.Code,
        //            price = x.Price,
        //            count = x.Count,
        //            statusId = x.StatusId,
        //            statusTitle = x.Status.PersianTitle,
        //            featureList = x.ProductFeatureList.OrderBy(y => y.CategoryFeatureValue.Order).Select(y => new ProductFeatureAutoCompleteViewModel
        //            {
        //                id = y.Id,
        //                productId = x.Id,
        //                title = y.CategoryFeatureValue.Title,
        //                order = y.CategoryFeatureValue.Order,
        //                price = y.Price,
        //                count = y.Count
        //            }).ToList()
        //        }).ToList();

        //        return Success(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}

        /// <summary>
        /// تغییر وضعیت محصول
        /// </summary>
        /// <param name="model">ردیف معصول و ردیف وضعیت مورد نظر جهت تغییر</param>
        /// <returns>تغییر وضعیت محصول</returns>
        public JsonResult ChangeStatus(ChangeProductStatusViewModel model)
        {
            try
            {
                var entity = _context.Product.Single(x => x.Id == model.id);
                entity.StatusId = model.statusId;
                _context.SaveChanges();
                return Success();
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}