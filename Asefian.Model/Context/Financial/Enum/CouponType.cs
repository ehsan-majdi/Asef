using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// نوع کوپن
    /// </summary>
    [Table(name: "CouponType", Schema = "enum")]
    public class CouponType : BaseEnum<CouponType>
    {
        public CouponType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// مبلغ
        /// </summary>
        public readonly static CouponType Amount = new CouponType(1, "Amount", "مبلغ");
        /// <summary>
        /// درصد
        /// </summary>
        public readonly static CouponType Percentage = new CouponType(2, "Percentage", "درصد");

        /// <summary>
        /// دریافت تمام مقدارهای نوع کوپن به صورت لیست
        /// </summary>
        /// <returns>لیست نوع کوپن</returns>
        public new static IEnumerable<CouponType> GetList()
        {
            return new CouponType[] {
                Amount, Percentage
            };
        }
    }
}
