using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت اعلانات کاربر
    /// </summary>
    [Table(name: "NotificationStatus", Schema = "enum")]
    public class NotificationStatus : BaseEnum<NotificationStatus>
    {
        public NotificationStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// جدید
        /// </summary>
        public readonly static NotificationStatus New = new NotificationStatus(1, "New", "جدید");
        /// <summary>
        /// تحویل شده
        /// </summary>
        public readonly static NotificationStatus Delivered = new NotificationStatus(2, "Delivered", "تحویل شده");
        /// <summary>
        /// دیده شده
        /// </summary>
        public readonly static NotificationStatus Seen = new NotificationStatus(3, "Seen", "دیده شده");
        /// <summary>
        /// خوانده نشده
        /// </summary>
        public readonly static NotificationStatus Unread = new NotificationStatus(4, "Unread", "خوانده نشده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static NotificationStatus Deleted = new NotificationStatus(5, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت اعلانات کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اعلانات کاربر</returns>
        public new static IEnumerable<NotificationStatus> GetList()
        {
            return new NotificationStatus[] {
                New, Delivered, Seen, Unread, Deleted
            };
        }
    }
}
