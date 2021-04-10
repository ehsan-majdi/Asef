using Asefian.Model.Context.Account;
using Asefian.Model.Context.Financial.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// سابقه فاکتور
    /// </summary>
    [Table("InvoiceLog", Schema = "financial")]
    public class InvoiceLog
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int InvoiceId { get; set; }
        /// <summary>
        /// ردیف وضعیت فاکتور
        /// </summary>
        public int StatusId { get; set; }
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
        /// فاکتور
        /// </summary>
        public virtual Invoice Invoice { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual InvoiceStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
    }
}
