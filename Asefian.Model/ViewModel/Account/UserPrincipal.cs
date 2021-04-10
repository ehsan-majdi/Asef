namespace Asefian.Model.ViewModel.Account
{
    /// <summary>
    /// مدل کاربر اصلی
    /// </summary>
    public class UserPrincipal
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// آدرس تصویر کاربر
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// شماره تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// شماره تلفن همراه کاربر اعتبار سنجی شده است
        /// </summary>
        public bool mobileNumberValid { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// آدرس ایمیل کاربر اعتبار سنحی شده است
        /// </summary>
        public bool emailValid { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// دسترسی به داشبورد مدیریت
        /// </summary>
        public bool dashboard { get; set; }
        /// <summary>
        /// دسترسی های کاربر
        /// </summary>
        public string token { get; set; }
    }
}
