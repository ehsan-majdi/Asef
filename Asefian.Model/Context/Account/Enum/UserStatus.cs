using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت کاربر
    /// </summary>
    [Table(name: "UserStatus", Schema = "enum")]
    public class UserStatus : BaseEnum<UserStatus>
    {
        private UserStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static UserStatus Active = new UserStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static UserStatus Inactive = new UserStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// مسدود شده
        /// </summary>
        public readonly static UserStatus Suspend = new UserStatus(3, "Suspend", "مسدود شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static UserStatus Deleted = new UserStatus(4, "Deleted", "حذف شده");
        
        /// <summary>
        /// دریافت تمام مقدارهای وضعیت کاربران به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت کاربران</returns>
        public new static IEnumerable<UserStatus> GetList()
        {
            return new UserStatus[] {
                Active, Inactive, Suspend, Deleted
            };
        }
    }
}
