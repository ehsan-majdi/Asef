using System;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل کوپن
    /// </summary>
    public class CouponViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// کد کوپن
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// ردیف نوع کوپن
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// عنوان نوع کوپن
        /// </summary>
        public string typeTitle { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public decimal value { get; set; }
        /// <summary>
        /// تعداد استفاده شده
        /// </summary>
        public int usedCount { get; set; }
        /// <summary>
        /// تعداد قابل استفاده
        /// </summary>
        public int? usableCount { get; set; }
        /// <summary>
        /// ردیف کاربر برای استفاده از کوپن
        /// </summary>
        public int? userId { get; set; }
        /// <summary>
        /// از تاریخ
        /// </summary>
        public DateTime? fromDate { get; set; }
        /// <summary>
        /// از تاریخ شمسی
        /// </summary>
        public string fromDatePersian { get; set; }
        /// <summary>
        /// تا تاریخ
        /// </summary>
        public DateTime? toDate { get; set; }
        /// <summary>
        /// تا تاریخ شمسی
        /// </summary>
        public string toDatePersian { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }

        /// <summary>
        /// پیشوند برای کد تخفیف
        /// </summary>
        public string prefix { get; set; }
        /// <summary>
        /// تعداد کوپن تخفیف برای ساختن
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// تعداد کاراکترهای کد تخفیف
        /// </summary>
        public int codeCount { get; set; }
    }

    /// <summary>
    /// مدل جستجوی فایل
    /// </summary>
    public class SearchDownloadCenterViewModel
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
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int? categoryId { get; set; }
    }

}
