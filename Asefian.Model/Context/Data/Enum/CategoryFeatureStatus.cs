using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// وضعیت ویژگی دسته بندی
    /// </summary>
    [Table(name: "CategoryFeatureStatus", Schema = "enum")]
    public class CategoryFeatureStatus : BaseEnum<CategoryFeatureStatus>
    {
        public CategoryFeatureStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static CategoryFeatureStatus Active = new CategoryFeatureStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static CategoryFeatureStatus Inactive = new CategoryFeatureStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static CategoryFeatureStatus Deleted = new CategoryFeatureStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت ویژگی دسته بندی به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت ویژگی دسته بندی</returns>
        public new static IEnumerable<CategoryFeatureStatus> GetList()
        {
            return new CategoryFeatureStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
