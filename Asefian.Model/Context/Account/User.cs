using Asefian.Model.Context.Account.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// کاربر
    /// </summary>
    [Table("UserProfile", Schema = "account")]
    public class User
    {
        public User()
        {
            UserFavoriteList = new List<UserFavorite>();
            UserNotificationList = new List<UserNotification>();
        }

        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; }
        /// <summary>
        /// شناسه فایل تصویر کاربر
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// نام فایل تصویر کاربر
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// شماره تلفن همراه
        /// </summary>
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        /// <summary>
        /// شماره تلفن همراه کاربر اعتبار سنجی شده است
        /// </summary>
        public bool MobileNumberValid { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }
        /// <summary>
        /// آدرس ایمیل کاربر اعتبار سنحی شده است
        /// </summary>
        public bool EmailValid { get; set; }
        /// <summary>
        /// ردیف جنسیت
        /// </summary>
        public int? SexId { get; set; }
        /// <summary>
        /// ردیف نوع کاربر
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// گذرواژه
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }
        /// <summary>
        /// تراز مالی کاربر
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// دسترسی های کاربر
        /// </summary>
        public long Permission { get; set; }
        /// <summary>
        /// اجبار کردن کاربر به تغییر رمز عبور
        /// </summary>
        public bool ForceChangePassword { get; set; }
        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// ردیف کاربر ویرایش کننده
        /// </summary>
        public int? ModifyUserId { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// آی پی ایجاد کننده
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreateIp { get; set; }
        /// <summary>
        /// آی پی آخرین تغییرات
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string ModifyIp { get; set; }
        /// <summary>
        /// نام کامل
        /// </summary>
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        /// <summary>
        /// جنسیت
        /// </summary>
        public virtual Sex Sex { get; set; }
        /// <summary>
        /// نوع کاربر
        /// </summary>
        public virtual UserType UserType { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual UserStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست محصولات مورد علاقه مندی کاربر
        /// </summary>
        public virtual List<UserFavorite> UserFavoriteList { get; set; }

        /// <summary>
        /// لیست محصولات مورد علاقه مندی کاربر
        /// </summary>
        public virtual List<UserNotification> UserNotificationList { get; set; }
    }
}
