using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// نوع دسته بندی
    /// </summary>
    [Table(name: "CategoryType", Schema = "enum")]
    public class CategoryType : BaseEnum<CategoryType>
    {
        public CategoryType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// محصول
        /// </summary>
        public readonly static CategoryType Product = new CategoryType(1, "Product", "محصول");
        /// <summary>
        /// مقالات
        /// </summary>
        public readonly static CategoryType Article = new CategoryType(2, "Article", "مقالات");
        /// <summary>
        /// بلاگ
        /// </summary>
        public readonly static CategoryType Blog = new CategoryType(3, "Blog", "بلاگ");
        /// <summary>
        /// مرکز دانلود
        /// </summary>
        public readonly static CategoryType DownloadCenter = new CategoryType(4, "Download Center", "مرکز دانلود");

        /// <summary>
        /// دریافت تمام مقدارهای نوع دسته بندی به صورت لیست
        /// </summary>
        /// <returns>لیست نوع دسته بندی</returns>
        public new static IEnumerable<CategoryType> GetList()
        {
            return new CategoryType[] {
                Product, Article, Blog, DownloadCenter
            };
        }
    }
}
