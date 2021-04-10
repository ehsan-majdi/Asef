using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// وضعیت سوالات متداول
    /// </summary>
    [Table(name: "FaqStatus", Schema = "enum")]
    public class FaqStatus : BaseEnum<FaqStatus>
    {
        public FaqStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static FaqStatus Active = new FaqStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static FaqStatus Inactive = new FaqStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static FaqStatus Deleted = new FaqStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت سوالات متداول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت سوالات متداول</returns>
        public new static IEnumerable<FaqStatus> GetList()
        {
            return new FaqStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
