using Asefian.Common.Utility;
using Asefian.Model.Context.Account;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Context.Financial;
using Asefian.Model.Context.Financial.Enum;
using Asefian.Model.Metadata;
using Asefian.Model.ViewModel.Financial;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر فاکتور سفارشات
    /// </summary>
    [Authorize(Roles = "admin, invoice")]
    public class InvoiceController : BaseController
    {
        /// <summary>
        /// صفحه لیست فاکتور ها
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه جزئیات فاکتور
        /// </summary>
        /// <param name="id">ردیف فاکتور</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var item = _context.Invoice.Single(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == id);
            return BaseView(item);
        }

        /// <summary>
        /// جستجوی لیست فاکتور های ثبت شده
        /// </summary>
        /// <param name="options">گزینه های مورد تاثیر در جستجو</param>
        /// <returns>لیست فاکتور های یافت شده در جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchInvoiceViewModel options)
        {
            try
            {
                var query = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x =>
                                            x.DetailList.Any(y => y.Product.Sku.Contains(word)) ||
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
                                .Select(x => new ResponseSearchInvoiceViewModel()
                                {
                                    id = x.Id,
                                    invoiceNo = x.InvoiceNo,
                                    fullName = x.User.FirstName + " " + x.User.LastName,
                                    invoiceStatusId = x.InvoiceStatusId,
                                    invoiceStatusTitle = x.InvoiceStatus.PersianTitle,
                                    price = x.Price,
                                    unpaidPrice = x.UnpaidPrice,
                                    date = x.CreateDate
                                }).ToList();

                data.ForEach(x => x.persianDate = DateUtility.GetPersianDateTime(x.date));

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر وضعیت سفارش
        /// </summary>
        /// <param name="model">اطلاعات مورد نیاز برای تغییر سفارش</param>
        /// <returns>نتیجه تغییر وضعیت سفارش</returns>
        [HttpPost]
        public JsonResult ChangeStatus(InvoiceChangeStatusViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var invoice = _context.Invoice.Single(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == model.invoiceId);

                invoice.InvoiceStatusId = model.statusId;
                invoice.ModifyUserId = currentUser.id;
                invoice.ModifyDate = GetDatetime();
                invoice.ModifyIp = GetCurrentIp();

                var invoiceLog = new InvoiceLog()
                {
                    InvoiceId = model.invoiceId,
                    StatusId = model.statusId,
                    Description = model.description.ToStandardPersian(),
                    CreateUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };

                _context.InvoiceLog.Add(invoiceLog);
                _context.SaveChanges();

                return Success("تغییر وضعیت با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// اضافه کردن محصول به فاکتور
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات محصول و فاکتور</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpPost]
        public JsonResult Addproduct(InvoiceChangeProductViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var product = _context.Product.Single(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == model.productId);
                if (model.count <= 0)
                {
                    return Error("تعداد وارد شده صحیح نیست.");
                }
                if (product.Count < model.count)
                {
                    return Error("تعداد موجودی از تعداد درخواستی برای اضافه شدن به فاکتور بیشتر است.");
                }

                var invoice = _context.Invoice.Single(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == model.invoiceId);

                var availableInvoiceStatusList = new InvoiceStatus[] { InvoiceStatus.Registered, InvoiceStatus.InProgress, InvoiceStatus.Factored, InvoiceStatus.Preparing };
                if (!availableInvoiceStatusList.Any(x => x.Id == invoice.InvoiceStatusId))
                {
                    return Error("فاکتور در وضعیتی نیست که بتوان به آن محصول اضافه کرد.");
                }

                var invoiceDetail = new InvoiceDetail()
                {
                    InvoiceId = invoice.Id,
                    ProductId = model.productId,
                    ProductFeatureId = model.productFeatureId,
                    Count = model.count,
                    Price = product.Price,
                    StatusId = InvoiceDetailStatus.Accepted.Id,
                    CreateUserId = currentUser.id,
                    ModifyUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    ModifyDate = GetDatetime(),
                    CreateIp = GetCurrentIp(),
                    ModifyIp = GetCurrentIp()
                };
                _context.InvoiceDetail.Add(invoiceDetail);

                invoice.Price += (invoiceDetail.Count * invoiceDetail.Price);
                invoice.UnpaidPrice += (invoiceDetail.Count * invoiceDetail.Price);
                invoice.ModifyUserId = currentUser.id;
                invoice.ModifyDate = GetDatetime();
                invoice.ModifyIp = GetCurrentIp();

                var invoiceLog = new InvoiceLog()
                {
                    InvoiceId = invoice.Id,
                    StatusId = invoice.InvoiceStatusId,
                    Description = string.Format("اضافه کردن محصول {0} به تعداد {1} در فاکتور", product.Code, model.count),
                    CreateUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };
                _context.InvoiceLog.Add(invoiceLog);

                product.Count -= model.count;
                product.ModifyUserId = currentUser.id;
                product.ModifyDate = GetDatetime();
                product.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("محصول مورد نظر با موفقیت به فاکتور اضافه شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف کردن محصول از فاکتور
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات محصول و فاکتور</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpPost]
        public JsonResult RemoveProduct(InvoiceChangeProductViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var product = _context.Product.Single(x => x.StatusId != ProductStatus.Deleted.Id && x.Id == model.productId);
                var invoice = _context.Invoice.Single(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == model.invoiceId);
                var invoiceDetail = invoice.DetailList.Single(x => x.StatusId != InvoiceDetailStatus.Deleted.Id && x.ProductId == model.productId);

                var availableInvoiceStatusList = new InvoiceStatus[] { InvoiceStatus.Registered, InvoiceStatus.InProgress, InvoiceStatus.Factored, InvoiceStatus.Preparing };
                if (!availableInvoiceStatusList.Any(x => x.Id == invoice.InvoiceStatusId))
                {
                    return Error("فاکتور در وضعیتی نیست که بتوان از آن محصول کسر کرد.");
                }

                if (model.count > invoiceDetail.Count)
                {
                    return Error("تعداد وارد شده از تعداد ثبت شده در فاکتور بیشتر است.");
                }

                var price = model.count * invoiceDetail.Price;
                if (price > invoice.UnpaidPrice)
                {
                    var user = _context.User.Single(x => x.Id == currentUser.id);
                    user.Balance = user.Balance + price;

                    var balanceLog = new UserBalanceLog()
                    {
                        UserId = invoice.User.Id,
                        Amount = price,
                        TypeId = BalanceType.Positive.Id,
                        Description = string.Format("بابت کسر محصول {0} به تعداد {1} از فاکتور {2}", product.Code, model.count, invoice.InvoiceNo),
                        CreateUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        CreateIp = GetCurrentIp()
                    };
                    _context.UserBalanceLog.Add(balanceLog);
                }
                else
                {
                    invoice.UnpaidPrice -= price;
                }

                invoice.Price = invoice.Price - price;
                invoiceDetail.Count = invoiceDetail.Count - model.count;

                if (invoiceDetail.Count == 0)
                {
                    invoiceDetail.StatusId = InvoiceDetailStatus.Deleted.Id;
                }

                var invoiceLog = new InvoiceLog()
                {
                    InvoiceId = invoice.Id,
                    StatusId = invoice.InvoiceStatusId,
                    Description = string.Format("کسر محصول {0} به تعداد {1} از فاکتور", product.Code, model.count),
                    CreateUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };
                _context.InvoiceLog.Add(invoiceLog);

                _context.SaveChanges();

                return Success("محصول مورد نظر با موفقیت از فاکتور کسر شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر آدرس فاکتور
        /// </summary>
        /// <param name="model">اطلاعات تغییر آدرس کاربر</param>
        /// <returns>نتیجه تغییر آدرس فاکتور</returns>
        [HttpPost]
        public JsonResult ChangeAddress(InvoiceChangeAddressViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var invoice = _context.Invoice.Single(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == model.invoiceId);
                var address = _context.UserAddress.Single(x => x.StatusId != UserAddressStatus.Deleted.Id && x.Id == model.addressId);

                if (address.UserId != invoice.UserId)
                {
                    return Error("آدرس انتخاب شده متعلق به کاربر ثبت کننده سفارش نیست.");
                }

                var availableInvoiceStatusList = new InvoiceStatus[] { InvoiceStatus.Registered, InvoiceStatus.InProgress, InvoiceStatus.Factored, InvoiceStatus.Preparing };
                if (!availableInvoiceStatusList.Any(x => x.Id == invoice.InvoiceStatusId))
                {
                    return Error("فاکتور در وضعیتی نیست که بتوان آدرس آن را تغییر داد..");
                }

                invoice.AddressId = model.addressId;
                invoice.ModifyUserId = currentUser.id;
                invoice.ModifyDate = GetDatetime();
                invoice.ModifyIp = GetCurrentIp();

                _context.SaveChanges();
                return Success("آدرس با موفقیت تغییر کرد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن تمام اطلاعات یک فاکتور
        /// </summary>
        /// <param name="id">ردیف فاکتور</param>
        /// <returns>اطلاعات یک فاکتور</returns>
        [HttpGet]
        public JsonResult LoadDetail(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var invoice = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == id).Select(x => new InvoiceViewModel()
                {
                    id = x.Id,
                    userId = x.UserId,
                    firstName = x.User.FirstName,
                    lastName = x.User.LastName,
                    invoiceStatusId = x.InvoiceStatusId,
                    invoiceStatusTitle = x.InvoiceStatus.PersianTitle,
                    paymentTypeId = x.PaymentTypeId,
                    paymentTypeTitle = x.PaymentType.PersianTitle,
                    deliveryTypeId = x.DeliveryTypeId,
                    deliveryTypeTitle = x.DeliveryType.PersianTitle,
                    invoiceNo = x.InvoiceNo,
                    price = x.Price,
                    unpaidPrice = x.UnpaidPrice,
                    deliveryPrice = x.DeliveryPrice,
                    addressId = x.AddressId,
                    receiverFullName = x.Address.ReceiverFullName,
                    receiverMobileNumber = x.Address.ReceiverMobileNumber,
                    phoneNumber = x.Address.PhoneNumber,
                    provinceId = x.Address.ProvinceId,
                    provinceTitle = x.Address.Province.Title,
                    cityId = x.Address.CityId,
                    cityTitle = x.Address.City.Title,
                    postalCode = x.Address.PostalCode,
                    address = x.Address.Address,
                    deliveryDate = x.DeliveryDate,
                    deliveryTime = x.DeliveryTime,
                    description = x.Description,
                    couponId = x.CouponId,
                    couponCode = x.Coupon.Code,
                    createDate = x.CreateDate,
                    detailList = x.DetailList.Where(y => y.StatusId != InvoiceDetailStatus.Deleted.Id).Select(y => new InvoiceDetailViewModel()
                    {
                        id = y.Id,
                        invoiceId = y.InvoiceId,
                        productId = y.ProductId,
                        productTitle = y.Product.Sku,
                        productFileId = y.Product.FileId,
                        productFileName = y.Product.FileName,
                        productCategoryTitle = y.Product.Category.Sku,
                        productBrandTitle = y.Product.Brand.Sku,
                        featureTitle = y.ProductFeature.CategoryFeatureValue.CategoryFeature.Sku,
                        productFeatureId = y.ProductFeatureId,
                        productFeatureTitle = y.ProductFeature.CategoryFeatureValue.Sku,
                        count = y.Count,
                        price = y.Price
                    }).ToList(),
                    logList = x.InvoiceLogList.Select(y => new InvoiceLogViewModel()
                    {
                        id = y.Id,
                        invoiceId = y.InvoiceId,
                        statusId = y.StatusId,
                        statusTitle = y.Status.PersianTitle,
                        description = y.Description,
                        date = y.CreateDate,
                        firstName = y.CreateUser.FirstName,
                        lastName = y.CreateUser.LastName
                    }).ToList()
                }).Single();

                invoice.PersianDeliveryDate = DateUtility.GetPersianDate(invoice.deliveryDate);
                invoice.PersianCreateDate = DateUtility.GetPersianDateTime(invoice.createDate, " - ");
                invoice.detailList.ForEach(x => x.productUrlTitle = x.productTitle.ToUrlString());
                invoice.logList.ForEach(x => x.PersianDate = DateUtility.GetPersianDateTime(x.date));

                return Success(invoice);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت پرداخت برای فاکتور
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات پرداخت</param>
        /// <returns>نتیجه ثبت پرداخت</returns>
        [HttpPost]
        public JsonResult InvoicePaid(InvoicePaidViewModel model)
        {
            try
            {
                if (model.invoiceId <= 0)
                {
                    return Error("مبلغ برای ثبت می بایست از ");
                }

                var currentUser = GetAuthenticatedUser();

                var invoice = _context.Invoice.Where(x => x.InvoiceStatusId != InvoiceStatus.Deleted.Id && x.Id == model.invoiceId).Single();
                if (invoice.UnpaidPrice < model.price)
                {
                    return Error("مبلغ وارد شده از میزان بدهی فاکتور بیشتر است.");
                }

                invoice.UnpaidPrice -= model.price;
                invoice.ModifyUserId = currentUser.id;
                invoice.ModifyDate = GetDatetime();
                invoice.ModifyIp = GetCurrentIp();


                var invoiceLog = new InvoiceLog()
                {
                    InvoiceId = model.invoiceId,
                    StatusId = invoice.InvoiceStatusId,
                    Description = "ثبت پرداخت به مبلغ " + model.price.ToString("N0") + " " + AsefianMetadata.CurrencyName,
                    CreateUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };

                _context.InvoiceLog.Add(invoiceLog);
                _context.SaveChanges();

                return Success("ثبت پرداخت با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
                throw;
            }
        }
    }
}