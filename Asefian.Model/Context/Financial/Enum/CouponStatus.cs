using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// وضعیت کوپن
    /// </summary>
    [Table(name: "CouponStatus", Schema = "enum")]
    public class CouponStatus : BaseEnum<CouponStatus>
    {
        public CouponStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static CouponStatus Active = new CouponStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static CouponStatus Inactive = new CouponStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static CouponStatus Deleted = new CouponStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت کوپن به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت کوپن</returns>
        public new static IEnumerable<CouponStatus> GetList()
        {
            return new CouponStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
