using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// نوع پیام های تیکت
    /// </summary>
    [Table(name: "TicketMessageType", Schema = "enum")]
    public class TicketMessageType : BaseEnum<TicketMessageType>
    {
        public TicketMessageType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// پاسخ مشتری
        /// </summary>
        public readonly static TicketMessageType CustomerAnswer = new TicketMessageType(1, "CustomerAnswer", "پاسخ مشتری");
        /// <summary>
        /// پاسخ کارشناس
        /// </summary>
        public readonly static TicketMessageType ExpertAnswer = new TicketMessageType(2, "ExpertAnswer", "پاسخ کارشناس");
        
        /// <summary>
        /// دریافت تمام مقدارهای نوع پیام های تیکت به صورت لیست
        /// </summary>
        /// <returns>لیست نوع پیام های تیکت</returns>
        public new static IEnumerable<TicketMessageType> GetList()
        {
            return new TicketMessageType[] {
                CustomerAnswer, ExpertAnswer
            };
        }
    }
}
