using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// وضعیت استعلام
    /// </summary>
    [Table(name: "InquiryStatus", Schema = "enum")]
    public class InquiryStatus : BaseEnum<InquiryStatus>
    {
        public InquiryStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// جدید
        /// </summary>
        public readonly static InquiryStatus New = new InquiryStatus(1, "New", "جدید");
        /// <summary>
        /// درحال بررسی
        /// </summary>
        public readonly static InquiryStatus InProgress = new InquiryStatus(2, "In Progress", "درحال بررسی");
        /// <summary>
        /// پاسخ داده شده
        /// </summary>
        public readonly static InquiryStatus Answered = new InquiryStatus(3, "Answered", "پاسخ داده شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static InquiryStatus Deleted = new InquiryStatus(4, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت استعلام به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت استعلام</returns>
        public new static IEnumerable<InquiryStatus> GetList()
        {
            return new InquiryStatus[] {
                New, InProgress, Answered, Deleted
            };
        }
    }
}
