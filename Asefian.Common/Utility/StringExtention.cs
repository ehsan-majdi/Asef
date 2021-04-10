using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// متد های اضافه شده به کلاس استرینگ
    /// </summary>
    public static class StringExtention
    {
        /// <summary>
        /// تبدیل رشته عربی و تبدیل حروف عربی به فارسی
        /// </summary>
        /// <param name="input">متن ورودی</param>
        /// <returns>متن خروجی تبدیل شده به فارسی</returns>
        public static string ToPersian(this string input)
        {
            return (string.IsNullOrEmpty(input) ? input : input.Replace("ي", "ی").Replace("ك", "ک"));
        }

        /// <summary>
        /// جایگزینی اعداد داخل رشته به اعداد فارسی
        /// </summary>
        /// <param name="input">متن</param>
        /// <returns>متن با اعداد فارسی شده</returns>
        public static string ToPersianNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Replace("1", "۱");
            input = input.Replace("2", "۲");
            input = input.Replace("3", "۳");
            input = input.Replace("4", "۴");
            input = input.Replace("5", "۵");
            input = input.Replace("6", "۶");
            input = input.Replace("7", "۷");
            input = input.Replace("8", "۸");
            input = input.Replace("9", "۹");
            input = input.Replace("0", "۰");

            return input.Trim();
        }

        /// <summary>
        /// تبدیل رشته فارسی و تبدیل حروف فارسی به عربی
        /// </summary>
        /// <param name="input">متن ورودی</param>
        /// <returns>متن خروجی تبدیل شده به عربی</returns>
        public static string ToArabic(this string input)
        {
            return (string.IsNullOrEmpty(input) ? input : input.Replace("ی", "ي").Replace("ک", "ك"));
        }

        /// <summary>
        /// استاندارد کردن متن فارسی و جایگزینی حروف فارسی
        /// </summary>
        /// <param name="input">متن فارسی</param>
        /// <returns>متن فارسی استاندارد شده</returns>
        public static string ToStandardPersian(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Replace("الله", "اله");
            input = input.Replace("أ", "ا");
            input = input.Replace("إ", "ا");
            input = input.Replace("ك", "ک");
            input = input.Replace("ي", "ی");
            input = input.Replace("ئ", "ی");
            input = input.Replace("ؤ", "و");
            input = input.Replace("ة", "ه");
            input = input.Replace("ۀ", "ه");
            input = input.Replace("ء", "ی");
            input = input.Replace("وو", "و");
            input = input.Replace("ئو", "و");

            input = input.Replace("\u064B", string.Empty); //tanvin-ann
            input = input.Replace("\u064C", string.Empty); //tanvin-onn
            input = input.Replace("\u064D", string.Empty); //tanvin-enn
            input = input.Replace("\u064E", string.Empty); //fathe
            input = input.Replace("\u064F", string.Empty); //zamme
            input = input.Replace("\u0650", string.Empty); //kasre            
            input = input.Replace("\u0651", string.Empty); //tashdid
            input = input.Replace("\u0654", string.Empty); //hamza-high
            input = input.Replace("\u0655", string.Empty); //hamza-low
            input = input.Replace("\u0674", string.Empty); //hamza

            input = input.Replace("۱", "1");
            input = input.Replace("۲", "2");
            input = input.Replace("۳", "3");
            input = input.Replace("۴", "4");
            input = input.Replace("۵", "5");
            input = input.Replace("۶", "6");
            input = input.Replace("۷", "7");
            input = input.Replace("۸", "8");
            input = input.Replace("۹", "9");
            input = input.Replace("۰", "0");

            return input.Trim();
        }

        /// <summary>
        /// کوچک کردن حرف اول یک رشته متنی
        /// </summary>
        /// <param name="input">متن ورودی</param>
        /// <returns>متنی که حرف اول آن کوچک شده است</returns>
        public static string FirstCharToLower(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }

        /// <summary>
        /// تبدیل یک رشته و حذف کردن حروف اضافه آن برای نمایش در آدرس
        /// </summary>
        /// <param name="input">متن ورودی</param>
        /// <returns>رشته مورد نظر برای نمایش در آدرس باز</returns>
        public static string ToUrlString(this string input)
        {
            input = input.Replace(" ", "_");
            input = input.Replace(".", "_");
            input = input.Replace("-", "_");
            input = input.Replace("%", "");
            input = input.Replace("/", "");
            input = input.Replace("|", "");
            input = input.Replace("*", "_");

            while(input.IndexOf("__") > 0)
            {
                input = input.Replace("__", String.Empty);
            }

            return input;
        }

        /// <summary>
        /// تبدیل یه متن به عدد
        /// </summary>
        /// <param name="input">متن</param>
        /// <returns>عدد</returns>
        public static long ToLong(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            return long.Parse(input);
        }
    }

}
