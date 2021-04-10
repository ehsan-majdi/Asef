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
    /// کنترلر شعبه ها
    /// </summary>
    [Authorize(Roles = "admin, branch")]
    public class BranchController : BaseController
    {
        /// <summary>
        /// صفحه لیست شعب
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن شعب
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "شعبه جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش شعب
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش شعبه";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست شعبه ها
        /// به وسیله فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchBranchViewModel options)
        {
            try
            {
                var query = _context.Branch.Where(x => x.StatusId != BranchStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Title.Contains(word) || x.Address.Contains(word) || x.Email.Contains(word) || x.Tel.Contains(word) || x.Fax.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Select(x => new ResponseSearchBranchViewModel()
                {
                    id = x.Id,
                    title = x.Title,
                    order = x.Order,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, 1, count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات شعبه
        /// </summary>
        /// <param name="id">ردیف شعبه مورد نظر</param>
        /// <returns>نتیجه خواندن شعبه</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Branch.Where(x => x.StatusId != BranchStatus.Deleted.Id && x.Id == id).Select(x => new BranchViewModel()
                {
                    id = x.Id,
                    sku = x.Sku,
                    order = x.Order,
                    address = x.Address,
                    email = x.Email,
                    tel = x.Tel,
                    fax = x.Fax,
                    latitude = x.Latitude,
                    longitude = x.Longitude,
                    statusId = x.StatusId
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره شعبه
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات شعبه جهت ذخیره</param>
        /// <returns>نتیجه ثبت شعبه به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(BranchViewModel model)
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
                    var entity = _context.Branch.Single(x => x.Id == model.id);

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
                            entityItem.Address = item.address?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new BranchTranslation()
                            {
                                BranchId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Address = item.address?.ToStandardPersian()
                            };
                            _context.BranchTranslation.Add(entityItem);
                        }
                    });
                    _context.SaveChanges();
                    return Success(string.Format("اطلاعات دسته بندی \"{0}\" با موفقیت ویرایش شد.", entity.Sku));
                }
                else
                {
                    var entity = new Branch()
                    {
                        Sku = model.translations.Single(x => x.languageId == Language.Persian.Id).title.ToStandardPersian(),
                        Order = model.order,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.Branch.Add(entity);
                    model.translations.ForEach(item =>
                    {
                        var entityItem = entity.TranslationList.SingleOrDefault(x => x.LanguageId == item.languageId);
                        if (entityItem != null)
                        {
                            entityItem.Title = item.title.ToStandardPersian();
                            entityItem.Address = item.address?.ToStandardPersian();
                        }
                        else
                        {
                            entityItem = new BranchTranslation()
                            {
                                BranchId = entity.Id,
                                LanguageId = item.languageId,
                                Title = item.title.ToStandardPersian(),
                                Address = item.address?.ToStandardPersian()
                            };
                            _context.BranchTranslation.Add(entityItem);
                        }
                    });

                    _context.SaveChanges();

                    return Success(string.Format("شعبه \"{0}\" با موفقیت ایجاد شد.", entity.Sku));
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف شعبه
        /// </summary>
        /// <param name="id">ردیف شعبه</param>
        /// <returns>نتیجه حذف شعبه</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Branch.Single(x => x.StatusId != BranchStatus.Deleted.Id && x.Id == id);
                entity.StatusId = BranchStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("شعبه با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}