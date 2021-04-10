using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// وضعیت شعبه
    /// </summary>
    [Table(name: "BranchStatus", Schema = "enum")]
    public class BranchStatus : BaseEnum<BranchStatus>
    {
        public BranchStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static BranchStatus Active = new BranchStatus(1, "Active", "فعال");
        /// <summary>
        /// در حال تعمیر
        /// </summary>
        public readonly static BranchStatus UnderRepair = new BranchStatus(2, "UnderRepair", "در حال تعمیر");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static BranchStatus Inactive = new BranchStatus(3, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static BranchStatus Deleted = new BranchStatus(4, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت شعبه به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت شعبه</returns>
        public new static IEnumerable<BranchStatus> GetList()
        {
            return new BranchStatus[] {
                Active, UnderRepair, Inactive, Deleted
            };
        }
    }
}
