namespace Asefian.Model.ViewModel.Account
{
    /// <summary>
    /// مدل کاربر
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string lastName { get; set; }
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
        /// گذرواژه
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// تکرار گذرواژه
        /// </summary>
        public string confirmPassword { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int? statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// ردیف جنسیت
        /// </summary>
        public int? sex { get; set; }
    }

    /// <summary>
    /// مدل جستجوی کاربر
    /// </summary>
    public class SearchUserViewModel
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
        /// ردیف نوع کاربر
        /// </summary>
        public int userTypeId { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی کاربر
    /// </summary>
    public class ResponseSearchUserViewModel
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
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
    }

    /// <summary>
    /// مدل ورود کاربر به سیستم
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// گذرواژه
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// سیستم عامل
        /// </summary>
        public int? operatingSystem { get; set; }
        /// <summary>
        /// مرورگر
        /// </summary>
        public int? browser { get; set; }
        /// <summary>
        /// ابعاد صفحه نمایش
        /// </summary>
        public string screenSize { get; set; }
        /// <summary>
        /// نوع دستگاه ارسال کننده درخواست
        /// </summary>
        public int? device { get; set; }
        /// <summary>
        /// ورژن درخواست کننده لاگین
        /// </summary>
        public string version { get; set; }
    }

    /// <summary>
    /// مدل نتیجه درخواست ورود کاربر
    /// </summary>
    public class LoginResponseViewModel
    {
        /// <summary>
        /// آدرسی که بعد از لاگین بایست کاربر هدایت شود
        /// </summary>
        public string redirect { get; set; }
    }

    /// <summary>
    /// مدل برای ثبت نام کاربر
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// نام
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// شماره تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// گذرواژه
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// تکرار گذرواژه
        /// </summary>
        public string confirmPassword { get; set; }
        /// <summary>
        /// ردیف جنسیت
        /// </summary>
        public int? sex { get; set; }
    }

    /// <summary>
    /// مدل آدرس کاربر
    /// </summary>
    public class UserAddressViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// نام کامل دریافت کننده بسته
        /// </summary>
        public string receiverFullName { get; set; }
        /// <summary>
        /// تلفن همراه دریافت کننده بسته
        /// </summary>
        public string receiverMobileNumber { get; set; }
        /// <summary>
        /// تلفن محل دریافت بسته
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// ردیف استان
        /// </summary>
        public int provinceId { get; set; }
        /// <summary>
        /// ردیف شهر
        /// </summary>
        public int cityId { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string postalCode { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// آدرس پیشفرض
        /// </summary>
        public bool main { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات نقشه برای کاربر
    /// </summary>
    public class UserAddressMapViewModel
    {
        public int id { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public string longitude { get; set; }
    }

    /// <summary>
    /// مدل علاقه مندی های کاربر
    /// </summary>
    public class UserFavouriteFolderViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف فولدر پدر
        /// </summary>
        public int? parentId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// مدل علاقه مندی محصول
    /// </summary>
    public class UserFavouriteProductViewModel
    {
        /// <summary>
        /// ردیف پوشه
        /// </summary>
        public int? folderId { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
    }

    /// <summary>
    /// لیست علاقه مندی های کاربر
    /// </summary>
    public class UserFavouriteVideModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// نوع
        /// 1. پوشه
        /// 2. محصول
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// مدل تغییر رمزعبور کاربر
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// رمز عبور جدید
        /// </summary>
        public string newPassword { get; set; }
        /// <summary>
        /// تکرار رمز عبور
        /// </summary>
        public string confirmNewPassword { get; set; }
    }

    /// <summary>
    /// مدل ریست کردن رمزعبور کاربر
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// ردیف کاربر
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// رمز عبور جدید
        /// </summary>
        public string newPassword { get; set; }
        /// <summary>
        /// تکرار رمز عبور
        /// </summary>
        public string confirmNewPassword { get; set; }
    }

    /// <summary>
    /// مدل اعتبار سنجی کاربر
    /// </summary>
    public class ValidateViewModel
    {
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// کد تایید
        /// </summary>
        public string code { get; set; }
    }
}
