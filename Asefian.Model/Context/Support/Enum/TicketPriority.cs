using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// اولویت تیکت
    /// </summary>
    [Table(name: "TicketPriority", Schema = "enum")]
    public class TicketPriority : BaseEnum<TicketPriority>
    {
        public TicketPriority(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// معمولی
        /// </summary>
        public readonly static TicketPriority Normal = new TicketPriority(1, "Normal", "معمولی");
        /// <summary>
        /// مهم
        /// </summary>
        public readonly static TicketPriority Important = new TicketPriority(2, "Important", "مهم");
        /// <summary>
        /// خیلی مهم
        /// </summary>
        public readonly static TicketPriority VeryImportant = new TicketPriority(3, "VeryImportant", "خیلی مهم");
        
        /// <summary>
        /// دریافت تمام مقدارهای وضعیت تیکت ها به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت تیکت ها</returns>
        public new static IEnumerable<TicketPriority> GetList()
        {
            return new TicketPriority[] {
                Normal, Important, VeryImportant
            };
        }
    }
}
