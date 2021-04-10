using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// وضعیت فاکتور
    /// </summary>
    [Table(name: "InvoiceStatus", Schema = "enum")]
    public class InvoiceStatus : BaseEnum<InvoiceStatus>
    {
        public InvoiceStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// ثبت شده
        /// </summary>
        public readonly static InvoiceStatus Registered = new InvoiceStatus(1, "Registered", "ثبت شده");
        /// <summary>
        /// در صف بررسی
        /// </summary>
        public readonly static InvoiceStatus InProgress = new InvoiceStatus(2, "In Progress", "در صف بررسی");
        /// <summary>
        /// فاکتور شده
        /// </summary>
        public readonly static InvoiceStatus Factored = new InvoiceStatus(3, "Factored", "فاکتور شده");
        /// <summary>
        /// در حال آماده سازی
        /// </summary>
        public readonly static InvoiceStatus Preparing = new InvoiceStatus(4, "Preparing", "در حال آماده سازی");
        /// <summary>
        /// آماده ارسال
        /// </summary>
        public readonly static InvoiceStatus Packaged = new InvoiceStatus(5, "Packaged", "آماده ارسال");
        /// <summary>
        /// ارسال شده
        /// </summary>
        public readonly static InvoiceStatus Sent = new InvoiceStatus(6, "Sent", "ارسال شده");
        /// <summary>
        /// تحویل شده
        /// </summary>
        public readonly static InvoiceStatus Delivered = new InvoiceStatus(7, "Delivered", "تحویل شده");
        /// <summary>
        /// لغو شده
        /// </summary>
        public readonly static InvoiceStatus Cancel = new InvoiceStatus(8, "Cancel", "لغو شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static InvoiceStatus Deleted = new InvoiceStatus(9, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت فاکتور به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت فاکتور</returns>
        public new static IEnumerable<InvoiceStatus> GetList()
        {
            return new InvoiceStatus[] {
                Registered, InProgress, Factored, Preparing, Packaged, Sent, Delivered, Cancel, Deleted
            };
        }
    }
}
