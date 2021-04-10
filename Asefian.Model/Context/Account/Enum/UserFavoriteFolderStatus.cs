using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// وضعیت پوشه علاقه مندی کاربر
    /// </summary>
    [Table(name: "UserFavoriteFolderStatus", Schema = "enum")]
    public class UserFavoriteFolderStatus : BaseEnum<UserFavoriteFolderStatus>
    {
        private UserFavoriteFolderStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static UserFavoriteFolderStatus Active = new UserFavoriteFolderStatus(1, "Active", "فعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static UserFavoriteFolderStatus Deleted = new UserFavoriteFolderStatus(2, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت پوشه علاقه مندی کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت پوشه علاقه مندی کاربر</returns>
        public new static IEnumerable<UserFavoriteFolderStatus> GetList()
        {
            return new UserFavoriteFolderStatus[] {
                Active, Deleted
            };
        }
    }
}
