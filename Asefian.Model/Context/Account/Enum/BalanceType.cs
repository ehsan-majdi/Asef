using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// نوع بالانس
    /// </summary>
    [Table(name: "BalanceType", Schema = "enum")]
    public class BalanceType : BaseEnum<BalanceType>
    {
        public BalanceType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// مثبت
        /// </summary>
        public readonly static BalanceType Positive = new BalanceType(1, "Positive", "مثبت");
        /// <summary>
        /// منفی
        /// </summary>
        public readonly static BalanceType Negative = new BalanceType(2, "Negative", "منفی");

        /// <summary>
        /// دریافت تمام مقدارهای نوع بالانس به صورت لیست
        /// </summary>
        /// <returns>لیست نوع بالانس ها</returns>
        public new static IEnumerable<BalanceType> GetList()
        {
            return new BalanceType[] {
                Positive, Negative
            };
        }
    }
}
