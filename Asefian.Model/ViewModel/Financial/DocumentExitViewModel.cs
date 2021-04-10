using System;
using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Financial
{
    /// <summary>
    /// مدل سند ورود محصولات
    /// </summary>
    public class DocumentExitViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره سند ورود
        /// </summary>
        public string no { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// لیست محصولات
        /// </summary>
        public List<DocumentExitProductViewModel> productList { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات محصول سند ورود محصولات
    /// </summary>
    public class DocumentExitProductViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// کد محصول
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// عنوان محصول
        /// </summary>
        public string productTitle { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// لیست سند ورود محصول برای محصولات دارای ویژگی دسته بندی
        /// </summary>
        public List<DocumentCategoryFeatureValue> featureValueList { get; set; }
    }

    /// <summary>
    /// مدل جستجوی سند ورود محصولات
    /// </summary>
    public class SearchDocumentExitViewModel
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
    /// مدل نتیجه جستجوی سند ورود محصولات
    /// مدل نتیجه جستجوی سند ورود محصولات
    /// </summary>
    public class ResponseSearchDocumentExitViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره سند ورود
        /// </summary>
        public string no { get; set; }
        /// <summary>
        /// تعداد محصولات در سند ورود
        /// </summary>
        public int productCount { get; set; }
        /// <summary>
        /// تعداد کل محصولات در سند ورود
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianDate { get; set; }
    }

}
