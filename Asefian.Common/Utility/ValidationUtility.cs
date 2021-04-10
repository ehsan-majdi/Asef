using System.Text.RegularExpressions;

namespace Asefian.Common.Utility
{
    /// <summary>
    /// کلاس کمکی برای اعتبار سنجی فیلد ها و مقادیر
    /// </summary>
    public class ValidationUtility
    {
        /// <summary>
        /// اعتبار سنجی آدرس ایمیل
        /// </summary>
        /// <param name="email">آدرس ایمیل</param>
        /// <returns>نتیجه بررسی ایمیل</returns>
        public static bool ValidateEmail(string email)
        {
            return Regex.Match(email, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase).Success;
        }

        /// <summary>
        /// بررسی شماره تلفن همراه
        /// </summary>
        /// <param name="mobileNumber">شماره تلفن همراه</param>
        /// <returns>نتیجه بررسی تلفن وارد شده</returns>
        public static bool ValidateMobileNumber(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber)) return false;

            if (mobileNumber.Length != 11) return false;

            if (!mobileNumber.StartsWith("09")) return false;

            return true;
        }

        /// <summary>
        /// بررسی صحت کد پستی
        /// </summary>
        /// <param name="postalCode">کد پستی</param>
        /// <returns>نتیجه بررسی صحت کد پستی</returns>
        public static bool ValidationPostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode)) return false;

            if (postalCode.Length != 10) return false;

            return true;
        }

        /// <summary>
        /// بررسی عرض جغرافیایی
        /// </summary>
        /// <param name="latitude">عرض جغرافیایی</param>
        /// <returns>نتیجه صحت عرض جغرافیایی</returns>
        public static bool ValidateLatitude(string latitude)
        {
            return Regex.Match(latitude, @"^(\\+|-)?(?:90(?:(?:\\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\\.[0-9]{1,6})?))$", RegexOptions.IgnoreCase).Success;
        }

        /// <summary>
        /// بررسی طول جغرافیایی
        /// </summary>
        /// <param name="longitude">طول جغرافیایی</param>
        /// <returns>نتیجه صحت طول جغرافیایی</returns>
        public static bool ValidateLongitude(string longitude)
        {
            return Regex.Match(longitude, @"^(\\+|-)?(?:180(?:(?:\\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\\.[0-9]{1,6})?))$", RegexOptions.IgnoreCase).Success;
        }

    }
}
