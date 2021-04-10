using Asefian.Common.Utility;
using Asefian.Model.Context;
using Asefian.Model.Context.Core;
using Asefian.Model.FileContext;
using Asefian.Model.Metadata;
using Asefian.Model.ViewModel.Core;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// تنظیمات سایت
    /// </summary>
    [Authorize(Roles = "admin, settings")]
    public class SiteController : BaseController
    {
        /// <summary>
        /// تنظیمات سایت
        /// </summary>
        /// <returns>صفحه تنظیمات سایت</returns>
        public ActionResult Settings()
        {
            return BaseView();
        }

        /// <summary>
        /// تنظیمات صفحات سایت
        /// </summary>
        /// <param name="id">ردیف سایت</param>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult Page(string id)
        {
            var keys = new string[] {   BaseInformationKey.AboutUs,
                                        BaseInformationKey.eBusinessLaw,
                                        BaseInformationKey.Solicitorship,
                                        BaseInformationKey.ExportProduct,
                                        BaseInformationKey.HowToOrder,
                                        BaseInformationKey.TrackingOrder,
                                        BaseInformationKey.Delivery,
                                        BaseInformationKey.PaymentTerms,
                                        BaseInformationKey.ReturnPolicy,
                                        BaseInformationKey.Warranty
                                    };

            if (!keys.Any(x => x.ToLower() == id.ToLower()))
            {
                return HttpNotFound();
            }

            if (id.ToLower() == BaseInformationKey.AboutUs.ToLower())
            {
                ViewBag.Title = "درباره ما";
            }
            else if (id.ToLower() == BaseInformationKey.eBusinessLaw.ToLower())
            {
                ViewBag.Title = "قانون مندی کسب و کار اینترنتی";
            }
            else if (id.ToLower() == BaseInformationKey.Solicitorship.ToLower())
            {
                ViewBag.Title = "اخذ نمایندگی";
            }
            else if (id.ToLower() == BaseInformationKey.ExportProduct.ToLower())
            {
                ViewBag.Title = "صادرات";
            }
            else if (id.ToLower() == BaseInformationKey.HowToOrder.ToLower())
            {
                ViewBag.Title = "ثبت سفارش";
            }
            else if (id.ToLower() == BaseInformationKey.TrackingOrder.ToLower())
            {
                ViewBag.Title = "رهگیری سفارشات";
            }
            else if (id.ToLower() == BaseInformationKey.Delivery.ToLower())
            {
                ViewBag.Title = "ارسال سفارش";
            }
            else if (id.ToLower() == BaseInformationKey.PaymentTerms.ToLower())
            {
                ViewBag.Title = "شیوه های پرداخت";
            }
            else if (id.ToLower() == BaseInformationKey.ReturnPolicy.ToLower())
            {
                ViewBag.Title = "رویه بازگرداندن کالا";
            }
            else if (id.ToLower() == BaseInformationKey.Warranty.ToLower())
            {
                ViewBag.Title = "خدمات پس از فروش";
            }

            var persianData = _context.BaseInformation.Where(x => x.Key == id && x.LanguageId == Language.Persian.Id).SingleOrDefault()?.Value;
            var englishData = _context.BaseInformation.Where(x => x.Key == id && x.LanguageId == Language.English.Id).SingleOrDefault()?.Value;
            ViewBag.Key = id;
            ViewBag.PersianData = persianData;
            ViewBag.EnglishData = englishData;
            return BaseView();
        }

        /// <summary>
        /// ذخیره اطلاعات صفحه
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات صفحه</param>
        /// <returns>نتیجه ذخیره صفحه</returns>
        public JsonResult SavePage(BaseInformationViewModel model)
        {
            try
            {
                var keys = new string[] {   BaseInformationKey.AboutUs,
                                            BaseInformationKey.eBusinessLaw,
                                            BaseInformationKey.Solicitorship,
                                            BaseInformationKey.ExportProduct,
                                            BaseInformationKey.HowToOrder,
                                            BaseInformationKey.TrackingOrder,
                                            BaseInformationKey.Delivery,
                                            BaseInformationKey.PaymentTerms,
                                            BaseInformationKey.ReturnPolicy,
                                            BaseInformationKey.Warranty
                                        };

                if (!keys.Any(x => x.ToLower() == model.key.ToLower()))
                {
                    return Error("اطلاعات مورد نظر صحیح نیست.");
                }

                var currentUser = GetAuthenticatedUser();
                foreach (var item in model.translations)
                {
                    var entity = _context.BaseInformation.Where(x => x.Key.ToLower() == model.key.ToLower() && x.LanguageId == item.languageId).SingleOrDefault();
                    if (entity == null)
                    {
                        entity = new BaseInformation
                        {
                            Key = model.key,
                            Value = item.value.ToStandardPersian(),
                            LanguageId = item.languageId,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };

                        _context.BaseInformation.Add(entity);
                    }
                    else
                    {
                        entity.Key = model.key;
                        entity.Value = item.value.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                }


                _context.SaveChanges();

                return Success("اطلاعات صفحه با موفقیت به روزرسانی شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
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
                AsefianFileContextHelper.DeleteFile(id, fileName, currentUser.id);
                return Success("فایل با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// صفحه تعرفه های ارسال فروشگاه
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Tariff()
        {
            var items = new string[] {
                                        BaseInformationKey.OrderGapDay,
                                        BaseInformationKey.MinDeliveryFreePrice,
                                        BaseInformationKey.DeliveryPrice
                                    };

            var data = _context.BaseInformation.Where(x => items.Any(y => y == x.Key)).ToList();
            return BaseView(data);
        }

        /// <summary>
        /// ذخیره تعرفه های ثبت شده توسط کاربر
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Tariff(TariffViewModel model)
        {
            try
            {
                if (model.orderGapDay <= 0)
                {
                    return Error("وارد کردن فاصله زمانی روز که کاربر می بایست حداقل صبر کند اجباری است.");
                }

                if (model.minDeliveryFreePrice < 0)
                {
                    return Error("میزان حداقل سفارش برای ارسال رایگان باید بزرگتر از صفر باشد.");
                }

                if (model.deliveryPrice < 0)
                {
                    return Error("هزینه حمل می بایست عدد بزرگتر از صفر باشد.");
                }

                var currentUser = GetAuthenticatedUser();

                var items = new string[] {
                                        BaseInformationKey.OrderGapDay,
                                        BaseInformationKey.MinDeliveryFreePrice,
                                        BaseInformationKey.DeliveryPrice
                                    };

                var data = _context.BaseInformation.Where(x => items.Any(y => y == x.Key)).ToList();

                var orderGapDay = data.SingleOrDefault(x => x.Key == BaseInformationKey.OrderGapDay);
                if (orderGapDay != null)
                {
                    orderGapDay.ModifyUserId = currentUser.id;
                    orderGapDay.Value = model.orderGapDay.ToString();
                    orderGapDay.ModifyDate = GetDatetime();
                    orderGapDay.ModifyIp = GetCurrentIp();
                }
                else
                {
                    orderGapDay = new BaseInformation();

                    orderGapDay.Key = BaseInformationKey.OrderGapDay;
                    orderGapDay.ModifyUserId = currentUser.id;
                    orderGapDay.Value = model.orderGapDay.ToString();
                    orderGapDay.ModifyDate = GetDatetime();
                    orderGapDay.ModifyIp = GetCurrentIp();

                    _context.BaseInformation.Add(orderGapDay);
                }

                var minDeliveryFreePrice = data.SingleOrDefault(x => x.Key == BaseInformationKey.MinDeliveryFreePrice);
                if (minDeliveryFreePrice != null)
                {
                    minDeliveryFreePrice.ModifyUserId = currentUser.id;
                    minDeliveryFreePrice.Value = model.minDeliveryFreePrice.ToString();
                    minDeliveryFreePrice.ModifyDate = GetDatetime();
                    minDeliveryFreePrice.ModifyIp = GetCurrentIp();
                }
                else
                {
                    minDeliveryFreePrice = new BaseInformation();

                    minDeliveryFreePrice.Key = BaseInformationKey.MinDeliveryFreePrice;
                    minDeliveryFreePrice.ModifyUserId = currentUser.id;
                    minDeliveryFreePrice.Value = model.minDeliveryFreePrice.ToString();
                    minDeliveryFreePrice.ModifyDate = GetDatetime();
                    minDeliveryFreePrice.ModifyIp = GetCurrentIp();

                    _context.BaseInformation.Add(minDeliveryFreePrice);
                }

                var deliveryPrice = data.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryPrice);
                if (deliveryPrice != null)
                {
                    deliveryPrice.ModifyUserId = currentUser.id;
                    deliveryPrice.Value = model.deliveryPrice.ToString();
                    deliveryPrice.ModifyDate = GetDatetime();
                    deliveryPrice.ModifyIp = GetCurrentIp();
                }
                else
                {
                    deliveryPrice = new BaseInformation();

                    deliveryPrice.Key = BaseInformationKey.DeliveryPrice;
                    deliveryPrice.ModifyUserId = currentUser.id;
                    deliveryPrice.Value = model.deliveryPrice.ToString();
                    deliveryPrice.ModifyDate = GetDatetime();
                    deliveryPrice.ModifyIp = GetCurrentIp();

                    _context.BaseInformation.Add(deliveryPrice);
                }

                _context.SaveChanges();
                return Success("اطلاعات با موفقیت به روز رسانی شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// لیست شهرهای تحت پوشش پیک
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Delivery()
        {
            return BaseView();
        }

        /// <summary>
        /// ذخیره شهر تحت پوشش پیک
        /// </summary>
        /// <param name="cityId">ردیف شهر</param>
        /// <returns>نتیجه ذخیره</returns>
        [HttpPost]
        public JsonResult SaveDelivery(int cityId)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var baseInformation = _context.BaseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity);
                if (baseInformation != null)
                {
                    var cities = baseInformation.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    if (!cities.Contains(cityId))
                    {
                        cities.Add(cityId);
                    }

                    baseInformation.Value = string.Join("-", cities);
                    baseInformation.ModifyUserId = currentUser.id;
                    baseInformation.ModifyDate = GetDatetime();
                    baseInformation.ModifyIp = GetCurrentIp();
                }
                else
                {
                    baseInformation = new BaseInformation();

                    baseInformation.Key = BaseInformationKey.DeliveryManCity;
                    baseInformation.Value = cityId.ToString();
                    baseInformation.ModifyUserId = currentUser.id;
                    baseInformation.ModifyDate = GetDatetime();
                    baseInformation.ModifyIp = GetCurrentIp();

                    _context.BaseInformation.Add(baseInformation);
                }

                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف شهر تحت پوشش پیک
        /// </summary>
        /// <param name="id">ردیف شهر</param>
        /// <returns>نتیجه ذخیره</returns>
        [HttpPost]
        public JsonResult RemoveDelivery(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var baseInformation = _context.BaseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity);
                if (baseInformation != null)
                {
                    var cities = baseInformation.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    if (cities.Contains(id))
                    {
                        cities.Remove(id);
                    }

                    baseInformation.Value = string.Join("-", cities);
                    baseInformation.ModifyUserId = currentUser.id;
                    baseInformation.ModifyDate = GetDatetime();
                    baseInformation.ModifyIp = GetCurrentIp();


                    if (cities.Count == 0) _context.BaseInformation.Remove(baseInformation);
                }

                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// لیست شهر تحت پوشش پیک
        /// </summary>
        /// <returns>لیست شهرها</returns>
        [HttpGet]
        public JsonResult GetCityDeliveryList()
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var baseInformation = _context.BaseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.DeliveryManCity);

                if (baseInformation != null)
                {

                    var cities = baseInformation.Value.Split('-').Select(x => int.Parse(x)).ToList();

                    var data = _context.Location.Where(x => cities.Any(y => y == x.Id)).Select(x => new
                    {
                        id = x.Id,
                        title = x.Title
                    }).ToList();

                    return SuccessSearch(data);
                }
                else
                {
                    return SuccessSearch(new List<int>());
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// اطلاعات تماس با ما
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Contact()
        {
            var variable = new string[] { BaseInformationKey.SiteAddress, BaseInformationKey.SitePhoneNumber, BaseInformationKey.SiteFax, BaseInformationKey.SiteEmail, BaseInformationKey.SiteLocation };
            var persianData = _context.BaseInformation.Where(x => x.Key == BaseInformationKey.SiteAddress && x.LanguageId == Language.Persian.Id).SingleOrDefault()?.Value;
            var englishData = _context.BaseInformation.Where(x => x.Key == BaseInformationKey.SiteAddress && x.LanguageId == Language.English.Id).SingleOrDefault()?.Value;
            ViewBag.PersianData = persianData;
            ViewBag.EnglishData = englishData;
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return View(data);
        }

        /// <summary>
        /// صفحه تنظیمات شبکه های اجتماعی
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult SocialNetwork()
        {
            var variable = new string[] { BaseInformationKey.SiteInstagram, BaseInformationKey.SiteTelegram, BaseInformationKey.SiteFacebook, BaseInformationKey.SiteYoutube, BaseInformationKey.SiteAparat, BaseInformationKey.SiteTwitter };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return View(data);
        }

        /// <summary>
        /// دخیره اطلاعات تماس سایت
        /// </summary>
        /// <param name="model">مدل اطلاعات تماس</param>
        /// <returns>نتیجه ذخیره</returns>
        [HttpPost]
        public JsonResult SaveContact(SiteContactViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var variable = new string[] { BaseInformationKey.SiteAddress, BaseInformationKey.SitePhoneNumber, BaseInformationKey.SiteFax, BaseInformationKey.SiteEmail, BaseInformationKey.SiteLocation };
                var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();

                foreach (var item in model.translations)
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteAddress) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteAddress && x.LanguageId == item.languageId);
                        entity.Value = item.value.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteAddress,
                            Value = item.value.ToStandardPersian(),
                            LanguageId = item.languageId.Value,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }


                }
                if (!string.IsNullOrEmpty(model.phoneNumber))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SitePhoneNumber) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SitePhoneNumber);
                        entity.Value = model.phoneNumber.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SitePhoneNumber,
                            Value = model.phoneNumber.ToStandardPersian(),
                            LanguageId = 1,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SitePhoneNumber) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SitePhoneNumber));
                    }
                }

                if (!string.IsNullOrEmpty(model.fax))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteFax) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteFax);
                        entity.Value = model.fax.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteFax,
                            Value = model.fax.ToStandardPersian(),
                            LanguageId = 1,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteEmail) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteEmail));
                    }
                }

                if (!string.IsNullOrEmpty(model.email))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteEmail) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteEmail);
                        entity.Value = model.email.Trim();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteEmail,
                            Value = model.email.Trim(),
                            LanguageId = 1,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteEmail) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteEmail));
                    }
                }

                if (!string.IsNullOrEmpty(model.latitude) && !string.IsNullOrEmpty(model.longitude))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteLocation) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteLocation);
                        entity.Value = model.latitude.Trim() + "/" + model.longitude.Trim();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteLocation,
                            Value = model.latitude.Trim() + "/" + model.longitude.Trim(),
                            LanguageId = 1,
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else if (!string.IsNullOrEmpty(model.latitude) || !string.IsNullOrEmpty(model.longitude))
                {
                    return Error("برای نمایش نقشه در فوتر سایت وارد کردن طول و عرض جغرافیایی اجباری است.");
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteLocation) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteLocation));
                    }
                }

                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }


        /// <summary>
        /// دخیره اطلاعات تماس سایت
        /// </summary>
        /// <param name="model">مدل اطلاعات تماس</param>
        /// <returns>نتیجه ذخیره</returns>
        [HttpPost]
        public JsonResult SaveSocialNetwork(SiteSocialNetworkViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var variable = new string[] { BaseInformationKey.SiteInstagram, BaseInformationKey.SiteTelegram, BaseInformationKey.SiteFacebook, BaseInformationKey.SiteYoutube, BaseInformationKey.SiteAparat, BaseInformationKey.SiteTwitter };
                var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();

                if (!string.IsNullOrEmpty(model.instagram))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteInstagram) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteInstagram);
                        entity.Value = model.instagram.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteInstagram,
                            Value = model.instagram.ToStandardPersian(),
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteInstagram) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteInstagram));
                    }
                }

                if (!string.IsNullOrEmpty(model.telegram))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteTelegram) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteTelegram);
                        entity.Value = model.telegram.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteTelegram,
                            Value = model.telegram.ToStandardPersian(),
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteTelegram) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteTelegram));
                    }
                }

                if (!string.IsNullOrEmpty(model.facebook))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteFacebook) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteFacebook);
                        entity.Value = model.facebook.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteFacebook,
                            Value = model.facebook.ToStandardPersian(),
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteFacebook) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteFacebook));
                    }
                }

                if (!string.IsNullOrEmpty(model.youtube))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteYoutube) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteYoutube);
                        entity.Value = model.youtube.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteYoutube,
                            Value = model.youtube.ToStandardPersian(),
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteYoutube) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteYoutube));
                    }
                }

                if (!string.IsNullOrEmpty(model.twitter))
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteTwitter) > 0)
                    {
                        var entity = data.Single(x => x.Key == BaseInformationKey.SiteTwitter);
                        entity.Value = model.twitter.ToStandardPersian();
                        entity.ModifyUserId = currentUser.id;
                        entity.ModifyDate = GetDatetime();
                        entity.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        var entity = new BaseInformation
                        {
                            Key = BaseInformationKey.SiteTwitter,
                            Value = model.twitter.ToStandardPersian(),
                            ModifyUserId = currentUser.id,
                            ModifyDate = GetDatetime(),
                            ModifyIp = GetCurrentIp()
                        };
                        _context.BaseInformation.Add(entity);
                    }
                }
                else
                {
                    if (data.Count(x => x.Key == BaseInformationKey.SiteTwitter) > 0)
                    {
                        _context.BaseInformation.Remove(data.Single(x => x.Key == BaseInformationKey.SiteTwitter));
                    }
                }

                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}