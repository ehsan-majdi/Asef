using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت فیلتر محصول
    /// </summary>
    [Table(name: "ProductFilterStatus", Schema = "enum")]
    public class ProductFilterStatus : BaseEnum<ProductFilterStatus>
    {
        public ProductFilterStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ProductFilterStatus Active = new ProductFilterStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ProductFilterStatus Inactive = new ProductFilterStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ProductFilterStatus Deleted = new ProductFilterStatus(3, "Stock", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت محصول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت محصول</returns>
        public new static IEnumerable<ProductFilterStatus> GetList()
        {
            return new ProductFilterStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
