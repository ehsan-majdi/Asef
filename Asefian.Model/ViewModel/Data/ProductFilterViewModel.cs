using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل فیلتر نمایش محصولات
    /// </summary>
    public class ProductFilterViewModel
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
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// ردیف نوع فیلتر
        /// </summary>
        public int filterTypeId { get; set; }
        /// <summary>
        /// عنوان نوع فیلتر
        /// </summary>
        public string filterTypeTitle { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// لیست مقادیر ثبت شده فیلتر ها
        /// </summary>
        public List<ProductFilterValueViewModel> valueList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProductFilterTranslationViewModel> translations { get; set; }

    }

    public class ProductFilterTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int productFilterId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// مدل جستجوی فیلتر محصول
    /// </summary>
    public class SearchProductFilterViewModel
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
    /// مدل پاسخ جستجوی فیلتر محصول
    /// </summary>
    public class ResponseSearchProductFilterViewModel
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
        /// ردیف دسته بندی
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string categoryTitle { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// ردیف نوع فیلتر
        /// </summary>
        public int filterTypeId { get; set; }
        /// <summary>
        /// عنوان نوع فیلتر
        /// </summary>
        public string filterTypeTitle { get; set; }
    }

    /// <summary>
    /// لیست مقادیر فیلتر محصول
    /// </summary>
    public class ProductFilterValueViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف فیلتر محصول
        /// </summary>
        public int productFilterId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// مفدار
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }

        public List<ProductFilterValueTranslationViewModel> translations { get; set; }
    }

    public class ProductFilterValueTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// کلاس کمکی مرتب سازی برای فیلتر ها
    /// </summary>
    public class RearrangeFilterItem
    {
        /// <summary>
        /// ردیف فیلتر
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// لیست مقادیر درون فیلتر ها و مقدار آن ها
        /// </summary>
        public List<RearrangeFilterValueItem> valueList { get; set; }
    }

    /// <summary>
    /// کلاس کمکی مرتب سازی برای مقادیر درون فیلتر ها
    /// </summary>
    public class RearrangeFilterValueItem
    {
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// ردیف مقدار فیلتر محصول
        /// </summary>
        public int id { get; set; }
    }

    /// <summary>
    /// مدل کمکی برای مقدار ثبت شده برای فیلتر ها
    /// </summary>
    public class ProductFilterDataViewModel
    {
        /// <summary>
        /// ردیف فیلتر داده ها
        /// </summary>
        public int productFilterId { get; set; }
        /// <summary>
        /// لیست مقدار های ثبت شده برای محصول
        /// </summary>
        public List<ProductFilterDataValueViewModel> valueList { get; set; }
    }

    /// <summary>
    /// کلاس مقدار ثبت شده برای هر محصول که هر فیلتر چه مقداری برای آن ثبت شده است.
    /// </summary>
    public class ProductFilterDataValueViewModel
    {
        public int? productFilterValueId { get; set; }
        public string value { get; set; }
        public bool? valueBoolean { get; set; }

    }
}
