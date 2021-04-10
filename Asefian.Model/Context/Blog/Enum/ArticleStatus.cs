using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Blog.Enum
{
    /// <summary>
    /// وضعیت مقالات
    /// </summary>
    [Table(name: "ArticleStatus", Schema = "enum")]
    public class ArticleStatus : BaseEnum<ArticleStatus>
    {
        public ArticleStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// پیش نویس
        /// </summary>
        public readonly static ArticleStatus Draft = new ArticleStatus(1, "Draft", "پیش نویس");
        /// <summary>
        /// منتشر نشده
        /// </summary>
        public readonly static ArticleStatus Unpublished = new ArticleStatus(2, "Unpublished", "منتشر نشده");
        /// <summary>
        /// منتشر شده
        /// </summary>
        public readonly static ArticleStatus Published = new ArticleStatus(3, "Published", "منتشر شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ArticleStatus Deleted = new ArticleStatus(4, "Deleted", "حذف شده");
        
        /// <summary>
        /// دریافت تمام مقدارهای وضعیت مقالات به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت مقالات</returns>
        public new static IEnumerable<ArticleStatus> GetList()
        {
            return new ArticleStatus[] {
                Draft, Unpublished, Published, Deleted
            };
        }
    }
}
