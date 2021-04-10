using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت مقدار فیلتر محصول
    /// </summary>
    [Table(name: "ProductFilterValueStatus", Schema = "enum")]
    public class ProductFilterValueStatus : BaseEnum<ProductFilterValueStatus>
    {
        public ProductFilterValueStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ProductFilterValueStatus Active = new ProductFilterValueStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ProductFilterValueStatus Inactive = new ProductFilterValueStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ProductFilterValueStatus Deleted = new ProductFilterValueStatus(3, "Stock", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای فیلتر محصول به صورت لیست
        /// </summary>
        /// <returns>لیست فیلتر محصول</returns>
        public new static IEnumerable<ProductFilterValueStatus> GetList()
        {
            return new ProductFilterValueStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
