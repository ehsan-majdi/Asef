namespace Asefian.Model.ViewModel.Support
{
    /// <summary>
    /// مدل دسته بندی
    /// </summary>
    public class FaqViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف دسته بندی سوالات متداول
        /// </summary>
        public int faqCategoryId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// سوال
        /// </summary>
        public string question { get; set; }
        /// <summary>
        /// پاسخ
        /// </summary>
        public string answer { get; set; }
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
    /// مدل جستجوی دسته بندی
    /// </summary>
    public class SearchFaqViewModel
    {
        /// <summary>
        /// ردیف دسته بندی سوالات متداول
        /// </summary>
        public int faqCategoryId { get; set; }
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
    /// مدل پاسخ جستجوی دسته بندی
    /// </summary>
    public class ResponseSearchFaqViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// سوال
        /// </summary>
        public string question { get; set; }
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
