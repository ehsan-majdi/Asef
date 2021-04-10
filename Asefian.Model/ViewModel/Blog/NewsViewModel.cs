using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Asefian.Model.ViewModel.Blog
{
    /// <summary>
    /// مدل اخبار
    /// </summary>
    public class NewsViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int view { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime? publishDate { get; set; }
        /// <summary>
        /// تاریخ انتشار شمسی
        /// </summary>
        public string PersianPublishDate { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        public DateTime? expiredDate { get; set; }
        /// <summary>
        /// تاریخ انقضاء شمسی
        /// </summary>
        public string PersianExpiredDate { get; set; }
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
        public List<NewsTranslationViewModel> translations { get; set; }
    }

    public class NewsTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int newsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// خلاصه خبر
        /// </summary>
        public string abstractText { get; set; }
        /// <summary>
        /// متن خبر
        /// </summary>
        [AllowHtml]
        public string text { get; set; }
        /// <summary>
        /// کلمات کلیدی
        /// </summary>
        public string keywords { get; set; }
    }

    /// <summary>
    /// مدل جستجوی اخبار
    /// </summary>
    public class SearchNewsViewModel
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
    /// مدل پاسخ جستجوی اخبار
    /// </summary>
    public class ResponseSearchNewsViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string titleUrl { get; set; }
        /// <summary>
        /// خلاصه خبر
        /// </summary>
        public string abstractText { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int view { get; set; }
        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime? publishDate { get; set; }
        /// <summary>
        /// تاریخ انتشار شمسی
        /// </summary>
        public string PersianPublishDate { get; set; }
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
