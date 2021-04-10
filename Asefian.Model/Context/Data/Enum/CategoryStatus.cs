using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت دسته بندی
    /// </summary>
    [Table(name: "CategoryStatus", Schema = "enum")]
    public class CategoryStatus : BaseEnum<CategoryStatus>
    {
        public CategoryStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static CategoryStatus Active = new CategoryStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static CategoryStatus Inactive = new CategoryStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static CategoryStatus Deleted = new CategoryStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت دسته بندی به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت دسته بندی</returns>
        public new static IEnumerable<CategoryStatus> GetList()
        {
            return new CategoryStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
