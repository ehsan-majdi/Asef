using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت درباره ما
    /// </summary>
    [Table(name: "AboutUsStatus", Schema = "enum")]
    public class AboutUsStatus : BaseEnum<AboutUsStatus>
    {
        public AboutUsStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static AboutUsStatus Active = new AboutUsStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static AboutUsStatus Inactive = new AboutUsStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static AboutUsStatus Deleted = new AboutUsStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت درباره ما به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت درباره ما</returns>
        public new static IEnumerable<AboutUsStatus> GetList()
        {
            return new AboutUsStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
