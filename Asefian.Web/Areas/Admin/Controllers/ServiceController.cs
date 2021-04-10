using Asefian.Model.Context;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.FileContext;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر خدمات
    /// </summary>
    [Authorize(Roles = "admin, service")]
    public class ServiceController : BaseController
    {
        /// <summary>
        /// صفحه لیست خدمات
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن خدمات
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "خدمات جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش خدمات
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش خدمات";
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
                AsefianFileContextHelper.DeleteFile(id, fileName, currentUser.id, "admin", "Service");
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست خدمات
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchServiceViewModel options)
        {
            try
            {
                var query = _context.Service.Where(x => x.StatusId != ServiceStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Sku.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchServiceViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    title = x.Sku,
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
        /// خواندن اطلاعات خدمات
        /// </summary>
        /// <param name="id">ردیف خدمات مورد نظر</param>
        /// <returns>نتیجه خواندن خدمات</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Service.Where(x => x.StatusId != ServiceStatus.Deleted.Id && x.Id == id).Select(x => new ServiceViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new ServiceTranslationViewModel()
                    {
                        languageId = y.LanguageId,
                        title = y.Title,
                        description = y.Description,
                        summary = y.Summary
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
        /// ذخیره خدمات
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات خدمات جهت ثبت</param>
        /// <returns>نتیجه ثبت خدمات به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(ServiceViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                //if (string.IsNullOrEmpty(model.title))
                //{
                //    return Error("وارد کردن عنوان اجباری است.");
                //}

                //if (string.IsNullOrEmpty(model.summary))
                //{
                //    return Error("وارد کردن خلاصه خدمات اجباری است.");
                //}

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.Service.Single(x => x.StatusId != ServiceStatus.Deleted.Id && x.Id == model.id);

                    if (Request.Files.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "service");
                        }

                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "service");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else if (string.IsNullOrEmpty(model.fileId) && !string.IsNullOrEmpty(entity.FileId))
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "service");
                        }
                    }

                    entity.Order = model.order;
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
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
                            entityItem.Summary = item.summary?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new ServiceTranslation()
                            {
                                ServiceId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian(),
                                Summary = item.summary?.ToStandardPersian()
                            };
                            _context.ServiceTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    //VerifyHtmlImageFile(model.description);

                    return Success("اطلاعات خدمات با موفقیت ویرایش شد.");
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
                        model.fileId = null;
                        model.fileName = null;
                    }

                    var entity = new Service()
                    {
                        Order = model.order,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
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

                    _context.Service.Add(entity);

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Description = item.description?.ToStandardPersian();
                            entityItem.Summary = item.summary?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new ServiceTranslation()
                            {
                                ServiceId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Description = item.description?.ToStandardPersian(),
                                Summary = item.summary?.ToStandardPersian(),
                            };
                            _context.ServiceTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    // تایید تمام فایل های جدید ذخیره شده در نقد و بررسی
                    //VerifyHtmlImageFile(model.description);

                    return Success("خدمات با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف خدمات
        /// </summary>
        /// <param name="id">ردیف خدمات</param>
        /// <returns>نتیجه حذف خدمات</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Service.Single(x => x.StatusId != ServiceStatus.Deleted.Id && x.Id == id);

                if (!string.IsNullOrEmpty(entity.FileId))
                {
                    AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "service");
                }

                //DeleteHtmlImageFile(entity.Description);

                entity.StatusId = ServiceStatus.Deleted.Id;
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