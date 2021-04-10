using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// نوع پرداخت
    /// </summary>
    [Table(name: "PaymentType", Schema = "enum")]
    public class PaymentType : BaseEnum<PaymentType>
    {
        public PaymentType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// نامشخص
        /// </summary>
        public readonly static PaymentType Unknown = new PaymentType(1, "Unknown", "نامشخص");
        /// <summary>
        /// آنلاین
        /// </summary>
        public readonly static PaymentType Online = new PaymentType(2, "Online", "آنلاین");
        /// <summary>
        /// نقدی
        /// </summary>
        public readonly static PaymentType Cash = new PaymentType(3, "Cash", "نقدی");

        /// <summary>
        /// دریافت تمام مقدارهای نوع پرداخت به صورت لیست
        /// </summary>
        /// <returns>لیست نوع پرداخت</returns>
        public new static IEnumerable<PaymentType> GetList()
        {
            return new PaymentType[] {
                Unknown, Online, Cash
            };
        }
    }
}
