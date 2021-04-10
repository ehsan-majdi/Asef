using Asefian.Common.Web;
using Asefian.Model.Context;
using Asefian.Model.FileContext;
using Newtonsoft.Json;
using Asefian.Model.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر پایه که همه کنترلر ها از آن ارث بری میکنند
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        protected AsefianContext _context;

        public BaseController()
        {
            _context = new AsefianContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.User = GetAuthenticatedUser();
        }

        /// <summary>
        /// برگرداندن ویو مورد نظر بر اساس این که درخواست اجکس بوده یه رفرش صفحه
        /// که در صورت اجکس بودن درخواست بدون تم اصلی صفحه مورد نظر برگردانده می شود.
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult BaseView()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        /// <summary>
        /// برگرداندن ویو مورد نظر بر اساس این که درخواست اجکس بوده یه رفرش صفحه
        /// که در صورت اجکس بودن درخواست بدون تم اصلی صفحه مورد نظر برگردانده می شود.
        /// </summary>
        /// <param name="view">نام ویو مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult BaseView(string view)
        {
            if (Request.IsAjaxRequest())
                return PartialView(view);
            else
                return View(view);
        }

        /// <summary>
        /// برگرداندن ویو مورد نظر بر اساس این که درخواست اجکس بوده یه رفرش صفحه
        /// که در صورت اجکس بودن درخواست بدون تم اصلی صفحه مورد نظر برگردانده می شود.
        /// </summary>
        /// <param name="model">اطلاعاتی که به عنوان مدل به ویو فرستاده شود</param>
        /// <returns>صفحه مورد نظر</returns>
        public ActionResult BaseView(object model)
        {
            if (Request.IsAjaxRequest())
                return PartialView(model);
            else
                return View(model);
        }

        /// <summary>
        /// درخواست تاریخ جاری سیستم
        /// </summary>
        /// <returns></returns>
        protected DateTime GetDatetime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// دریافت آی پی ارسال کننده درخواست
        /// </summary>
        /// <returns>آی پی درخواست کننده</returns>
        protected string GetCurrentIp()
        {
            return Request.UserHostAddress;
        }

        /// <summary>
        /// ساخت یک رشته متن تصادفی
        /// </summary>
        /// <returns>رشته متن ساخته شده تصادفی</returns>
        protected string GetUniqueId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// گرفتن کاربر لاگین شده
        /// </summary>
        /// <returns>کاربر لاگین شده</returns>
        protected UserPrincipal GetAuthenticatedUser()
        {
            try
            {
                HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    return JsonConvert.DeserializeObject<UserPrincipal>(ticket.Name);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static UserPrincipal StaticGetAuthenticatedUser()
        {
            try
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    return JsonConvert.DeserializeObject<UserPrincipal>(ticket.Name);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// بازگشت جیسون به صورت نتیجه جستجوی موفقیت آمیر
        /// </summary>
        /// <param name="data">لیست دیتا</param>
        /// <param name="page">شماره صفحه</param>
        /// <param name="count">تعداد هر صفحه</param>
        /// <param name="total">تعداد کل دیتا</param>
        /// <param name="orderBy">فیلد مرتب سازی</param>
        /// <param name="orderType">نوع مرتب سازی</param>
        /// <returns></returns>
        protected JsonResult SuccessSearch<T>(List<T> data, int page = 1, int count = 0, int total = 0, string orderBy = "id", string orderType = "desc")
        {
            var searchResponse = new SearchResponse()
            {
                list = data,
                page = page,
                count = count,
                total = total,
                totalPage = (int)Math.Ceiling((double)total / count),
                orderBy = "id",
                orderType = "desc"
            };

            return Success(searchResponse);
        }

        /// <summary>
        /// دریافت نتیجه خروجی موفق برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Success()
        {
            return Result(ResponseStatus.Ok);
        }

        /// <summary>
        /// دریافت نتیجه خروجی موفق برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">پیام سمت سرور</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Success(string message)
        {
            return Result(message, ResponseStatus.Ok);
        }

        /// <summary>
        /// دریافت نتیجه خروجی موفق برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="data">دیتا نتیجه خروجی</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Success(object data)
        {
            Response response = new Response()
            {
                status = ResponseStatus.Ok,
                data = data
            };
            return Result(response);
        }

        /// <summary>
        /// دریافت نتیجه خروجی موفق برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">متن پیام ارسالی برای کاربر</param>
        /// <param name="data">دیتا نتیجه خروجی</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Success(string message, object data)
        {
            Response response = new Response()
            {
                status = ResponseStatus.Ok,
                message = message,
                data = data
            };
            return Result(response);
        }

        /// <summary>
        /// دریافت نتیجه خروجی همراه با خطا برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">پیام سمت سرور</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Error(string message)
        {
            return Result(message, ResponseStatus.InternalServerError);
        }

        /// <summary>
        /// دریافت نتیجه خروجی خطای سمت سرور برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult ServerError(Exception ex)
        {
            var message = "";
            if (GetLanguage().Id == Language.English.Id)
            {
                message = "serverError";
            }
            if (GetLanguage().Id == Language.Persian.Id)
            {
                message = "در هنگام اجرای عملیات خطایی رخ داد.";
            }
            return Result(message, ResponseStatus.InternalServerError);
        }

        /// <summary>
        /// دریافت نتیجه خروجی  برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="status">وضعیت</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Result(ResponseStatus status)
        {
            Response response = new Response()
            {
                status = status
            };
            return Result(response);
        }

        /// <summary>
        /// دریافت نتیجه خروجی  برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">پیام</param>
        /// <param name="status">وضعیت</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Result(string message, ResponseStatus status)
        {
            Response response = new Response()
            {
                status = status,
                message = message
            };
            return Result(response);
        }

        /// <summary>
        /// ساخت نتیجه خروجی  برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="response">شی پاسخ</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected JsonResult Result(Response response)
        {
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// دریافت نتیجه خروجی موفق برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="data">دیتا نتیجه خروجی</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected static Response ResponseSuccess(object data)
        {
            Response response = new Response()
            {
                status = ResponseStatus.Ok,
                data = data
            };
            return response;
        }

        /// <summary>
        /// دریافت نتیجه خروجی همراه با خطا برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">پیام سمت سرور</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected static Response ResponseError(string message)
        {
            return ResponseResult(message, ResponseStatus.InternalServerError);
        }

        /// <summary>
        /// ساخت نتیجه خروجی  برای بازگرداندن به درخواست کاربر
        /// </summary>
        /// <param name="message">پیام</param>
        /// <param name="status">وضعیت</param>
        /// <returns>جیسون خروجی از نوع پاسخ</returns>
        protected static Response ResponseResult(string message, ResponseStatus status)
        {
            return new Response
            {
                status = status,
                message = message
            };
        }



        /// <summary>
        /// تایید فایل تصاویر در کد های اچ تی ام ال
        /// </summary>
        /// <param name="text">متن اچ تی ام ال</param>
        protected void VerifyHtmlImageFile(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            foreach (Match item in Regex.Matches(text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase))
            {
                string matchString = item.Groups[1].Value;
                var imageParts = matchString.Split('/');
                AsefianFileContextHelper.VerifyFile(imageParts[3], imageParts[4]);
            }
        }

        /// <summary>
        /// تایید فایل تصاویر در کد های اچ تی ام ال
        /// </summary>
        /// <param name="text">متن اچ تی ام ال</param>
        protected void DeleteHtmlImageFile(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            var currentUser = GetAuthenticatedUser();

            foreach (Match item in Regex.Matches(text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase))
            {
                string matchString = item.Groups[1].Value;
                var imageParts = matchString.Split('/');
                AsefianFileContextHelper.DeleteFile(imageParts[3], imageParts[4], currentUser.id);
            }
        }

        /// <summary>
        /// ساخت لینک متناسب با زبان سیستم
        /// </summary>
        /// <param name="link">لینک مورد نظر بدون پیشوند زبان</param>
        /// <returns>لینک زبان ساخته شده</returns>
        public static string Link(string link)
        {
            return "/" + GetLanguageText() + link;
        }

        /// <summary>
        /// دریافت زبان جاری درخواست اچ تی تی پی به صورت متنی
        /// </summary>
        /// <returns>زبان جاری درخواست</returns>
        public static string GetLanguageText()
        {
            var context = new HttpContextWrapper(System.Web.HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(context);
            var language = (string)routeData.Values["lang"];

            if (string.IsNullOrEmpty(language)) language = "fa";

            return language;
        }

        /// <summary>
        /// دریافت زبان جاری درخواست اچ تی تی پی
        /// </summary>
        /// <returns>شی زبان سیستم</returns>
        public static Language GetLanguage()
        {
            switch (GetLanguageText().ToLower())
            {
                case "fa":
                    return Language.Persian;
                case "en":
                    return Language.English;
                default:
                    throw new Exception("language not found");
            }
        }

        /// <summary>
        /// دریافت آدرس و تبدیل لینک آن به فارسی
        /// </summary>
        /// <param name="path">آدرس مورد نظر</param>
        /// <returns>تبدیل آدرس وارد شده به آدرس فارسی و باز گرداندن آن به صورت رشته متنی</returns>
        public static string GetPersianUrl(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "/" || path == "/en" || path == "/en/")
                return "/fa";

            if (path.StartsWith("/en/"))
                return path.Replace("/en/", "/fa/");
            else
                return path;
        }

        /// <summary>
        /// دریافت آدرس و تبدیل لینک آن به انگلیسی
        /// </summary>
        /// <param name="path">آدرس مورد نظر</param>
        /// <returns>تبدیل آدرس وارد شده به آدرس انگلیسی و باز گرداندن آن به صورت رشته متنی</returns>
        public static string GetEnglishUrl(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "/" || path == "/fa" || path == "/fa/")
                return "/en";

            if (path.StartsWith("/fa/"))
                return path.Replace("/fa/", "/en/");
            else
                return path;
        }

        public static string MakeLanguageUrl(string url)
        {
            if (GetLanguage().Id == Language.English.Id)
            {
                return "/en" + url;
            }
            else
            {
                return "/fa" + url;

            }
        }
    }
}