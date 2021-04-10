using System;

namespace Asefian.Model.ViewModel.Account
{
    /// <summary>
    /// مدل نمایشی اعلانات
    /// </summary>
    public class NotificationViewModel
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
        /// متن پیام
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// لینک برای کلیک شدن
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// غیر قابل خوانده شدن
        /// </summary>
        public bool immortal { get; set; }
        /// <summary>
        /// ردیف نوع
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// تاریخ شمسی ایجاد
        /// </summary>
        public string PersianCreateDate { get; set; }
        /// <summary>
        /// زمان ایجاد اعلان
        /// </summary>
        public string time { get; set; }
    }

    /// <summary>
    /// مدل جستجوی دسته بندی
    /// </summary>
    public class SearchNotificationViewModel
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

}
