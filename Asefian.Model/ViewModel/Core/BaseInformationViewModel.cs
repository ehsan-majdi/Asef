using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Core
{
    /// <summary>
    /// اطلاعات پایه
    /// </summary>
    public class BaseInformationViewModel
    {
        /// <summary>
        /// کلید
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// لیست زبان جهت ذخیره سازی برای بخش تماس با ما
        /// </summary>
        public List<BaseInformationTranslationViewModel> translations { get; set; }
    }

    public class BaseInformationTranslationViewModel
    {
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int? languageId { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// مدل تعرفه های ثبت شده
    /// </summary>
    public class TariffViewModel
    {
        /// <summary>
        /// فاصله زمانی روز که کاربر می بایست حداقل صبر کند تا سفارش خود را تحویل بگیرد
        /// </summary>
        public int orderGapDay { get; set; }
        /// <summary>
        /// حداقل مبلغ سفارش برای ارسال رایگان
        /// </summary>
        public decimal minDeliveryFreePrice { get; set; }
        /// <summary>
        /// هزینه ارسال
        /// </summary>
        public decimal deliveryPrice { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات تماس سایت
    /// </summary>
    public class SiteContactViewModel
    {
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// لیست زبان جهت ذخیره سازی برای بخش تماس با ما
        /// </summary>
        public List<SaveContactTranslationViewModel> translations { get; set; }
    }

    public class SaveContactTranslationViewModel
    {

        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int? languageId { get; set; }

        /// <summary>
        /// مقدار
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات شبکه های اجتماعی سایت
    /// </summary>
    public class SiteSocialNetworkViewModel
    {
        /// <summary>
        /// آدرس اینستاگرام
        /// </summary>
        public string instagram { get; set; }
        /// <summary>
        /// آدرس تلگرام
        /// </summary>
        public string telegram { get; set; }
        /// <summary>
        /// آدرس فیس بوک
        /// </summary>
        public string facebook { get; set; }
        /// <summary>
        /// آدرس یوتوب
        /// </summary>
        public string youtube { get; set; }
        /// <summary>
        /// آدرس آپارات
        /// </summary>
        public string aparat { get; set; }
        /// <summary>
        /// آدرس توئیتر
        /// </summary>
        public string twitter { get; set; }
    }

}
