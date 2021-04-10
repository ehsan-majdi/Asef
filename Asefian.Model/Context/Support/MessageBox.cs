using Asefian.Model.Context.Account;
using Asefian.Model.Context.Support.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support
{
    /// <summary>
    /// صندوق پیام ها
    /// </summary>
    [Table("MessageBox", Schema = "support")]
    public class MessageBox
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام کامل
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [MaxLength(150)]
        public string Education { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        [MaxLength(1000)]
        public string Text { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// ردیف نوع پیام
        /// </summary>
        public int MessageTypeId { get; set; }
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
        /// وضعیت
        /// </summary>
        public virtual MessageBoxStatus Status { get; set; }
        /// <summary>
        /// وضعیت مسیج
        /// </summary>
        public virtual MessageType MessageType { get; set; }
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
