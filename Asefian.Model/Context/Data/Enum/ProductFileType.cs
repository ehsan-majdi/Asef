using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// نوع فایل های محصول
    /// </summary>
    [Table(name: "ProductFileType", Schema = "enum")]
    public class ProductFileType : BaseEnum<ProductFileType>
    {
        public ProductFileType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// تصویر
        /// </summary>
        public readonly static ProductFileType Picture = new ProductFileType(1, "Picture", "تصویر");
        /// <summary>
        /// پی دی اف
        /// </summary>
        public readonly static ProductFileType PDF = new ProductFileType(2, "PDF", "پی دی اف");
        /// <summary>
        /// ویدیو
        /// </summary>
        public readonly static ProductFileType Video = new ProductFileType(3, "Video", "ویدیو");
        /// <summary>
        /// صدا
        /// </summary>
        public readonly static ProductFileType Voice = new ProductFileType(4, "Voice", "صدا");
        /// <summary>
        /// مشخصات
        /// </summary>
        public readonly static ProductFileType Sheet = new ProductFileType(5, "Sheet", "مشخصات");
        /// <summary>
        /// سایر
        /// </summary>
        public readonly static ProductFileType Other = new ProductFileType(6, "Other", "سایر");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت تصاویر گالری محصول به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت تصاویر گالری محصول</returns>
        public new static IEnumerable<ProductFileType> GetList()
        {
            return new ProductFileType[] {
                Picture, PDF, Video, Voice, Sheet, Other
            };
        }
    }
}
