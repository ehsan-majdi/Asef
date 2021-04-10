using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل فایل در مرکز دانلود فایل
    /// </summary>
    public class DownloadCenterViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [AllowHtml]
        public string description { get; set; }
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
    }



    /// <summary>
    /// مدل جستجوی کوپن
    /// </summary>
    public class SearchCouponViewModel
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
    /// نتیجه جستجو فایل برای رسم برای کاربر مصرف کننده
    /// </summary>
    public class DownloadCenterResultViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string categoryTitle { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianDate { get; set; }
        /// <summary>
        /// لیست فایل ها
        /// </summary>
        public List<DownloadCenterFileViewModel> fileList { get; set; }
    }

    /// <summary>
    /// کلاس فایل های مرکز دانلود
    /// </summary>
    public class DownloadCenterFileViewModel
    {
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
    }

}
