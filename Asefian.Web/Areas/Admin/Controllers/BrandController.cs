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
    /// کنترلر برند ها
    /// </summary>
    [Authorize(Roles = "admin, brand")]
    public class BrandController : BaseController
    {
        /// <summary>
        /// صفحه لیست برند
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن برند
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "برند جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش برند
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش برند";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست برند ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchBrandViewModel options)
        {
            try
            {
                var query = _context.Brand.Where(x => x.StatusId != BrandStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.TranslationList.Select(y => y.Title).Contains(word) || x.TranslationList.Select(y => y.Description).Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchBrandViewModel()
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
        /// خواندن اطلاعات برند
        /// </summary>
        /// <param name="id">ردیف برند مورد نظر</param>
        /// <returns>نتیجه خواندن برند</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Brand.Where(x => x.StatusId != BrandStatus.Deleted.Id && x.Id == id).Select(x => new BrandViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new BrandTranslationViewModel()
                    {
                        languageId = y.LanguageId,
                        title = y.Title,
                        description = y.Description
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
        /// ثبت نام برند
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات برند جهت ثبت</param>
        /// <returns>نتیجه ثبت برند به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(BrandViewModel model)
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
                    var entity = _context.Brand.Single(x => x.StatusId != BrandStatus.Deleted.Id && x.Id == model.id);

                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var contentTypes = new List<string>() { "image/x-png", "image/gif", "image/jpeg" };
                        if (!contentTypes.Contains(file.ContentType))
                        {
                            return Error("پسوند فایل انتخاب شده معتبر نیست.");
                        }

                        //if (!string.IsNullOrEmpty(entity.FileId))
                        //{
                        //    AsefianFileContextHelper.DeleteFilePermanently(entity.FileId, entity.FileName);
                        //}

                        var fileEntity = AsefianFileContextHelper.Save(file, currentUser.id, GetCurrentIp(), "admin", "category");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else if (string.IsNullOrEmpty(model.fileId) && !string.IsNullOrEmpty(entity.FileId))
                    {
                        //if (!string.IsNullOrEmpty(entity.FileId))
                        //{
                        //    AsefianFileContextHelper.DeleteFilePermanently(entity.FileId, entity.FileName);
                        //}
                    }

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
                            entityItem = new BrandTranslation()
                            {
                                BrandId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.BrandTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات برند با موفقیت ویرایش شد.");
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {
                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "brand");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else
                    {
                        model.fileId = null;
                        model.fileName = null;
                    }

                    var entity = new Brand()
                    {
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        Order = model.order,
                        FileId = model.fileId,
                        FileName = model.fileName,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.Brand.Add(entity);

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
                            entityItem = new BrandTranslation()
                            {
                                BrandId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian()
                            };
                            _context.BrandTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    return Success("برند با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف برند
        /// </summary>
        /// <param name="id">ردیف برند</param>
        /// <returns>نتیجه حذف برند</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Brand.Single(x => x.StatusId != BrandStatus.Deleted.Id && x.Id == id);

                if (!string.IsNullOrEmpty(entity.FileId))
                {
                    AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "brand");
                }

                entity.StatusId = BrandStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("برند با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست برند ها
        /// </summary>
        /// <returns>لیست برند ها</returns>
        [HttpGet]
        [Authorize(Roles = "admin, brand, product")]
        public JsonResult Select()
        {
            try
            {
                var data = _context.Brand.Where(x => x.StatusId != BrandStatus.Deleted.Id).OrderBy(x => x.Order).Select(x => new SelectOption()
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
