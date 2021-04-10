using Asefian.Common.Web;
using Asefian.Model.Context;
using Asefian.Model.Context.Core;
using Asefian.Model.Context.Core.Enum;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.FileContext;
using Asefian.Model.ViewModel.Core;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر پروژه های خاص صفحه اصلی
    /// </summary>
    [Authorize(Roles = "admin")]
    public class SpecialProjectController : BaseController
    {
        /// <summary>
        /// صفحه لیست پروژه های خاص
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن پروژه های خاص
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "پروژه های خاص جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش پروژه های خاص
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش پروژه های خاص";
            ViewBag.Id = id;
            return BaseView();
        }


        /// <summary>
        /// تصاویر محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Gallery(int id)
        {
            return View();
        }

        public ActionResult AddGallery(int id)
        {
            ViewBag.Id = id;
            var SpecialProject = _context.SpecialProject.SingleOrDefault(x => x.Id == id && x.StatusId != SpecialProjectStatus.Deleted.Id);
            return View(SpecialProject);
        }
        /// <summary>
        /// دریافت لیست پروژه های خاص ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search()
        {
            try
            {
                var query = _context.SpecialProject.Where(x => x.StatusId != SpecialProjectStatus.Deleted.Id);

                var data = query.Select(x => new SpecialProjectViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    sku = x.Sku,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    galleryCount = x.SpecialProjectFileList.Where(y => y.StatusId != SpecialProjectFileStatus.Deleted.Id).Count(),

                }).ToList();

                return SuccessSearch(data, 1, data.Count(), data.Count());
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات پروژه های خاص
        /// </summary>
        /// <param name="id">ردیف پروژه های خاص مورد نظر</param>
        /// <returns>نتیجه خواندن پروژه های خاص</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.SpecialProject.Where(x => x.StatusId != SpecialProjectStatus.Deleted.Id && x.Id == id).Select(x => new SpecialProjectViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new SpecialProjectTranslationViewModel()
                    {
                        specialProjectId = x.Id,
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
        /// ثبت نام پروژه های خاص
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات پروژه های خاص جهت ثبت</param>
        /// <returns>نتیجه ثبت پروژه های خاص به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(SpecialProjectViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.fileId) && Request.Files.Count == 0)
                {
                    return Error("وارد کردن فایل تصویر الزامی است.");
                }

                var currentUser = GetAuthenticatedUser();

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.SpecialProject.Single(x => x.StatusId != SpecialProjectStatus.Deleted.Id && x.Id == model.id);

                    if (Request.Files.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "mainPage");
                        }

                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "mainPage");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else if (string.IsNullOrEmpty(model.fileId) && !string.IsNullOrEmpty(entity.FileId))
                    {
                        if (!string.IsNullOrEmpty(entity.FileId))
                        {
                            AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "mainPage");
                        }
                    }

                    entity.Order = model.order;
                    entity.FileId = model.fileId;
                    entity.FileName = model.fileName;
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian();
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
                            entityItem.Description = item.description.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new SpecialProjectTranslation()
                            {
                                SpecialProjectId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title,
                                Description = item.description.ToStandardPersian(),
                            };
                            _context.SpecialProjectTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات پروژه های خاص با موفقیت ویرایش شد.");
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {
                        var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "mainPage");

                        model.fileId = fileEntity.id;
                        model.fileName = fileEntity.fileName;
                    }
                    else
                    {
                        model.fileId = null;
                        model.fileName = null;
                    }

                    var entity = new SpecialProject()
                    {
                        Order = model.order,
                        FileId = model.fileId,
                        FileName = model.fileName,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.SpecialProject.Add(entity);
                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Description = item.description.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new SpecialProjectTranslation()
                            {
                                SpecialProjectId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title,
                                Description = item.description.ToStandardPersian(),
                            };
                            _context.SpecialProjectTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    return Success("پروژه های خاص با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف پروژه های خاص
        /// </summary>
        /// <param name="id">ردیف پروژه های خاص</param>
        /// <returns>نتیجه حذف پروژه های خاص</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.SpecialProject.Single(x => x.StatusId != SpecialProjectStatus.Deleted.Id && x.Id == id);

                if (!string.IsNullOrEmpty(entity.FileId))
                {
                    AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "mainPage");
                }

                entity.StatusId = SpecialProjectStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("پروژه های خاص با موفقیت حذف شد.");
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
        public JsonResult SpecialProjectImageList(int id)
        {
            try
            {
                var imageList = _context.SpecialProjectFile.Where(x => x.SpecialProjectId == id && x.StatusId != SpecialProjectFileStatus.Deleted.Id)
                    .Select(x => new SpecialProjectFileViewModel()
                    {
                        id = x.Id,
                        specialProjectId = x.SpecialProjectId,
                        sku = x.Sku,
                        fileName = x.FileName,
                        fileId = x.FileId,
                        link = "/upload/image/500x500/" + x.FileId + "/" + x.FileName
                    }).ToList();

                var searchResponse = new SearchResponse()
                {
                    list = imageList,
                    specialProjectId = id,
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
        public JsonResult AddImage(AddSpecialProjectFileViewModel model)
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
                        var entity = new SpecialProjectFile()
                        {
                            SpecialProjectId = model.id,
                            FileName = data.fileName,
                            Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                            FileId = data.id,
                            StatusId = SpecialProjectFileStatus.Active.Id,
                            CreateUserId = currentUser.id,
                            ModifyUserId = currentUser.id,
                            CreateDate = GetDatetime(),
                            ModifyDate = GetDatetime(),
                            CreateIp = GetCurrentIp(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.SpecialProjectFile.Add(entity);
                        model.translations.ForEach(item =>
                        {
                            var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                            if (entityItem != null)
                            {
                                entityItem.Title = item.title.ToStandardPersian();
                                entityItem.Description = item.description.ToStandardPersian();
                            }
                            else
                            {
                                entityItem = new SpecialProjectFileTranslation()
                                {
                                    SpecialProjectFileId = entity.Id,
                                    LanguageId = item.languageId,
                                    Title = item.title,
                                    Description = item.description,
                                };
                                _context.SpecialProjectFileTranslation.Add(entityItem);
                            }
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


        //public JsonResult AddSpecialProjectImage(AddSpecialProjectImageViewModel model)
        //{
        //    var currentUser = GetAuthenticatedUser();
        //    try
        //    {
        //        var entity = _context.SpecialProject.Single(x => x.Id == model.id);

        //        if (Request.Files.Count > 0)
        //        {
        //            if (!string.IsNullOrEmpty(entity.FileId))
        //            {
        //                AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "SpecialProject");
        //            }

        //            var fileEntity = AsefianFileContextHelper.Save(Request.Files[0], currentUser.id, GetCurrentIp(), "admin", "SpecialProject");

        //            model.SpecialProjectFileId = fileEntity.id;
        //            model.SpecialProjectFileName = fileEntity.fileName;
        //        }
        //        else if (string.IsNullOrEmpty(model.SpecialProjectFileId) && !string.IsNullOrEmpty(entity.FileId))
        //        {
        //            if (!string.IsNullOrEmpty(entity.FileId))
        //            {
        //                AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "SpecialProject");
        //            }
        //        }
        //        entity.FileId = model.SpecialProjectFileId;
        //        entity.FileName = model.SpecialProjectFileName;
        //        _context.SaveChanges();
        //        var data = new
        //        {
        //            id = entity.Id,
        //            fileName = entity.FileName,
        //            fileId = entity.FileId
        //        };
        //        return Success("Done", data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}

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
                var file = _context.SpecialProjectFile.Single(x => x.Id == id);
                if (file != null)
                {
                    AsefianFileContextHelper.DeleteFile(file.FileId, file.FileName, currentUser.id);

                    file.StatusId = SpecialProjectFileStatus.Deleted.Id;
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

    }
}