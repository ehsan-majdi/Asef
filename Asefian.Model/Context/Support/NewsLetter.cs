using Asefian.Model.Context.Account;
using Asefian.Model.Context.Core.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support
{
    /// <summary>
    /// خبرنامه
    /// </summary>
    [Table("NewsLetter", Schema = "support")]
    public class NewsLetter
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام کامل
        /// </summary>
        [MaxLength(100)]
        public string FullName { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// آی پی ایجاد کننده
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreateIp { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual NewsLetterStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
    }
}
