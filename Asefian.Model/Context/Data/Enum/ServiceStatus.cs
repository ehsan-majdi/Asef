using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت خدمات
    /// </summary>
    [Table(name: "ServiceStatus", Schema = "enum")]
    public class ServiceStatus : BaseEnum<ServiceStatus>
    {
        public ServiceStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ServiceStatus Active = new ServiceStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ServiceStatus Inactive = new ServiceStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ServiceStatus Deleted = new ServiceStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت خدمات به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت خدمات</returns>
        public new static IEnumerable<ServiceStatus> GetList()
        {
            return new ServiceStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
