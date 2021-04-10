using Asefian.Common.Utility;
using Asefian.Common.Web;
using Asefian.Model.Context.Financial;
using Asefian.Model.Context.Financial.Enum;
using Asefian.Model.Metadata;
using Asefian.Model.ViewModel.Financial;
using Asefian.Web.Areas.Admin.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر فاکتور های کاربر
    /// </summary>
    public class InvoiceController : BaseController
    {

        /// <summary>
        /// جستجوی لیست فاکتور های کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات جستجو</param>
        /// <returns>نتیجه جستجوی کاربر</returns>
        public JsonResult List(SearchInvoiceViewModel options)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var query = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.UserId == currentUser.id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x =>
                                            //x.DetailList.Any(y => y.Product.Title.Contains(word)) ||
                                            x.DetailList.Any(y => y.Product.Code.Contains(word)) ||
                                            x.InvoiceNo.Contains(word) ||
                                            x.User.FirstName.Contains(word) ||
                                            x.User.LastName.Contains(word) ||
                                            x.User.MobileNumber.Contains(word) ||
                                            x.User.Email.Contains(word)
                                        );
                }

                if (options.statusList != null && options.statusList.Count(x => x > 0) > 0)
                {
                    query = query.Where(x => options.statusList.Any(y => y == x.InvoiceStatusId));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                                .Skip(options.page * options.count)
                                .Take(options.count)
                                .Select(x => new ResponseUserSearchInvoiceViewModel()
                                {
                                    id = x.Id,
                                    invoiceNo = x.InvoiceNo,
                                    invoiceStatusId = x.InvoiceStatusId,
                                    invoiceStatusTitle = x.InvoiceStatus.PersianTitle,
                                    price = x.Price,
                                    unpaidPrice = x.UnpaidPrice,
                                    date = x.CreateDate
                                }).ToList();

                data.ForEach(x => x.PersianDate = DateUtility.GetPersianDateTime(x.date));

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// پرداخت فاکتور
        /// </summary>
        /// <param name="id">ردیف فاکتور</param>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult Payment(int id)
        {
            var currentUser = GetAuthenticatedUser();

            var invoice = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.UserId == currentUser.id && x.Id == id).SingleOrDefault();

            if (invoice == null || invoice.InvoiceStatusId != InvoiceStatus.Registered.Id)
                return HttpNotFound();

            if (invoice.InvoiceStatusId == InvoiceStatus.InProgress.Id)
            {
                return Redirect("/invoice/detail/" + id);
            }

            ViewBag.TotalPrice = invoice.Price;

            var keys = new string[] { BaseInformationKey.OrderGapDay, BaseInformationKey.MinDeliveryFreePrice, BaseInformationKey.DeliveryPrice, BaseInformationKey.DeliveryManCity };
            var baseInformation = _context.BaseInformation.Where(x => keys.Any(y => y == x.Key)).ToList();

            ViewBag.GapDay = int.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.OrderGapDay)?.Value ?? AsefianMetadata.DefaultGapDay);
            ViewBag.MinDeliveryFreePrice = long.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.MinDeliveryFreePrice)?.Value ?? AsefianMetadata.DefaultMinDeliveryFreePrice);
            ViewBag.DeliveryPrice = long.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryPrice)?.Value ?? AsefianMetadata.DefaultDeliveryPrice);

            ViewBag.CityForDelivery = JsonConvert.SerializeObject(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity)?.Value.Split('-').Select(x => int.Parse(x)).ToArray() ?? new int[] { });


            return View(invoice);
        }

        /// <summary>
        /// تایید نحوه پرداخت فاکتور
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات تایید پرداخت</param>
        /// <returns>نتیجه تایید پرداخت که در صورت پرداخت اینترنتی به بانک و در غیر این صورت به خلاصه سفارش</returns>
        [HttpPost]
        public ActionResult SubmitPayment(SubmitPaymentViewModel model)
        {
            var currentUser = GetAuthenticatedUser();

            if (model.paymentType <= 0)
            {
                TempData["SubmitPaymentError"] = "انتخاب نوع پرداخت اجباری است.";
                return Redirect("/invoice/payment/" + model.id);
            }

            var invoice = _context.Invoice.Where(x => x.InvoiceStatusId == InvoiceStatus.Registered.Id && x.Id == model.id && x.UserId == currentUser.id).SingleOrDefault();
            if (invoice == null)
                return HttpNotFound();

            if (invoice.InvoiceStatusId == InvoiceStatus.InProgress.Id)
                return Redirect("/invoice/detail/" + model.id);

            if (invoice.CouponId != null && !string.IsNullOrEmpty(model.coupon))
            {
                Response couponResponse = null;

                couponResponse = CouponController.CheckCoupon(_context, currentUser, model.coupon, out Coupon coupon);
                if (couponResponse.status != ResponseStatus.Ok)
                {
                    TempData["SubmitPaymentError"] = "انتخاب نوع پرداخت اجباری است.";
                    return Redirect("/invoice/payment/" + model.id);
                }

                if (coupon.TypeId == CouponType.Amount.Id)
                    invoice.Price -= coupon.Value;
                else if (coupon.TypeId == CouponType.Percentage.Id)
                    invoice.Price -= ((invoice.Price / 100) * coupon.Value);

                if (invoice.Price < 0)
                    invoice.Price = 0;

                invoice.CouponId = coupon.Id;
            }

            invoice.InvoiceStatusId = InvoiceStatus.InProgress.Id;
            invoice.PaymentTypeId = model.paymentType;
            invoice.ModifyUserId = currentUser.id;
            invoice.ModifyDate = GetDatetime();
            invoice.ModifyIp = GetCurrentIp();

            var invoiceLog = new InvoiceLog()
            {
                InvoiceId = invoice.Id,
                StatusId = InvoiceStatus.InProgress.Id,
                CreateUserId = currentUser.id,
                CreateDate = GetDatetime(),
                CreateIp = GetCurrentIp()
            };
            _context.InvoiceLog.Add(invoiceLog);

            _context.SaveChanges();

            return Redirect("/invoice/detail/" + model.id);
        }

        /// <summary>
        /// لیست فاکتور های کاربر
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// مشاهده جزئیات یک فاکتور
        /// </summary>
        /// <param name="id">ردیف فاکتور</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var currentUser = GetAuthenticatedUser();

            var invoice = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.UserId == currentUser.id && x.Id == id).SingleOrDefault();

            if (invoice == null)
                return HttpNotFound();

            return View(invoice);
        }

    }
}