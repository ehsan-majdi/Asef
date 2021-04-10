using Asefian.Common.Utility;
using Asefian.Common.Web;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Context.Data.Enum;
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
    /// کنترلر سبد خرید
    /// </summary>
    public class CartController : BaseController
    {
        /// <summary>
        /// لیست محصولات موجود در سبد خرید
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("Cart")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return BaseView();
        }

        /// <summary>
        /// مشاهده و تایید اطلاعات سفارش و انتخاب روش ارسال
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("Shipping")]
        public ActionResult Shipping()
        {
            var currentUser = GetAuthenticatedUser();

            var cartList = _context.Cart.GroupBy(x => x.UserId == currentUser.id).ToList();

            if (cartList.Count == 0)
                return Redirect("/cart");

            var cart = cartList.Select(x => new
            {
                price = x.Sum(y => y.Product.Price - y.Product.Discount * y.Count),
                totalPrice = x.Sum(y => y.Product.Price * y.Count),
                discount = x.Sum(y => y.Product.Discount * y.Count),
            }).Single();

            ViewBag.TotalPrice = cart.totalPrice;
            ViewBag.Price = cart.price;
            ViewBag.Discount = cart.discount;

            var keys = new string[] { BaseInformationKey.OrderGapDay, BaseInformationKey.MinDeliveryFreePrice, BaseInformationKey.DeliveryPrice, BaseInformationKey.DeliveryManCity };
            var baseInformation = _context.BaseInformation.Where(x => keys.Any(y => y == x.Key)).ToList();

            ViewBag.GapDay = int.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.OrderGapDay)?.Value ?? AsefianMetadata.DefaultGapDay);
            ViewBag.MinDeliveryFreePrice = long.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.MinDeliveryFreePrice)?.Value ?? AsefianMetadata.DefaultMinDeliveryFreePrice);
            ViewBag.DeliveryPrice = long.Parse(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryPrice)?.Value ?? AsefianMetadata.DefaultDeliveryPrice);

            ViewBag.CityForDelivery = JsonConvert.SerializeObject(baseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity)?.Value.Split('-').Select(x => int.Parse(x)).ToArray() ?? new int[] { });

            return BaseView();
        }

        /// <summary>
        /// تایید سفارش و بسته شدن سبد خرید
        /// </summary>
        /// <param name="model">اطلاعات تایید سفارش</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpPost]
        public ActionResult Shipping(ShippingViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var cartList = _context.Cart.Where(x => x.UserId == currentUser.id).ToList();

                if (cartList.Count == 0)
                {
                    return Error("سبد خرید شما خالی است.");
                }

                var addressList = _context.UserAddress.Where(x => x.StatusId == UserAddressStatus.Active.Id && x.UserId == currentUser.id).ToList();
                var address = addressList.Single(x => x.Id == model.addressId);
                if (model.addressId > 0)
                {
                    if (addressList.SingleOrDefault(x => x.Id == model.addressId) == null)
                    {
                        return Error("آدرس انتخاب شده صحیح نیست.");
                    }
                    else
                    {
                        addressList.ForEach(x => x.MainAddress = false);
                        address.MainAddress = true;
                    }
                }
                else
                {
                    return Error("لطفا آدرس تحویل سفارش خود را مشخص کنید.");
                }

                if (model.deliveryType > 0)
                {
                    if (model.deliveryType == 1 && string.IsNullOrEmpty(model.time) && model.time.Split(' ').Length != 2)
                    {
                        return Error("لطفا زمان تحویل سفارش خود را مشخص کنید.");
                    }
                }
                else
                {
                    return Error("لطفا نحوه تحویل سفارش خود را مشخص کنید.");
                }

                Response couponResponse = null;
                Coupon coupon = null;
                if (!string.IsNullOrEmpty(model.coupon))
                {
                    couponResponse = CouponController.CheckCoupon(_context, currentUser, model.coupon, out coupon);
                    if (couponResponse.status != ResponseStatus.Ok)
                        return Error(couponResponse.message);
                }

                var invoiceDetailList = new List<InvoiceDetail>();
                foreach (var item in cartList)
                {

                    var totalPrice = item.Product.Price - item.Product.Discount;
                    var invoiceDetail = new InvoiceDetail()
                    {
                        ProductId = item.ProductId,
                        ProductFeatureId = item.ProductFeatureId,
                        Count = item.Count,
                        Price = totalPrice,
                        StatusId = InvoiceDetailStatus.Accepted.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    invoiceDetailList.Add(invoiceDetail);

                }

                var price = invoiceDetailList.Sum(x => x.Count * x.Price);

                if (coupon != null)
                {
                    if (coupon.TypeId == CouponType.Amount.Id)
                        price = price - coupon.Value;
                    else if (coupon.TypeId == CouponType.Percentage.Id)
                        price = price - ((price / 100) * coupon.Value);

                    if (price < 0)
                        price = 0;
                }

                var keys = new string[] { BaseInformationKey.OrderGapDay, BaseInformationKey.MinDeliveryFreePrice, BaseInformationKey.DeliveryPrice, BaseInformationKey.DeliveryManCity };
                var baseInformations = _context.BaseInformation.Where(x => keys.Any(y => y == x.Key));

                var GapDay = int.Parse(baseInformations.SingleOrDefault(x => x.Key == BaseInformationKey.OrderGapDay)?.Value ?? AsefianMetadata.DefaultGapDay);
                var MinDeliveryFreePrice = long.Parse(baseInformations.SingleOrDefault(x => x.Key == BaseInformationKey.MinDeliveryFreePrice)?.Value ?? AsefianMetadata.DefaultMinDeliveryFreePrice);
                var DeliveryPrice = long.Parse(baseInformations.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryPrice)?.Value ?? AsefianMetadata.DefaultDeliveryPrice);

                if (model.deliveryType == DeliveryType.DeliveryMan.Id)
                {
                    var deliveryManCity = baseInformations.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity)?.Value.Split('-').Select(x => int.Parse(x)).ToArray() ?? new int[] { };
                    if (deliveryManCity.Count(x => x == address.CityId) == 0)
                    {
                        return Error("برای آدرس مورد نظر شما امکان ارسال با پیک فراهم نیست.");
                    }
                }

                var deliveryPrice = DeliveryPrice;
                if (price >= MinDeliveryFreePrice)
                {
                    deliveryPrice = 0;
                }

                DateTime? deliveryDate = null;
                if (!string.IsNullOrEmpty(model.time))
                {
                    deliveryDate = DateUtility.GetDateTime(model.time.Split(' ')[0]);
                }

                if (deliveryDate != null && DateTime.Today.AddDays(GapDay) > deliveryDate)
                {
                    return Error("تاریخ وارد شده برای تحویل سفارش صحیح نیست.");
                }

                var invoice = new Invoice()
                {
                    UserId = currentUser.id,
                    InvoiceStatusId = InvoiceStatus.Registered.Id,
                    PaymentTypeId = PaymentType.Unknown.Id,
                    InvoiceNo = GetNewInvoiceNo(),
                    Price = price,
                    UnpaidPrice = price,
                    DeliveryPrice = deliveryPrice,
                    DeliveryTypeId = model.deliveryType,
                    AddressId = model.addressId,
                    DeliveryDate = deliveryDate,
                    DeliveryTime = model.time?.Split(' ')[1],
                    CouponId = coupon?.Id,
                    CreateUserId = currentUser.id,
                    ModifyUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    ModifyDate = GetDatetime(),
                    CreateIp = GetCurrentIp(),
                    ModifyIp = GetCurrentIp(),

                    DetailList = invoiceDetailList
                };

                _context.Cart.RemoveRange(cartList);
                _context.Invoice.Add(invoice);

                var invoiceLog = new InvoiceLog()
                {
                    Invoice = invoice,
                    StatusId = InvoiceStatus.Registered.Id,
                    CreateUserId = currentUser.id,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };
                _context.InvoiceLog.Add(invoiceLog);

                _context.SaveChanges();

                return Success("سفارش شما با موفقیت ثبت شد.", new
                {
                    id = invoice.Id
                });
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت شماره فاکتور جدید
        /// </summary>
        /// <returns>شماره فاکتور جدید</returns>
        public string GetNewInvoiceNo()
        {
            var invoice = _context.Invoice.OrderByDescending(x => x.InvoiceNo).FirstOrDefault();
            if (invoice != null)
            {
                return (int.Parse(invoice.InvoiceNo) + 1).ToString("D5");
            }
            else
            {
                return 1.ToString("D5");
            }
        }

        /// <summary>
        /// خواندن اطلاعات سبد خرید
        /// </summary>
        /// <returns>محتویات سبد خرید</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCart()
        {
            try
            {
                List<CartViewModel> carts;
                if (Request.IsAuthenticated)
                {
                    var currentUser = GetAuthenticatedUser();
                    carts = _context.Cart.Where(x => x.Product.StatusId == ProductStatus.Stock.Id && x.UserId == currentUser.id).Select(x => new CartViewModel()
                    {
                        id = x.Id,
                        productId = x.ProductId,
                        //productTitle = x.Product.Title,
                        fileId = x.Product.FileId,
                        fileName = x.Product.FileName,
                        categoryTitle = x.Product.Category.TranslationList.Single(z=>z.LanguageId == GetLanguage().Id).Title,
                        //brandTitle = x.Product.Brand.Title,
                        price = x.ProductFeatureId == null ? x.Product.Price : x.Product.ProductFeatureList.FirstOrDefault(y => y.Id == x.ProductFeatureId).Price,
                        discount = x.ProductFeatureId == null ? x.Product.Discount : x.Product.ProductFeatureList.FirstOrDefault(y => y.Id == x.ProductFeatureId).Discount,
                        count = x.Count,
                        inventory = x.ProductFeatureId == null ? x.Product.Count : x.Product.ProductFeatureList.FirstOrDefault(y => y.Id == x.ProductFeatureId).Count
                    
                    }).ToList();

                    carts.ForEach(x =>
                    {
                        x.productUrlTitle = x.productTitle.ToUrlString();
                    });
                }
                else
                {
                    if (Request.Cookies["Cart"] != null)
                    {
                        var value = Request.Cookies["Cart"].Value;
                        var cookieCart = JsonConvert.DeserializeObject<List<CartCookieViewModel>>(value);
                        var productIdList = cookieCart.Select(x => x.productId).ToList();
                        carts = _context.Product.Where(x => x.StatusId == ProductStatus.Stock.Id && productIdList.Any(y => y == x.Id)).Select(x => new CartViewModel
                        {
                            productId = x.Id,
                            //productTitle = x.Title,
                            fileId = x.FileId,
                            fileName = x.FileName,
                            //categoryTitle = x.Category.Title,
                            //brandTitle = x.Brand.Title,
                            price = x.Price,
                            discount = x.Discount,
                            inventory = x.Count,

                        }).ToList();

                        carts.ForEach(x =>
                        {
                            x.count = cookieCart.Single(y => y.productId == x.productId).count;
                            x.productFeatureId = cookieCart.Single(y => y.productId == x.productId).productFeatureId;
                            x.productFeatureTitle = cookieCart.Single(y => y.productId == x.productId).productFeatureTitle;
                            x.productUrlTitle = x.productTitle.ToUrlString();
                            if (x.productFeatureId != null)
                            {
                                var feature = _context.ProductFeature.Single(y => y.Id == x.productFeatureId);
                                x.price = feature.Price;
                                x.discount = feature.Discount;
                                x.inventory = feature.Count;
                            }
                        });
                    }
                    else
                    {
                        carts = new List<CartViewModel>();
                    }
                }
                return Success(new
                {
                    list = carts,
                    totalDiscount = carts.Sum(x => (x.discount) * x.count),
                    totalPrice = carts.Sum(x => (x.price - x.discount) * x.count)
                });
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// اضافه کردن محصول به سبد خرید
        /// در صورتی که شخصی لاگین نباشد به کوکی اضافه می شود.
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>نتیجه اضافه شدن به سبد خرید</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddToCart(int id)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    var currentUser = GetAuthenticatedUser();
                    var product = _context.Product.Single(x => x.StatusId == ProductStatus.Stock.Id && x.Id == id);
                    if (product.Count >= 1)
                    {
                        var entity = new Cart()
                        {
                            UserId = currentUser.id,
                            ProductId = id,
                            Count = 1,
                            CreateUserId = currentUser.id,
                            ModifyUserId = currentUser.id,
                            CreateDate = GetDatetime(),
                            ModifyDate = GetDatetime(),
                            CreateIp = GetCurrentIp(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.Cart.Add(entity);
                    }
                    else
                    {
                        return Error("تعداد وارد شده صحیح نیست.");
                    }
                    _context.SaveChanges();
                }
                else
                {
                    var product = _context.Product.Single(x => x.StatusId == ProductStatus.Stock.Id && x.Id == id);
                    if (Request.Cookies["Cart"] != null)
                    {
                        var value = Request.Cookies["Cart"].Value;
                        var cookieCart = JsonConvert.DeserializeObject<List<CartCookieViewModel>>(value);
                        if (cookieCart.Count(x => x.productId == id) > 0)
                        {
                            if (product.Count >= cookieCart.Single(x => x.productId == id).count + 1)
                            {
                                cookieCart.Single(x => x.productId == id).count = cookieCart.Single(x => x.productId == id).count + 1;
                                var cookie = Request.Cookies["Cart"];
                                cookie.Value = JsonConvert.SerializeObject(cookieCart);
                                cookie.Expires = DateTime.Now.AddDays(10);
                                Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                return Error("تعداد وارد شده صحیح نیست.");
                            }
                        }
                        else
                        {

                            if (product.Count >= 1)
                            {
                                cookieCart.Add(new CartCookieViewModel()
                                {
                                    productId = id,
                                    count = 1
                                });
                                var cookie = Request.Cookies["Cart"];
                                cookie.Value = JsonConvert.SerializeObject(cookieCart);
                                cookie.Expires = DateTime.Now.AddDays(10);
                                Response.Cookies.Add(cookie);
                            }
                            else
                            {
                                return Error("تعداد وارد شده صحیح نیست.");
                            }
                        }
                    }
                    else
                    {
                        if (product.Count >= 1)
                        {
                            var cookie = new HttpCookie("Cart");
                            Dictionary<int, int> cookieCart = new Dictionary<int, int>();
                            cookieCart.Add(id, 1);
                            cookie.Value = JsonConvert.SerializeObject(cookieCart);
                            cookie.Expires = DateTime.Now.AddDays(10);
                            Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            return Error("تعداد وارد شده صحیح نیست.");
                        }
                    }
                }

                return Success("محصول با موفقیت به سبد خرید اضافه شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر تعداد محصول موجود در سبد خرید
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <param name="count">تعداد جدید</param>
        /// <returns>نتیجه تغییر تعداد محصول</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ChangeCount(int id, int? productFeatureId, int count)
        {
            try
            {
                if (count > 0)
                {

                    if (Request.IsAuthenticated)
                    {
                        var currentUser = GetAuthenticatedUser();
                        var item = _context.Cart.SingleOrDefault(x => x.UserId == currentUser.id && x.ProductId == id && x.ProductFeatureId == productFeatureId);
                        if (item != null)
                        {
                            if (item.ProductFeatureId == null)
                            {
                                if (item.Product.Count >= count)
                                {
                                    item.Count = count;
                                    item.ModifyUserId = currentUser.id;
                                    item.ModifyDate = GetDatetime();
                                    item.ModifyIp = GetCurrentIp();

                                    _context.SaveChanges();
                                    return Success("تعداد محصول با موفقیت تغییر کرد.");
                                }
                                else
                                {
                                    return Error("تعداد وارد شده صحیح نیست.");
                                }
                            }
                            else
                            {
                                if (item.Product.ProductFeatureList.Single(x => x.Id == productFeatureId).Count >= count)
                                {
                                    item.Count = count;
                                    item.ModifyUserId = currentUser.id;
                                    item.ModifyDate = GetDatetime();
                                    item.ModifyIp = GetCurrentIp();

                                    _context.SaveChanges();
                                    return Success("تعداد محصول با موفقیت تغییر کرد.");
                                }
                                else
                                {
                                    return Error("تعداد وارد شده صحیح نیست.");
                                }
                            }
                        }
                        else
                        {
                            return Error("محصول مورد نظر در سبد خرید یافت نشد.");
                        }
                    }
                    else
                    {
                        if (Request.Cookies["Cart"] != null)
                        {
                            var value = Request.Cookies["Cart"].Value;
                            var cookieCart = JsonConvert.DeserializeObject<List<CartCookieViewModel>>(value);
                            if (cookieCart.Count(x => x.productId == id && x.productFeatureId == productFeatureId) > 0)
                            {
                                var product = _context.Product.Single(x => x.StatusId == ProductStatus.Stock.Id && x.Id == id);
                                if (cookieCart.Single(x => x.productId == id && x.productFeatureId == productFeatureId).productFeatureId == null)
                                {
                                    if (product.Count >= count)
                                    {
                                        cookieCart.Single(x => x.productId == id && x.productFeatureId == productFeatureId).count = count;

                                        var cookie = Request.Cookies["Cart"];
                                        cookie.Value = JsonConvert.SerializeObject(cookieCart);
                                        cookie.Expires = DateTime.Now.AddDays(10);
                                        Response.Cookies.Add(cookie);

                                        return Success("تعداد محصول با موفقیت تغییر کرد.");
                                    }
                                    else
                                    {
                                        return Error("تعداد وارد شده صحیح نیست.");
                                    }
                                }
                                else
                                {
                                    if (product.ProductFeatureList.Single(x => x.Id == productFeatureId).Count >= count)
                                    {
                                        cookieCart.Single(x => x.productId == id && x.productFeatureId == productFeatureId).count = count;

                                        var cookie = Request.Cookies["Cart"];
                                        cookie.Value = JsonConvert.SerializeObject(cookieCart);
                                        cookie.Expires = DateTime.Now.AddDays(10);
                                        Response.Cookies.Add(cookie);

                                        return Success("تعداد محصول با موفقیت تغییر کرد.");
                                    }
                                    else
                                    {
                                        return Error("تعداد وارد شده صحیح نیست.");
                                    }
                                }
                            }
                            else
                            {
                                return Error("محصول مورد نظر در سبد خرید یافت نشد.");
                            }
                        }
                        else
                        {
                            return Error("محصول مورد نظر یافت نشد.");
                        }
                    }
                }
                else
                {
                    return Error("تعداد محصول می بایست از 1 بیتشر باشد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف یک محصول از سبد خرید
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>نتیجه حذف محصول</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RemoveFromCart(int id, int? productFeatureId)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    var currentUser = GetAuthenticatedUser();
                    var item = _context.Cart.SingleOrDefault(x => x.UserId == currentUser.id && x.ProductId == id && x.ProductFeatureId == productFeatureId);
                    if (item != null)
                    {
                        _context.Cart.Remove(item);
                        _context.SaveChanges();

                        return Success("محصول با موفقیت از سبد خرید حذف شد.");
                    }
                    else
                    {
                        return Error("محصول مورد نظر در سبد خرید یافت نشد.");
                    }
                }
                else
                {
                    if (Request.Cookies["Cart"] != null)
                    {
                        var value = Request.Cookies["Cart"].Value;
                        var cookieCart = JsonConvert.DeserializeObject<List<CartCookieViewModel>>(value);
                        if (cookieCart.Count(x => x.productId == id && x.productFeatureId == productFeatureId) > 0)
                        {
                            cookieCart.Remove(cookieCart.Single(x => x.productId == id && x.productFeatureId == productFeatureId));

                            var cookie = Request.Cookies["Cart"];
                            cookie.Value = JsonConvert.SerializeObject(cookieCart);
                            cookie.Expires = DateTime.Now.AddDays(10);
                            Response.Cookies.Add(cookie);

                            return Success("محصول با موفقیت از سبد خرید حذف شد.");
                        }
                        else
                        {
                            return Error("محصول مورد نظر در سبد خرید یافت نشد.");
                        }
                    }
                    else
                    {
                        return Error("محصول مورد نظر یافت نشد.");
                    }
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}