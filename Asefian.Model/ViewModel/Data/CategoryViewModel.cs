using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل دسته بندی
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف والد
        /// </summary>
        public int? parentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// نام دسته بندی
        /// </summary>
        public string title { get; set; }
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
        /// لیست محصولات
        /// </summary>
        public List<ProductViewModel> productList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CategoryTranslationViewModel> translations { get; set; }
    }

    public class CategoryTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int categoryId { get; set; }
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
    /// مدل جستجوی دسته بندی
    /// </summary>
    public class SearchCategoryViewModel
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
        /// ردیف دسته بندی والد
        /// </summary>
        public int? parentId { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی دسته بندی
    /// </summary>
    public class ResponseSearchCategoryViewModel
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
