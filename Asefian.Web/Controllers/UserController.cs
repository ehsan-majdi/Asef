using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Metadata;
using Asefian.Web.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر کاربر
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// درخواست نمایش صفحه ورود
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login(string message)
        {
            if (Request.IsAuthenticated)
                return Redirect("/");

            ViewBag.Message = message;
            return BaseView();
        }

        /// <summary>
        /// درخواست صفحه ثبت نام
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
                return Redirect("/");

            return BaseView();
        }

        /// <summary>
        /// درخواست صفحه فراموشی رمزعبور
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("forgetPassword")]
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            if (Request.IsAuthenticated)
                return Redirect("/");

            return BaseView();
        }

        /// <summary>
        /// درخواست صفحه تغییر رمز عبور
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("changePassword")]
        public ActionResult ChangePassword()
        {
            return BaseView();
        }

        /// <summary>
        /// درخواست خروج از سیستم
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("logout")]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Response.Cookies.Remove(AsefianMetadata.SiteToken);
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        /// <summary>
        /// پروفایل اطلاعات حساب کاربری
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("{lang}/profile")]
        public ActionResult UserProfile()
        {
            var currentuser = GetAuthenticatedUser();
            var user = _context.User.Where(x => x.Id == currentuser.id && x.StatusId != UserStatus.Deleted.Id).Single();
            return BaseView(user);
        }

        /// <summary>
        /// ویرایش پروفایل اطلاعات حساب کاربری
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Route("profile/edit")]
        public ActionResult EditUserProfile()
        {
            var currentuser = GetAuthenticatedUser();
            var user = _context.User.Where(x => x.Id == currentuser.id && x.StatusId != UserStatus.Deleted.Id).Single();
            return BaseView(user);
        }
    }
}