using Asefian.Model.Context.Account;
using Asefian.Model.Context.Support;
using Asefian.Model.Context.Support.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support
{
    /// <summary>
    /// پیام های تیکت
    /// </summary>
    [Table("TicketMessage", Schema = "support")]
    public class TicketMessage
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف تیکت
        /// </summary>
        public int TicketId { get; set; }
        /// <summary>
        /// متن تیکت
        /// </summary>
        [MaxLength(4000)]
        public string Message { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// فایل ضمیمه
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }
        /// <summary>
        /// ردیف نوع پاسخ
        /// </summary>
        public int TicketMessageTypeId { get; set; }

        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int CreateUserId { get; set; }
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
        /// تیکت
        /// </summary>
        public virtual Ticket Ticket { get; set; }
        /// <summary>
        /// نوع پاسخ
        /// </summary>
        public virtual TicketMessageType TicketMessageType { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
    }
}
