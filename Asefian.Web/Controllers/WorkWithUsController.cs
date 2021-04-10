using Asefian.Model.Context.Support;
using Asefian.Model.Context.Support.Enum;
using Asefian.Model.ViewModel.Account;
using Asefian.Model.ViewModel.Support;
using Nig.Web.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    public class WorkWithUsController : BaseController
    {
        /// <summary>
        /// صفحه اصلی همکاری با ما
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ذخیره پیام و ارسال پیام کاربر به سمت پنل کاربری
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WorkWithUs")]
        [AllowAnonymous]
        public ActionResult Index(WorkWithUsViewModel model)
        {

            if (!CaptchaImage.isValid(model.captcha))
            {
                ViewBag.Error = "عبارت امنیتی به درستی وارد نشده است.";
                return BaseView(model);
            }
            if (string.IsNullOrEmpty(model.fullName))
            {
                ViewBag.Error = "عبارت امنیتی به درستی وارد نشده است.";
                return BaseView(model);
            }
            if (string.IsNullOrEmpty(model.mobileNumber) && model.mobileNumber.Length < 6)
            {
                ViewBag.Error = "تلفن تماس به درستی وارد نشده است.";
                return BaseView(model);
            }
            if (string.IsNullOrEmpty(model.message))
            {
                ViewBag.Error = "وارد کردن متن پیام اجباری است.";
                return BaseView(model);
            }

            UserPrincipal currentUser = null;
            if (Request.IsAuthenticated)
                currentUser = GetAuthenticatedUser();

            var message = new MessageBox()
            {
                FullName = model.fullName.ToStandardPersian(),
                MobileNumber = model.mobileNumber,
                Email = model.email,
                Text = model.message.ToStandardPersian(),
                StatusId = MessageBoxStatus.New.Id,
                MessageTypeId = 2,
                CreateUserId = currentUser?.id,
                ModifyUserId = currentUser?.id,
                CreateDate = GetDatetime(),
                ModifyDate = GetDatetime(),
                CreateIp = GetCurrentIp(),
                ModifyIp = GetCurrentIp(),
            };
            _context.MessageBox.Add(message);
            _context.SaveChanges();

            ViewBag.Success = "پیام شما با موفقیت ثبت شد.";
            return BaseView();
        }
    }
}