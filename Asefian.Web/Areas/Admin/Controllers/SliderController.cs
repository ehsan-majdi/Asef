using Asefian.Model.Context;
using Asefian.Model.Context.Core;
using Asefian.Model.Context.Core.Enum;
using Asefian.Model.FileContext;
using Asefian.Model.ViewModel.Core;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر اسلایدر صفحه اصلی
    /// </summary>
    [Authorize(Roles = "admin, mainPage")]
    public class SliderController : BaseController
    {
        /// <summary>
        /// صفحه لیست اسلایدر
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن اسلایدر
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "اسلایدر جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش اسلایدر
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش اسلایدر";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست اسلایدر ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search()
        {
            try
            {
                var query = _context.Slider.Where(x => x.StatusId != SliderStatus.Deleted.Id);

                var data = query.Select(x => new SliderViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    text = x.Sku,
                    link = x.Link,
                    statusId = x.StatusId,
                    typeId=x.TypeId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, 1, data.Count(), data.Count());
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات اسلایدر
        /// </summary>
        /// <param name="id">ردیف اسلایدر مورد نظر</param>
        /// <returns>نتیجه خواندن اسلایدر</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Slider.Where(x => x.StatusId != SliderStatus.Deleted.Id && x.Id == id).Select(x => new SliderViewModel()
                {
                    id = x.Id,
                    order = x.Order,
                    fileId = x.FileId,
                    fileName = x.FileName,
                    link = x.Link,
                    statusId = x.StatusId,
                    typeId=x.TypeId,
                    statusTitle = x.Status.PersianTitle,
                    translations = x.TranslationList.Select(y => new SliderTranslationViewModel()
                    {
                        sliderId =y.Id,
                        languageId = y.LanguageId,
                        text = y.Text,
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
        /// ثبت نام اسلایدر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات اسلایدر جهت ثبت</param>
        /// <returns>نتیجه ثبت اسلایدر به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(SliderViewModel model)
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
                    var entity = _context.Slider.Single(x => x.StatusId != SliderStatus.Deleted.Id && x.Id == model.id);

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
                    entity.Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).text.ToStandardPersian();
                    entity.Link = model.link;
                    entity.StatusId = model.statusId;
                    entity.TypeId = model.typeId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Text = item.text.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new SliderTranslation()
                            {
                                SliderId = entity.Id,
                                LanguageId = item.languageId,
                                Text = item.text.ToStandardPersian(),
                            };
                            _context.SliderTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success("اطلاعات اسلایدر با موفقیت ویرایش شد.");
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

                    var entity = new Slider()
                    {
                        Order = model.order,
                        FileId = model.fileId,
                        FileName = model.fileName,
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).text.ToStandardPersian(),
                        Link = model.link,
                        StatusId = model.statusId,
                        TypeId=model.typeId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.Slider.Add(entity);
                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Text = item.text.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new SliderTranslation()
                            {
                                SliderId = entity.Id,
                                LanguageId = item.languageId,
                                Text = item.text.ToStandardPersian(),
                            };
                            _context.SliderTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();

                    return Success("اسلایدر با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف اسلایدر
        /// </summary>
        /// <param name="id">ردیف اسلایدر</param>
        /// <returns>نتیجه حذف اسلایدر</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Slider.Single(x => x.StatusId != SliderStatus.Deleted.Id && x.Id == id);

                if (!string.IsNullOrEmpty(entity.FileId))
                {
                    AsefianFileContextHelper.DeleteFile(entity.FileId, entity.FileName, currentUser.id, "admin", "mainPage");
                }

                entity.StatusId = SliderStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("اسلایدر با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}