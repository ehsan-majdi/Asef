using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت فایل در مرکز دانلود
    /// </summary>
    [Table(name: "DownloadCenterStatus", Schema = "enum")]
    public class DownloadCenterStatus : BaseEnum<DownloadCenterStatus>
    {
        public DownloadCenterStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static DownloadCenterStatus Active = new DownloadCenterStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static DownloadCenterStatus Inactive = new DownloadCenterStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static DownloadCenterStatus Deleted = new DownloadCenterStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت فایل در مرکز دانلود به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت فایل در مرکز دانلود</returns>
        public new static IEnumerable<DownloadCenterStatus> GetList()
        {
            return new DownloadCenterStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
