using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Enum
{
    /// <summary>
    /// وضعیت اعلانات کاربر
    /// </summary>
    [Table(name: "UserNotificationStatus", Schema = "enum")]
    public class UserNotificationStatus : BaseEnum<UserNotificationStatus>
    {
        public UserNotificationStatus(int id, string title, string persianTitle) : base(id, title, persianTitle)
        {
        }

        /// <summary>
        /// جدید
        /// </summary>
        public readonly static UserNotificationStatus New = new UserNotificationStatus(1, "New", "جدید");
        /// <summary>
        /// تحویل شده
        /// </summary>
        public readonly static UserNotificationStatus Delivered = new UserNotificationStatus(2, "Delivered", "تحویل شده");
        /// <summary>
        /// دیده شده
        /// </summary>
        public readonly static UserNotificationStatus Seen = new UserNotificationStatus(3, "Seen", "دیده شده");
        /// <summary>
        /// خوانده نشده
        /// </summary>
        public readonly static UserNotificationStatus Unread = new UserNotificationStatus(4, "Unread", "خوانده نشده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static UserNotificationStatus Deleted = new UserNotificationStatus(5, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت اعلانات کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اعلانات کاربر</returns>
        public new static IEnumerable<UserNotificationStatus> GetList()
        {
            return new UserNotificationStatus[] {
                New, Delivered, Seen, Unread, Deleted
            };
        }
    }
}
