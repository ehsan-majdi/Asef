using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    [Table("SpecialProjectFileStatus", Schema = "enum")]
    public class SpecialProjectFileStatus : BaseEnum<SpecialProjectFileStatus>
    {
        public SpecialProjectFileStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static SpecialProjectFileStatus Active = new SpecialProjectFileStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static SpecialProjectFileStatus Inactive = new SpecialProjectFileStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static SpecialProjectFileStatus Deleted = new SpecialProjectFileStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت عکس پروژه به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت عکس پروژه</returns>
        public new static IEnumerable<SpecialProjectFileStatus> GetList()
        {
            return new SpecialProjectFileStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
