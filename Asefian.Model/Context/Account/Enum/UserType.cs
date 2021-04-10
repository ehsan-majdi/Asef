using Asefian.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// نوع کاربر
    /// </summary>
    [Table(name: "UserType", Schema = "enum")]
    public class UserType : BaseEnum<UserType>
    {
        private UserType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// داخلی
        /// </summary>
        public readonly static UserType Insider = new UserType(1, "Insider", "داخلی");
        /// <summary>
        /// خارجی
        /// </summary>
        public readonly static UserType Outsider = new UserType(2, "Outsider", "خارجی");

        /// <summary>
        /// دریافت تمام مقدارهای نوع کاربر به صورت لیست
        /// </summary>
        /// <returns>لیست نوع کاربر</returns>
        public new static IEnumerable<UserType> GetList()
        {
            return new UserType[] {
                Insider, Outsider
            };
        }
    }
}
