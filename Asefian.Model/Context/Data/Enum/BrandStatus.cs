using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت برند
    /// </summary>
    [Table(name: "BrandStatus", Schema = "enum")]
    public class BrandStatus : BaseEnum<BrandStatus>
    {
        public BrandStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static BrandStatus Active = new BrandStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static BrandStatus Inactive = new BrandStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static BrandStatus Deleted = new BrandStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت برند به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت برند</returns>
        public new static IEnumerable<BrandStatus> GetList()
        {
            return new BrandStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
