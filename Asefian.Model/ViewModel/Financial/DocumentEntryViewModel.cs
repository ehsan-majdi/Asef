using System;
using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Financial
{
    /// <summary>
    /// مدل سند ورود محصولات
    /// </summary>
    public class DocumentEntryViewModel
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
        /// لیست محصولات
        /// </summary>
        public List<DocumentEntryProductViewModel> productList { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات محصول سند ورود محصولات
    /// </summary>
    public class DocumentEntryProductViewModel
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
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
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
        /// لیست مقدار ویژگی های محصول
        /// </summary>
        public List<DocumentCategoryFeatureValue> featureValueList { get; set; }
    }

    /// <summary>
    /// مدل جستجوی سند ورود محصولات
    /// </summary>
    public class SearchDocumentEntryViewModel
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
    public class ResponseSearchDocumentEntryViewModel
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
        public int? productCount { get; set; }
        /// <summary>
        /// تعداد کل محصولات در سند ورود
        /// </summary>
        public int? count { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianDate { get; set; }
    }

    /// <summary>
    /// مدل سند ورود محصول برای محصولات دارای ویژگی دسته بندی
    /// </summary>
    public class DocumentCategoryFeatureValue
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
        /// ردیف مقدار ویژگی دسته بندی
        /// </summary>
        public int categoryFeatureValueId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
    }

}
