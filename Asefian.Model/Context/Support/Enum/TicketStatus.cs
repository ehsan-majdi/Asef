using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// وضعیت تیکت ها
    /// </summary>
    [Table(name: "TicketStatus", Schema = "enum")]
    public class TicketStatus : BaseEnum<TicketStatus>
    {
        public TicketStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// جدید
        /// </summary>
        public readonly static TicketStatus New = new TicketStatus(1, "New", "جدید");
        /// <summary>
        /// باز
        /// </summary>
        public readonly static TicketStatus Open = new TicketStatus(2, "Open", "باز");
        /// <summary>
        /// نگه داشته شده
        /// </summary>
        public readonly static TicketStatus Hold = new TicketStatus(3, "Hold", "نگه داشته شده");
        /// <summary>
        /// بسته شده
        /// </summary>
        public readonly static TicketStatus Closed = new TicketStatus(4, "Closed", "بسته شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static TicketStatus Deleted = new TicketStatus(5, "Deleted", "حذف شده");
        
        /// <summary>
        /// دریافت تمام مقدارهای وضعیت تیکت ها به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت تیکت ها</returns>
        public new static IEnumerable<TicketStatus> GetList()
        {
            return new TicketStatus[] {
                New, Open, Hold, Closed, Deleted
            };
        }
    }
}
