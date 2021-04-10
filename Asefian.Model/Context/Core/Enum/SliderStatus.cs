using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// وضعیت اسلایدر
    /// </summary>
    [Table(name: "SliderStatus", Schema = "enum")]
    public class SliderStatus : BaseEnum<SliderStatus>
    {
        public SliderStatus(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// فعال
        /// </summary>
        public readonly static SliderStatus Active = new SliderStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static SliderStatus Inactive = new SliderStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static SliderStatus Deleted = new SliderStatus(3, "Deleted", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت اسلایدر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اسلایدر</returns>
        public new static IEnumerable<SliderStatus> GetList()
        {
            return new SliderStatus[] {
                Active, Inactive, Deleted
            };
        }
    }
}
