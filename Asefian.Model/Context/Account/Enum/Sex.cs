using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// جنیست
    /// </summary>
    [Table(name: "Sex", Schema = "enum")]
    public class Sex : BaseEnum<Sex>
    {
        private Sex(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// مرد
        /// </summary>
        public readonly static Sex Male = new Sex(1, "Male", "آقا");
        /// <summary>
        /// زن
        /// </summary>
        public readonly static Sex Female = new Sex(2, "Female", "خانم");

        /// <summary>
        /// دریافت تمام مقدارهای جنیست به صورت لیست
        /// </summary>
        /// <returns>لیست جنیست</returns>
        public new static IEnumerable<Sex> GetList()
        {
            return new Sex[] {
                Male, Female
            };
        }
    }
}
