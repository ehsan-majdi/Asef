using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// وضعیت پیام های داخل صندوق پیام
    /// </summary>
    [Table(name: "MessageBoxStatus", Schema = "enum")]
    public class MessageBoxStatus : BaseEnum<MessageBoxStatus>
    {
        private MessageBoxStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// جدید
        /// </summary>
        public readonly static MessageBoxStatus New = new MessageBoxStatus(1, "New", "جدید");
        /// <summary>
        /// خوانده شده
        /// </summary>
        public readonly static MessageBoxStatus Read = new MessageBoxStatus(2, "Read", "خوانده شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static MessageBoxStatus Deleted = new MessageBoxStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت پیام های داخل صندوق پیام به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت پیام های داخل صندوق پیام</returns>
        public new static IEnumerable<MessageBoxStatus> GetList()
        {
            return new MessageBoxStatus[] {
                New, Read, Deleted
            };
        }
    }
}
