using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Blog.Enum
{
    /// <summary>
    /// وضعیت اخبار
    /// </summary>
    [Table(name: "NewsStatus", Schema = "enum")]
    public class NewsStatus : BaseEnum<NewsStatus>
    {
        public NewsStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// پیش نویس
        /// </summary>
        public readonly static NewsStatus Draft = new NewsStatus(1, "Draft", "پیش نویس");
        /// <summary>
        /// منتشر نشده
        /// </summary>
        public readonly static NewsStatus Unpublished = new NewsStatus(2, "Unpublished", "منتشر نشده");
        /// <summary>
        /// منتشر شده
        /// </summary>
        public readonly static NewsStatus Published = new NewsStatus(3, "Published", "منتشر شده");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static NewsStatus Deleted = new NewsStatus(4, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت اخبار به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اخبار</returns>
        public new static IEnumerable<NewsStatus> GetList()
        {
            return new NewsStatus[] {
                Draft, Unpublished, Published, Deleted
            };
        }
    }
}
