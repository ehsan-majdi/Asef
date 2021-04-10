using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial.Enum
{
    /// <summary>
    /// نوع ارسال
    /// </summary>
    [Table(name: "DeliveryType", Schema = "enum")]
    public class DeliveryType : BaseEnum<DeliveryType>
    {
        public DeliveryType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// پیک
        /// </summary>
        public readonly static DeliveryType DeliveryMan = new DeliveryType(1, "DeliveryMan", "پیک");
        /// <summary>
        /// پست
        /// </summary>
        public readonly static DeliveryType Post = new DeliveryType(2, "Post", "پست");

        /// <summary>
        /// دریافت تمام مقدارهای نوع ارسال به صورت لیست
        /// </summary>
        /// <returns>لیست نوع ارسال</returns>
        public new static IEnumerable<DeliveryType> GetList()
        {
            return new DeliveryType[] {
                DeliveryMan, Post
            };
        }
    }
}
