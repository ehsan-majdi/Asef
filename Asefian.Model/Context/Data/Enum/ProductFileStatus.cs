using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت فایل های محصول
    /// </summary>
    [Table(name: "ProductFileStatus", Schema = "enum")]
    public class ProductFileStatus : BaseEnum<ProductFileStatus>
    {
        public ProductFileStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ProductFileStatus Active = new ProductFileStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ProductFileStatus Inactive = new ProductFileStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ProductFileStatus Deleted = new ProductFileStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت تصاویر گالری محصول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت تصاویر گالری محصول</returns>
        public new static IEnumerable<ProductFileStatus> GetList()
        {
            return new ProductFileStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
