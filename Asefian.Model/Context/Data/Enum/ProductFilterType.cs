using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// نوع فیلتر محصول
    /// </summary>
    [Table(name: "ProductFilterType", Schema = "enum")]
    public class ProductFilterType : BaseEnum<ProductFilterType>
    {
        public ProductFilterType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// متن
        /// </summary>
        public readonly static ProductFilterType Text = new ProductFilterType(1, "Text", "متن");
        /// <summary>
        /// انتخابی
        /// </summary>
        public readonly static ProductFilterType Selection = new ProductFilterType(2, "Selection", "انتخابی");
        /// <summary>
        /// رنگ
        /// </summary>
        public readonly static ProductFilterType Color = new ProductFilterType(3, "Color", "رنگ");
        /// <summary>
        /// بولی
        /// </summary>
        public readonly static ProductFilterType Boolean = new ProductFilterType(4, "Boolean", "بولی");
        /// <summary>
        /// بازه
        /// </summary>
        public readonly static ProductFilterType Range = new ProductFilterType(5, "Range", "بازه");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت محصول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت محصول</returns>
        public new static IEnumerable<ProductFilterType> GetList()
        {
            return new ProductFilterType[] {
                Text, Selection, Color, Boolean, Range
            };
        }
    }
}
