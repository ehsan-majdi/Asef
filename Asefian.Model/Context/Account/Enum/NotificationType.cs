using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// نوع اعلانات
    /// </summary>
    [Table(name: "NotificationType", Schema = "enum")]
    public class NotificationType : BaseEnum<NotificationType>
    {
        public NotificationType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// عادی
        /// </summary>
        public readonly static NotificationType Normal = new NotificationType(1, "Normal", "عادی");
        /// <summary>
        /// اطلاع رسانی
        /// </summary>
        public readonly static NotificationType Information = new NotificationType(2, "Information", "اطلاع رسانی");
        /// <summary>
        /// موفقیت آمیز
        /// </summary>
        public readonly static NotificationType Success = new NotificationType(3, "Success", "موفقیت آمیز");
        /// <summary>
        /// هشدار
        /// </summary>
        public readonly static NotificationType Warning = new NotificationType(4, "Warning", "هشدار");
        /// <summary>
        /// اخطار
        /// </summary>
        public readonly static NotificationType Alert = new NotificationType(5, "Alert", "اخطار");

        /// <summary>
        /// دریافت تمام مقدارهای نوع اعلانات به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اعلانات</returns>
        public new static IEnumerable<NotificationType> GetList()
        {
            return new NotificationType[] {
                Normal, Information, Success, Warning, Alert
            };
        }
    }
}
