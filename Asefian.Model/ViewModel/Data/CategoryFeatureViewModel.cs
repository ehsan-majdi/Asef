using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل ویژگی های دسته بندی
    /// </summary>
    public class CategoryFeatureViewModel
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
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ردیف نوع ویژگی دسته بندی
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
    }

    /// <summary>
    /// مدل جستجوی ویژگی های دسته بندی
    /// </summary>
    public class SearchCategoryFeatureViewModel
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
        /// ردیف ویژگی های دسته بندی والد
        /// </summary>
        public int? parentId { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی ویژگی های دسته بندی
    /// </summary>
    public class ResponseSearchCategoryFeatureViewModel
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
        /// ردیف نوع ویژگی دسته بندی
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// عنوان نوع ویژگی دسته بندی
        /// </summary>
        public string typeTitle { get; set; }
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
    /// مقدار های هر ویژگی تعریف شده
    /// </summary>
    public class CategoryFeatureValueViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف ویژگی
        /// </summary>
        public int categoryFeatureId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// کلاس کمکی مرتب سازی برای ویژگی دسته بندی ها
    /// </summary>
    public class RearrangeCategoryFeatureItem
    {
        /// <summary>
        /// ردیف ویژگی دسته بندی
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// لیست مقادیر درون فیلتر ها و مقدار آن ها
        /// </summary>
        public List<RearrangeCategoryFeatureValueItem> valueList { get; set; }
    }

    /// <summary>
    /// کلاس کمکی مرتب سازی برای مقادیر درون ویژگی دسته بندی ها
    /// </summary>
    public class RearrangeCategoryFeatureValueItem
    {
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// ردیف مقدار ویژگی دسته بندی
        /// </summary>
        public int id { get; set; }
    }

}
