using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت گروه
    /// </summary>
    [Table(name: "GroupStatus", Schema = "enum")]
    public class GroupStatus : BaseEnum<GroupStatus>
    {
        private GroupStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static GroupStatus Active = new GroupStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static GroupStatus Inactive = new GroupStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static GroupStatus Deleted = new GroupStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت گروه به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت گروه</returns>
        public new static IEnumerable<GroupStatus> GetList()
        {
            return new GroupStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
