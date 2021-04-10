using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    [Table("SliderTranslation", Schema = "core")]
    public class SliderTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف اسلایدر
        /// </summary>
        public int SliderId { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        [MaxLength(1500)]
        public string Text { get; set; }
        /// <summary>
        /// اسلایدر
        /// </summary>
        public virtual Slider Slider { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }


    }
}
