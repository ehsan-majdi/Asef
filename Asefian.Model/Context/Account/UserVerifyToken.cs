using Asefian.Model.Context.Account.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// نشان توکن برای احراز هویت کاربر
    /// </summary>
    [Table("UserVerifyToken", Schema = "account")]
    public class UserVerifyToken
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
        /// ردیف نوع نشان توکن
        /// </summary>
        public int TokenTypeId { get; set; }
        /// <summary>
        /// متن توکن
        /// </summary>
        [MaxLength(150)]
        [Index(IsUnique = true)]
        public string Token { get; set; }
        /// <summary>
        /// توکن مصرف شده
        /// </summary>
        public bool Used { get; set; }
        /// <summary>
        /// توکن منقضی شده
        /// </summary>
        public bool Expired { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخ مصرف
        /// </summary>
        public DateTime? UsedDate { get; set; }
        /// <summary>
        /// آی پی درخواست کننده
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreateIp { get; set; }
        /// <summary>
        /// آی پی مصرف کننده
        /// </summary>
        [MaxLength(48)]
        public string UsedIp { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// نوع نشان توکن
        /// </summary>
        public virtual TokenType TokenType { get; set; }
    }
}
