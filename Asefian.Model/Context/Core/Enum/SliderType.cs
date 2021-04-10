using Asefian.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// وضعیت نوع اسلایدر
    /// </summary>
    [Table(name: "SliderType", Schema = "enum")]
    public class SliderType : BaseEnum<SliderType>
    {
        public SliderType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// اسلایدر اصلی
        /// </summary>
        public readonly static SliderType Slider = new SliderType(1, "Slider", "اسلایدر اصلی");
        /// <summary>
        /// اسلایدر
        /// </summary>
        public readonly static SliderType BackgroundImage = new SliderType(2, "BackgroundImage", "اسلایدر");


        /// <summary>
        /// دریافت تمام مقدارهای وضعیت اسلایدر به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت اسلایدر</returns>
        public new static IEnumerable<SliderType> GetList()
        {
            return new SliderType[] {
                Slider, BackgroundImage, 
            };
        }
    }
}
