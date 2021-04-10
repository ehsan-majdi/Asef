using System;
using System.Globalization;

namespace Asefian.Common.Utility
{
    /// <summary>
    /// کلاس کمکی برای کار کردن با تاریخ
    /// </summary>
    public class DateUtility
    {
        private static string[] dayOfWeek = new string[] { "شنبه", "یک شنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ به همراه ساعت شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ و ساعت کنار با فرمت استاندارد</returns>
        public static string GetPersianDateTime(DateTime dateTime, string delimiter = " ")
        {
            PersianCalendar Calendar = new PersianCalendar();
            string Year = Calendar.GetYear(dateTime).ToString();
            string Month = Calendar.GetMonth(dateTime) >= 10 ? Calendar.GetMonth(dateTime).ToString() : "0" + Calendar.GetMonth(dateTime);
            string Day = Calendar.GetDayOfMonth(dateTime) >= 10 ? Calendar.GetDayOfMonth(dateTime).ToString() : "0" + Calendar.GetDayOfMonth(dateTime);

            string TimeOfTheDay = dateTime.TimeOfDay.ToString();
            string hour = dateTime.Hour >= 10 ? dateTime.Hour.ToString() : "0" + dateTime.Hour;
            string minute = dateTime.Minute >= 10 ? dateTime.Minute.ToString() : "0" + dateTime.Minute;

            return Year + "/" + Month + "/" + Day + delimiter + hour + ":" + minute;
        }

        /// <summary>
        /// گرفتن ساعت یک شی زمان
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>ساعت کنار با فرمت استاندارد</returns>
        public static string GetTime(DateTime dateTime)
        {
            string hour = dateTime.Hour >= 10 ? dateTime.Hour.ToString() : "0" + dateTime.Hour;
            string minute = dateTime.Minute >= 10 ? dateTime.Minute.ToString() : "0" + dateTime.Minute;

            return hour + ":" + minute;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ و ساعت شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ و ساعت کنار با فرمت استاندارد</returns>
        public static string GetPersianDateTime(DateTime? dateTime)
        {
            if (dateTime != null)
                return GetPersianDateTime(dateTime.GetValueOrDefault());
            else
                return null;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 4 رقمی</returns>
        public static string GetPersianDate(DateTime dateTime)
        {
            PersianCalendar Calendar = new PersianCalendar();
            string Year = Calendar.GetYear(dateTime).ToString();
            string Month = Calendar.GetMonth(dateTime) >= 10 ? Calendar.GetMonth(dateTime).ToString() : "0" + Calendar.GetMonth(dateTime);
            string Day = Calendar.GetDayOfMonth(dateTime) >= 10 ? Calendar.GetDayOfMonth(dateTime).ToString() : "0" + Calendar.GetDayOfMonth(dateTime);

            return Year + "/" + Month + "/" + Day;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 4 رقمی</returns>
        public static string GetPersianShortDateString(DateTime? dateTime)
        {
            if (dateTime != null)
                return GetPersianShortDateString(dateTime.Value);
            else
                return null;
        }
        
        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 4 رقمی</returns>
        public static string GetPersianShortDateString(DateTime dateTime)
        {
            PersianCalendar Calendar = new PersianCalendar();
            string Day = Calendar.GetDayOfMonth(dateTime) >= 10 ? Calendar.GetDayOfMonth(dateTime).ToString() : "0" + Calendar.GetDayOfMonth(dateTime);

            return PersionDayOfWeek(dateTime) + " " + Day + " " + PersianGetMonthName(Calendar.GetMonth(dateTime));
        }

        /// <summary>
        /// دریافت عنوان فارسی ماه
        /// </summary>
        /// <param name="month">عدد ماه مورد نظر</param>
        /// <returns>عنوان ماه</returns>
        private static string PersianGetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// دریافت عنوان روز هفته از تاریخ
        /// </summary>
        /// <param name="date">تاریخ مورد نظر</param>
        /// <returns>عنوان روز مورد نظر</returns>
        private static string PersionDayOfWeek(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return dayOfWeek[0];
                case DayOfWeek.Sunday:
                    return dayOfWeek[1];
                case DayOfWeek.Monday:
                    return dayOfWeek[2];
                case DayOfWeek.Tuesday:
                    return dayOfWeek[3];
                case DayOfWeek.Wednesday:
                    return dayOfWeek[4];
                case DayOfWeek.Thursday:
                    return dayOfWeek[5];
                case DayOfWeek.Friday:
                    return dayOfWeek[6];
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 4 رقمی</returns>
        public static string GetPersianDate(DateTime? dateTime)
        {
            if (dateTime != null)
                return GetPersianDate(dateTime.GetValueOrDefault());
            else
                return null;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ کوتاه شمسی.
        /// منظور از سال کوتاه سال 2 رقمی می باشد
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 2 رقمی</returns>
        public static string GetShortPersianDate(DateTime dateTime)
        {
            PersianCalendar Calendar = new PersianCalendar();
            string Year = Calendar.GetYear(dateTime).ToString().Substring(2);
            string Month = Calendar.GetMonth(dateTime) >= 10 ? Calendar.GetMonth(dateTime).ToString() : "0" + Calendar.GetMonth(dateTime);
            string Day = Calendar.GetDayOfMonth(dateTime) >= 10 ? Calendar.GetDayOfMonth(dateTime).ToString() : "0" + Calendar.GetDayOfMonth(dateTime);

            return Year + "/" + Month + "/" + Day;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ کوتاه شمسی.
        /// منظور از سال کوتاه سال 2 رقمی می باشد
        /// </summary>
        /// <param name="dateTime">ورودی تاریخ میلادی مورد نظر</param>
        /// <returns>تاریخ با فرمت استاندارد و سال 2 رقمی</returns>
        public static string GetShortPersianDate(DateTime? dateTime)
        {
            if (dateTime != null)
                return GetShortPersianDate(dateTime.GetValueOrDefault());
            else
                return null;
        }

        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <param name="hour">ساعت</param>
        /// <param name="minute">دقیقه</param>
        /// <param name="second">روز</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string date, int hour = 0, int minute = 0, int second = 0)
        {
            string[] parts = date.Split('/', '-');
            PersianCalendar Calendar = new PersianCalendar();
            return Calendar.ToDateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), hour, minute, second, 0);
        }

        /// <summary>
        /// گرفتن سال شمسی از تاریخ میلادی
        /// </summary>
        /// <param name="dateTime">تاریخ میلادی</param>
        /// <returns>سال شمسی</returns>
        public static string GetPersianYearFromDateTime(DateTime dateTime)
        {
            PersianCalendar Calendar = new PersianCalendar();
            return Calendar.GetYear(dateTime).ToString();
        }

    }
}
