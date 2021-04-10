using System.Collections.Generic;
using System.Web.Mvc;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل خدمات
    /// </summary>
    public class ServiceViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServiceTranslationViewModel> translations { get; set; }
    }

    public class ServiceTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// خلاصه خدمات
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [AllowHtml]
        public string description { get; set; }
    }

    /// <summary>
    /// مدل جستجوی خدمات
    /// </summary>
    public class SearchServiceViewModel
    {
        /// <summary>
        /// شماره صفحه
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// تعداد نمایش
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// عبارت مورد جستجو
        /// </summary>
        public string word { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی خدمات
    /// </summary>
    public class ResponseSearchServiceViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
    }
}
