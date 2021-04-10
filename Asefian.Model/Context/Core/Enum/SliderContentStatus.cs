using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// وضعیت محتوی اسلایدر
    /// </summary>
    [Table(name: "SliderContentStatus", Schema = "enum")]
    public class SliderContentStatus : BaseEnum<SliderContentStatus>
    {
        public SliderContentStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static SliderContentStatus Active = new SliderContentStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static SliderContentStatus Inactive = new SliderContentStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static SliderContentStatus Deleted = new SliderContentStatus(3, "Deleted", "حذف شده");


        /// <summary>
        /// دریافت تمام مقدارهای وضعیت محتوی اسلایدر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اسلایدر</returns>
        public new static IEnumerable<SliderContentStatus> GetList()
        {
            return new SliderContentStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
