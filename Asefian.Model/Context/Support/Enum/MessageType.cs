using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// نوع پیام های تیکت
    /// </summary>
    [Table(name: "MessageType", Schema = "enum")]
    public class MessageType : BaseEnum<MessageType>
    {
        public MessageType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {

        }
        /// <summary>
        /// طراح
        /// </summary>
        public readonly static MessageType Designer = new MessageType(1, "Designer", "طراح");
        /// <summary>
        /// همکاری با ما
        /// </summary>
        public readonly static MessageType WorkWithUs = new MessageType(2, "WorkWithUs", "همکاری با ما");
        /// <summary>
        /// تماس با ما
        /// </summary>
        public readonly static MessageType ContactUs = new MessageType(3, "ContactUs", "تماس با ما");

        /// <summary>
        /// دریافت تمام مقدارهای نوع پیام های تیکت به صورت لیست
        /// </summary>
        /// <returns>لیست نوع پیام های تیکت</returns>
        public new static IEnumerable<MessageType> GetList()
        {
            return new MessageType[] {
                Designer, WorkWithUs , ContactUs
            };
        }
    }
}
