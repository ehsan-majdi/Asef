using Nig.Common.Security;
using Nig.Model.Context.Account;
using Nig.Model.Metadata;

namespace Nig.Web.Helper
{
    /// <summary>
    /// کلاس کمکی برای استفاده از قالب ها
    /// </summary>
    public class TemplateHelper
    {
        /// <summary>
        /// ساخت و قراردادن متغیر های کاربر در قالب مورد استفاده
        /// </summary>
        /// <param name="template">قالب</param>
        /// <param name="user">کاربر</param>
        /// <returns>قالب که با متغیر های مورد نظر پر شده است</returns>
        public static void Manipulate(ref string template, User user)
        {
            template = template.Replace(BaseInformationKey.UserVariable_FullName, GetSexText(user.SexId));
            template = template.Replace(BaseInformationKey.UserVariable_FullName, user.FullName);
            template = template.Replace(BaseInformationKey.UserVariable_Mobile, user.MobileNumber);
            template = template.Replace(BaseInformationKey.UserVariable_Email, user.Email);
            template = template.Replace(BaseInformationKey.UserVariable_Password, PasswordUtility.Decrypt(user.Password));
        }

        /// <summary>
        /// ساخت و قراردادن متغیر های کد فعال سازی در قالب مورد استفاده
        /// </summary>
        /// <param name="template">قالب</param>
        /// <param name="token">توکن ساخته شده</param>
        /// <returns>قالب که با متغیر های مورد نظر پر شده است</returns>
        public static void Manipulate(ref string template, UserVerifyToken token)
        {
            template = template.Replace(BaseInformationKey.Variable_Token, token.Token);
        }

        /// <summary>
        /// عنوان مناسب برای جنسیت
        /// </summary>
        /// <param name="sexId">ردیف جنسیت</param>
        /// <returns>عنوان مناسب که به برای آقا یا خانم انتخاب می شود.</returns>
        private static string GetSexText(int? sexId)
        {
            switch (sexId)
            {
                case 1:
                    return "جناب آقای";
                case 2:
                    return "سرکار خانم";
                default:
                    return "";
            }
        }
    }
}