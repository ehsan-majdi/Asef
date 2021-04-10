using Asefian.Model.Common;
using System.Collections.Generic;

namespace Asefian.Model.Context.Data.Enum
{
    public class ExportStatus : BaseEnum<ExportStatus>
    {
        public ExportStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }
        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ExportStatus Active = new ExportStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ExportStatus Inactive = new ExportStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ExportStatus Deleted = new ExportStatus(3, "Stock", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت صادرات به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت صادرات</returns>
        public new static IEnumerable<ExportStatus> GetList()
        {
            return new ExportStatus[] {
                Active, Inactive, Deleted
            };
        }

    }
}
