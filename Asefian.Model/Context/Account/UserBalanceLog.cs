using Asefian.Model.Context.Account.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// سوابق بالانس مالی کاربر
    /// </summary>
    [Table("UserBalanceLog", Schema = "account")]
    public class UserBalanceLog
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
        /// مبلغ
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ردیف نوع بالانس
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; }
        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// آی پی آخرین تغییرات
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreateIp { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// نوع بالانس
        /// </summary>
        public virtual BalanceType Type { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
    }
}
