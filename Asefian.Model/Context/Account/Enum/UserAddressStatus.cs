using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت آدرس کاربر
    /// </summary>
    [Table(name: "UserAddressStatus", Schema = "enum")]
    public class UserAddressStatus : BaseEnum<UserAddressStatus>
    {
        private UserAddressStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static UserAddressStatus Active = new UserAddressStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static UserAddressStatus Inactive = new UserAddressStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static UserAddressStatus Deleted = new UserAddressStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت های آدرس کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت های آدرس کاربر</returns>
        public new static IEnumerable<UserAddressStatus> GetList()
        {
            return new UserAddressStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
