using System.Collections.Generic;
using System.Web.Mvc;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل محصول
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف گروه محصول
        /// </summary>
        public int groupId { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// ردیف سری
        /// </summary>
        public int serieId { get; set; }
        /// <summary>
        /// ردیف ویژگی دسته بندی
        /// </summary>
        public int? categoryFeatureId { get; set; }
        /// <summary>
        /// ردیف برند
        /// </summary>
        public int brandId { get; set; }
        /// <summary>
        /// کد
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// دسته بندی
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// عرض
        /// </summary>
        public string  width { get; set; }
        /// <summary>
        /// ارتفاع
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }

        public List<ProductTranslationViewModel> translations { get; set; }

    }

    public class ProductTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string review { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [AllowHtml]
        public string description { get; set; }
    }


    /// <summary>
    /// مدل جستجوی محصول
    /// </summary>
    public class SearchProductViewModel
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
        /// 
        /// </summary>
        public int? statusId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? hasPhoto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? hasFilter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? hasDescription { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int? categoryId { get; set; }
        /// <summary>
        /// لیست برند ها
        /// </summary>
        public List<int> brand { get; set; }
        /// <summary>
        /// لیست دسته بندی ها
        /// </summary>
        public List<int> category { get; set; }
        /// <summary>
        /// لیست مقادیر فیلتر
        /// </summary>
        public List<FilterSearchViewModel> filterValue { get; set; }
    }

    /// <summary>
    /// جستجوی مقدار های فیلتر در محصولات
    /// </summary>
    public class FilterSearchViewModel
    {
        /// <summary>
        /// ردیف فیلتر
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// مقدار بولین
        /// </summary>
        public bool? valueBoolean { get; set; }
        /// <summary>
        /// مقدار های انتخاب شده لیست
        /// </summary>
        public List<int> valueList { get; set; }
    }

    /// <summary>
    /// مدل پاسخ برای نمایش محصولات در فروشگاه سایت
    /// </summary>
    public class SiteSearchProductViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// عنوان برای آدرس
        /// </summary>
        public string titleUrl { get; set; }
        /// <summary>
        /// دسته بندی
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// کد
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// طول عکس
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// ارتفاع عکس
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// شناسه فایل در تلگرام
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public float rate { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی محصول
    /// </summary>
    public class ResponseSearchProductViewModel
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
        /// ردیف فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// تعداد موجودی
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
        /// تعداد گالری 
        /// </summary>
        public int galleryCount { get; set; }
        /// <summary>
        /// تعداد فیلتر
        /// </summary>
        public int filterCount { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// نقد و بررسی
        /// </summary>
        public string review { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
    }

    /// <summary>
    /// مدل فایل های محصول
    /// </summary>
    public class ProductFileViewModel
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
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// شناسه فایل در تلگرام
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// لینک
        /// </summary>
        public string link { get; set; }
    }

    /// <summary>
    /// مدل نقد و بررسی محصول
    /// </summary>
    public class ProductReviewViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// متن نقد و بررسی محصول
        /// </summary>
        public string review { get; set; }
    }

    /// <summary>
    /// مدل محصولات برای پر شدن خودکار
    /// </summary>
    public class ProductAutoCompleteViewModel
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
        /// کد
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// تعداد موجودی
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// وضعیت محصول
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// لیست ویژگی های محصولات
        /// </summary>
        public List<ProductFeatureAutoCompleteViewModel> featureList { get; set; }
    }

    /// <summary>
    /// مدل ویژگی محصولات
    /// </summary>
    public class ProductFeatureAutoCompleteViewModel
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
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// تعداد موجودی
        /// </summary>
        public int count { get; set; }
    }
    public class ChangeProductStatusViewModel
    {
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
    }
}
