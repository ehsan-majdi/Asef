using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{

    /// <summary>
    /// وضعیت محصول داخل فاکتور
    /// </summary>
    [Table(name: "InvoiceDetailStatus", Schema = "enum")]
    public class InvoiceDetailStatus : BaseEnum<InvoiceDetailStatus>
    {
        public InvoiceDetailStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// تایید شده
        /// </summary>
        public readonly static InvoiceDetailStatus Accepted = new InvoiceDetailStatus(1, "Accepted", "تایید شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static InvoiceDetailStatus Deleted = new InvoiceDetailStatus(2, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت محصول داخل فاکتور به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت محصول داخل فاکتور</returns>
        public new static IEnumerable<InvoiceDetailStatus> GetList()
        {
            return new InvoiceDetailStatus[] {
                Accepted, Deleted
            };
        }
    }
}
