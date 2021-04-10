using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// وضعیت ایمیل خبرنامه
    /// </summary>
    [Table(name: "NewsLetterStatus", Schema = "enum")]
    public class NewsLetterStatus : BaseEnum<NewsLetterStatus>
    {
        public NewsLetterStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static NewsLetterStatus Active = new NewsLetterStatus(1, "Active", "فعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static NewsLetterStatus Deleted = new NewsLetterStatus(2, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت ایمیل خبرنامه به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت ایمیل خبرنامه</returns>
        public new static IEnumerable<NewsLetterStatus> GetList()
        {
            return new NewsLetterStatus[] {
                Active, Deleted
            };
        }
    }
}
