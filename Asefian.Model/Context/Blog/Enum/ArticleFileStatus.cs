using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Blog.Enum
{
    /// <summary>
    /// وضعیت پست
    /// </summary>
    [Table(name: "ArticleFileStatus", Schema = "enum")]
    public class ArticleFileStatus : BaseEnum<ArticleFileStatus>
    {
        public ArticleFileStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static ArticleFileStatus Active = new ArticleFileStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ArticleFileStatus Inactive = new ArticleFileStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ArticleFileStatus Deleted = new ArticleFileStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت پست به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت پست</returns>
        public new static IEnumerable<ArticleFileStatus> GetList()
        {
            return new ArticleFileStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
