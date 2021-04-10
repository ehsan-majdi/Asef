using Asefian.Model.Context.Account;
using Asefian.Model.Context.Support.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support
{
    /// <summary>
    /// تیکت پشتیبانی
    /// </summary>
    [Table("Ticket", Schema = "support")]
    public class Ticket
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف کاربر
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// ردیف اولویت تیکت
        /// </summary>
        public int PriorityId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(250)]
        public string Title { get; set; }
        /// <summary>
        /// شماره تیکت
        /// </summary>
        public int TicketNo { get; set; }
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
        /// مشتری
        /// </summary>
        public virtual User Customer { get; set; }
        /// <summary>
        /// اولویت تیکت
        /// </summary>
        public virtual TicketPriority Priority { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual TicketStatus Status { get; set; }
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
