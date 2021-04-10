using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    [Table("SpecialProjectStatus", Schema = "enum")]
    public class SpecialProjectStatus : BaseEnum<SpecialProjectStatus>
    {
        public SpecialProjectStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static SpecialProjectStatus Active = new SpecialProjectStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static SpecialProjectStatus Inactive = new SpecialProjectStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static SpecialProjectStatus Deleted = new SpecialProjectStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت پروژه های خاص به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت  پروژه های خاص</returns>
        public new static IEnumerable<SpecialProjectStatus> GetList()
        {
            return new SpecialProjectStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
