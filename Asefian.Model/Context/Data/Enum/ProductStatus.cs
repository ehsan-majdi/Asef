using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت محصول
    /// </summary>
    [Table(name: "ProductStatus", Schema = "enum")]
    public class ProductStatus : BaseEnum<ProductStatus>
    {
        public ProductStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// موجود
        /// </summary>
        public readonly static ProductStatus Stock = new ProductStatus(1, "Stock", "موجود");
        /// <summary>
        /// ناموجود
        /// </summary>
        public readonly static ProductStatus OutOfStock = new ProductStatus(2, "Stock", "ناموجود");
        /// <summary>
        /// ناموجود
        /// </summary>
        public readonly static ProductStatus Inactive = new ProductStatus(3, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ProductStatus Deleted = new ProductStatus(4, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت محصول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت محصول</returns>
        public new static IEnumerable<ProductStatus> GetList()
        {
            return new ProductStatus[] {
                Stock, OutOfStock, Inactive, Deleted
            };
        }
    }
}
