using Asefian.Common.Utility;
using Asefian.Common.Web;
using Asefian.Model.Context;
using Asefian.Model.Context.Financial;
using Asefian.Model.Context.Financial.Enum;
using Asefian.Model.ViewModel.Account;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر کوپن ادمین
    /// </summary>
    [Authorize(Roles = "admin, coupon")]
    public class CouponController : BaseController
    {
        /// <summary>
        /// صفحه لیست کوپن های ساخته شده
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن کوپن
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "کد تخفیف جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش کوپن
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش کد تخفیف";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست کوپن ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchCouponViewModel options)
        {
            try
            {
                var query = _context.Coupon.Where(x => x.StatusId != CouponStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Code.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new CouponViewModel()
                {
                    id = x.Id,
                    code = x.Code,
                    typeId = x.TypeId,
                    typeTitle = x.Type.PersianTitle,
                    value = x.Value,
                    usedCount = x.UsedCount,
                    usableCount = x.UsableCount,
                    userId = x.UserId,
                    fromDate = x.FromDate,
                    toDate = x.ToDate,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                data.ForEach(x =>
                {
                    x.fromDatePersian = DateUtility.GetPersianDateTime(x.fromDate);
                    x.toDatePersian = DateUtility.GetPersianDateTime(x.toDate);
                });

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات کوپن
        /// </summary>
        /// <param name="id">ردیف کوپن مورد نظر</param>
        /// <returns>نتیجه خواندن کوپن</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Coupon.Where(x => x.StatusId != CouponStatus.Deleted.Id && x.Id == id).Select(x => new CouponViewModel()
                {
                    id = x.Id,
                    code = x.Code,
                    typeId = x.TypeId,
                    typeTitle = x.Type.PersianTitle,
                    value = x.Value,
                    usedCount = x.UsedCount,
                    usableCount = x.UsableCount,
                    userId = x.UserId,
                    fromDate = x.FromDate,
                    toDate = x.ToDate,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).Single();

                entity.fromDatePersian = DateUtility.GetPersianDateTime(entity.fromDate);
                entity.toDatePersian = DateUtility.GetPersianDateTime(entity.toDate);

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت نام کوپن
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات کوپن جهت ثبت</param>
        /// <returns>نتیجه ثبت کوپن به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(CouponViewModel model)
        {
            try
            {
                if (model.usableCount <= 0)
                {
                    return Error("تعداد قابل استفاده باید بیشتر از 1 باشد.");
                }

                if (model.typeId == CouponType.Percentage.Id && model.value > 100)
                {
                    return Error("برای مقدار کد تخفیف از نوع درصد، نمی توان مقدار را بیشتر از 100 وارد کرد.");
                }

                var currentUser = GetAuthenticatedUser();
                if (model.id != null && model.id > 0)
                {
                    var entity = _context.Coupon.Single(x => x.StatusId != CouponStatus.Deleted.Id && x.Id == model.id);

                    DateTime? fromDate = null;
                    if (!string.IsNullOrEmpty(model.fromDatePersian))
                        fromDate = DateUtility.GetDateTime(model.fromDatePersian);

                    DateTime? toDate = null;
                    if (!string.IsNullOrEmpty(model.toDatePersian))
                        toDate = DateUtility.GetDateTime(model.toDatePersian);

                    entity.Value = model.value;
                    entity.UsableCount = model.usableCount;
                    entity.UserId = model.userId;
                    entity.FromDate = fromDate;
                    entity.ToDate = toDate;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    return Success("اطلاعات کد تخفیف با موفقیت ویرایش شد.");
                }
                else
                {
                    if (model.codeCount < 5)
                    {
                        return Error("حداقل تعداد برای ساخت کد تخفیف 5 کاراکتر است.");
                    }

                    if (model.count <= 0)
                    {
                        return Error("تعداد بایست حداقل 1 و یا بیشتر باشد.");
                    }

                    DateTime? fromDate = null;
                    if (!string.IsNullOrEmpty(model.fromDatePersian))
                        fromDate = DateUtility.GetDateTime(model.fromDatePersian);

                    DateTime? toDate = null;
                    if (!string.IsNullOrEmpty(model.toDatePersian))
                        toDate = DateUtility.GetDateTime(model.toDatePersian);

                    if (fromDate != null && toDate != null && fromDate >= toDate)
                    {
                        return Error("تاریخ شروع و انقضای کارت تخفیف نمی تواند برابر باشد.");
                    }

                    if (toDate != null && toDate <= DateTime.Now)
                    {
                        return Error("پایان تاریخ انقضای کارت تخفیف می بایست از امروز بیشتر باشد.");
                    }

                    int group = 0;
                    var lastItem = _context.Coupon.OrderByDescending(x => x.Id).FirstOrDefault();
                    if (lastItem != null)
                        group = lastItem.Group + 1;
                    else
                        group = 1;

                    List<string> codeList = new List<string>();
                    for (int i = 1; i <= model.count; i++)
                    {
                        string code = string.Empty;
                        while (true)
                        {
                            code = StringUtility.RandomString(model.codeCount);
                            Coupon coupon = _context.Coupon.SingleOrDefault(x => x.Code == code);
                            if (coupon == null && codeList.Count(x => x == code) == 0) break;
                        }

                        var entity = new Coupon()
                        {
                            TypeId = model.typeId,
                            Group = group,
                            Code = model.prefix + code,
                            Value = model.value,
                            UsableCount = model.usableCount,
                            UserId = model.userId,
                            FromDate = fromDate,
                            ToDate = toDate,
                            StatusId = model.statusId,
                            CreateUserId = currentUser.id,
                            ModifyUserId = currentUser.id,
                            CreateDate = GetDatetime(),
                            ModifyDate = GetDatetime(),
                            CreateIp = GetCurrentIp(),
                            ModifyIp = GetCurrentIp()
                        };

                        _context.Coupon.Add(entity);
                    }
                    _context.SaveChanges();

                    return Success("کد تخفیف با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف کوپن
        /// </summary>
        /// <param name="id">ردیف کوپن</param>
        /// <returns>نتیجه حذف کوپن</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Coupon.Single(x => x.StatusId != CouponStatus.Deleted.Id && x.Id == id);

                entity.StatusId = CouponStatus.Deleted.Id;
                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("کوپن با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// بررسی کد کوپن وارد شده جهت بررسی
        /// </summary>
        /// <param name="code">کد کوپن ارسال</param>
        /// <returns>نتیجه بررسی کد ارسال شده</returns>
        public JsonResult Check(string code)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                return Result(CheckCoupon(_context, currentUser, code, out Coupon coupon));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// بررسی اطلاعات یک بن تخفیف
        /// </summary>
        /// <param name="_context">شی دیتابیس</param>
        /// <param name="user">اطلاعات کاربر بررسی کننده</param>
        /// <param name="code">کد کوپن مورد نظر</param>
        /// <returns>نتیجه بررسی کوپن</returns>
        public static Response CheckCoupon(AsefianContext _context, UserPrincipal user, string code, out Coupon coupon)
        {
            coupon = null;
            if (string.IsNullOrEmpty(code))
            {
                return ResponseError("وارد کردن کد تخفیف الزامی می باشد.");
            }
            var couponEntity = _context.Coupon.Where(x => x.Code == code && x.StatusId == CouponStatus.Active.Id).SingleOrDefault();

            if (couponEntity != null)
            {
                if (couponEntity.UsableCount != null && couponEntity.UsableCount > 0 && couponEntity.UsedCount >= couponEntity.UsableCount)
                {
                    return ResponseError("تعداد مصرف این کد تخفیف به اتمام رسیده است.");
                }

                if (couponEntity.FromDate != null && couponEntity.FromDate > DateTime.Now)
                {
                    return ResponseError("مهلت استفاده ار کد تخفیف هنوز نشده است.");
                }

                if (couponEntity.ToDate != null && couponEntity.ToDate < DateTime.Now)
                {
                    return ResponseError("مهلت استفاده ار کد تخفیف به اتمام رسیده است.");
                }

                if (couponEntity.UserId != null && couponEntity.UserId != user.id)
                {
                    return ResponseError("این کد تخفیف برای کاربر شما نیست.");
                }

                coupon = couponEntity;
                return ResponseSuccess(new
                {
                    type = coupon.TypeId,
                    value = coupon.Value
                });
            }
            else
            {
                return ResponseError("کد تخفیف مورد نظر یافت نشد.");
            }
        }

    }
}