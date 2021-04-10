using Nig.Common.Web;
using Nig.Model.Context;
using Nig.Model.Context.Financial;
using Nig.Model.Context.Financial.Enum;
using Nig.Model.ViewModel.Account;
using Nig.Model.ViewModel.Financial;
using Nig.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nig.Web.Areas.Api.Controllers
{
    /// <summary>
    /// کنترلر کوپن
    /// </summary>
    public class CouponController : BaseController
    {
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
        public static Response CheckCoupon(NigContext _context, UserPrincipal user, string code, out Coupon coupon)
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