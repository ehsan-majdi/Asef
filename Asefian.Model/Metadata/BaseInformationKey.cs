namespace Asefian.Model.Metadata
{
    /// <summary>
    /// کلید مقادیر اطلاعات پایه
    /// </summary>
    public class BaseInformationKey
    {
        /// <summary>
        /// متن درباره ما
        /// </summary>
        public const string AboutUs = "AboutUs";
        /// <summary>
        /// قانون مندی کسب و کار اینترنتی
        /// </summary>
        public const string eBusinessLaw = "eBusinessLaw";
        /// <summary>
        /// اخذ نمایندگی
        /// </summary>
        public const string Solicitorship = "Solicitorship";
        /// <summary>
        /// صادرات محصول
        /// </summary>
        public const string ExportProduct = "ExportProduct";
        /// <summary>
        /// ثبت سفارش
        /// </summary>
        public const string HowToOrder = "HowToOrder";
        /// <summary>
        /// رهگیری سفارشات
        /// </summary>
        public const string TrackingOrder = "TrackingOrder";
        /// <summary>
        /// ارسال سفارش
        /// </summary>
        public const string Delivery = "Delivery";
        /// <summary>
        /// شیوه های پرداخت
        /// </summary>
        public const string PaymentTerms = "PaymentTerms";
        /// <summary>
        /// رویه بازگرداندن کالا
        /// </summary>
        public const string ReturnPolicy = "ReturnPolicy";
        /// <summary>
        /// خدمات پس از فروش
        /// </summary>
        public const string Warranty = "Warranty";

        /// <summary>
        /// قالب فعال سازی اس ام اس
        /// </summary>
        public const string ActiveSmsTemplate = "ActiveSmsTemplate";
        /// <summary>
        /// قالب فعال سازی ایمیل
        /// </summary>
        public const string ActiveEmailTemplate = "ActiveEmailTemplate";

        /// <summary>
        /// قالب اس ام اس فراموشی رمزعبور
        /// </summary>
        public const string ForgetPasswordSmsTemplate = "ForgetPasswordSmsTemplate";
        /// <summary>
        /// قالب ایمیل فراموشی رمزعبور
        /// </summary>
        public const string ForgetPasswordEmailTemplate = "ForgetPasswordEmailTemplate";
        /// <summary>
        /// فاصله زمانی روز که کاربر می بایست حداقل صبر کند تا سفارش خود را تحویل بگیرد
        /// </summary>
        public const string OrderGapDay = "OrderGapDay";
        /// <summary>
        /// حداقل سفارش برای ارسال رایگان
        /// </summary>
        public const string MinDeliveryFreePrice = "MinDeliveryFreePrice";
        /// <summary>
        /// هزینه ارسال
        /// </summary>
        public const string DeliveryPrice = "DeliveryPrice";
        /// <summary>
        /// شهرهایی که تحویل پیک در آن ها وجود دارد.
        /// </summary>
        public const string DeliveryManCity = "DeliveryManCity";

        /// <summary>
        /// آدرس سایت
        /// </summary>
        public const string SiteAddress = "SiteAddress";
        /// <summary>
        /// تلفن تماس سایت
        /// </summary>
        public const string SitePhoneNumber = "SitePhoneNumber";
        /// <summary>
        /// فکس سایت
        /// </summary>
        public const string SiteFax = "SiteFax";
        /// <summary>
        /// ایمیل سایت
        /// </summary>
        public const string SiteEmail = "SiteEmail";
        /// <summary>
        /// موقعیت روی نقشه
        /// </summary>
        public const string SiteLocation = "SiteLocation";
        /// <summary>
        /// آدرس اینستاگرام
        /// </summary>
        public const string SiteInstagram = "SiteInstagram";
        /// <summary>
        /// آدرس تلگرام
        /// </summary>
        public const string SiteTelegram = "SiteTelegram";
        /// <summary>
        /// آدرس صفحه فیس بوک
        /// </summary>
        public const string SiteFacebook = "SiteFacebook";
        /// <summary>
        /// آدرس صفحه یوتوب
        /// </summary>
        public const string SiteYoutube = "SiteYoutube";
        /// <summary>
        /// آدرس صفحه آپارات
        /// </summary>
        public const string SiteAparat = "SiteAparat";
        /// <summary>
        /// آدرس توئیتر
        /// </summary>
        public const string SiteTwitter = "SiteTwitter";

        public const string UserVariable_Sex = "{جنسیت}";
        public const string UserVariable_FullName = "{نام}";
        public const string UserVariable_Mobile = "{تلفن همراه}";
        public const string UserVariable_Email = "{ایمیل}";
        public const string UserVariable_Password = "{رمزعبور}";

        public const string Variable_Token = "{کد فعال سازی}";
        public const string Variable_OrderNo = "{شماره سفارش}";
        public const string Variable_Coupon = "{کد تخفیف}";

    }
}
