using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support.Enum
{
    /// <summary>
    /// وضعیت دسته بندی سوالات متداول
    /// </summary>
    [Table(name: "FaqCategoryStatus", Schema = "enum")]
    public class FaqCategoryStatus : BaseEnum<FaqCategoryStatus>
    {
        public FaqCategoryStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static FaqCategoryStatus Active = new FaqCategoryStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static FaqCategoryStatus Inactive = new FaqCategoryStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static FaqCategoryStatus Deleted = new FaqCategoryStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت دسته بندی سوالات متداول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت دسته بندی سوالات متداول</returns>
        public new static IEnumerable<FaqCategoryStatus> GetList()
        {
            return new FaqCategoryStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
