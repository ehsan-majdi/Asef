namespace Asefian.Model.ViewModel.Support
{
    /// <summary>
    /// مدل دسته بندی
    /// </summary>
    public class NewsletterViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string email { get; set; }
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
    public class SearchNewsletterViewModel
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
