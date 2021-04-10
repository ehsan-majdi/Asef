using Asefian.Model.Context.Account.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// اعلانات
    /// </summary>
    [Table("Notification", Schema = "account")]
    public class Notification
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف کاربر
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        [MaxLength(2048)]
        public string Message { get; set; }
        /// <summary>
        /// لینک برای کلیک شدن
        /// </summary>
        [MaxLength(1024)]
        public string Link { get; set; }
        /// <summary>
        /// همیشگی، یعنی با دیده شدن، خوانده نشه
        /// از این کلید برای مواردی استفاده می شود که کاربر با بازکردن اعلان از لیست اعلان های وی جدید بودنش حذف نشود
        /// </summary>
        public bool Immortal { get; set; }
        /// <summary>
        /// کلید همیشگی بودن
        /// </summary>
        [MaxLength(50)]
        public string ImmortalKey { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// ردیف نوع
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// ردیف کاربر ویرایش کننده
        /// </summary>
        public int ModifyUserId { get; set; }
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
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// نوع
        /// </summary>
        public virtual NotificationType Type { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual NotificationStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
    }
}
