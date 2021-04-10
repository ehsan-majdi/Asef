namespace Asefian.Model.Metadata
{
    /// <summary>
    /// اطلاعات پایه ثابت
    /// </summary>
    public class AsefianMetadata
    {
        /// <summary>
        /// عنوان سایت
        /// </summary>
        public const string SiteName = "سیان";
        /// <summary>
        /// نام سایت
        /// </summary>
        public const string Site = "Asefian";
        /// <summary>
        /// نام توکن ذخیره شده
        /// </summary>
        public const string SiteToken = "AsefianToken";

        /// <summary>
        /// ردیف کشور ایران
        /// </summary>
        public const int IranIdKey = 1;

        /// <summary>
        /// مهلت استفاده از توکن ایمیل
        /// </summary>
        public const int EmailActiveTokenExpirateHour = 72;
        /// <summary>
        /// مهلت استفاده از توکن اس ام اس
        /// </summary>
        public const int SmsActiveTokenExpirateHour = 24;
        /// <summary>
        /// واحد پولی درج شده در سیستم
        /// </summary>
        public const string CurrencyName = "ریال";

        /// <summary>
        /// مقدار پیشفرض برای حداقل آماده سازی سفارش
        /// </summary>
        public const string DefaultGapDay = "1";
        /// <summary>
        /// حداقل مبلغ برای ارسال رایگان
        /// </summary>
        public const string DefaultMinDeliveryFreePrice = "1000000";
        /// <summary>
        /// هزینه پیشفرض حمل
        /// </summary>
        public const string DefaultDeliveryPrice = "150000";
    }
}
