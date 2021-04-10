using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت علاقه مندی کاربر
    /// </summary>
    [Table(name: "UserFavoriteStatus", Schema = "enum")]
    public class UserFavoriteStatus : BaseEnum<UserFavoriteStatus>
    {
        private UserFavoriteStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static UserFavoriteStatus Active = new UserFavoriteStatus(1, "Active", "فعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static UserFavoriteStatus Deleted = new UserFavoriteStatus(2, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت علاقه مندی کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت علاقه مندی کاربر</returns>
        public new static IEnumerable<UserFavoriteStatus> GetList()
        {
            return new UserFavoriteStatus[] {
                Active, Deleted
            };
        }
    }
}
