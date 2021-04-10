using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// درباره ما
    /// </summary>
    [Table("AboutUsTranslation", Schema = "data")]
    public class AboutUsTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف درباره ما
        /// </summary>
        public int AboutUsId { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(500)]
        public string Title { get; set; }
        /// <summary>
        /// خلاصه خدمات
        /// </summary>
        [MaxLength(4000)]
        public string Summary { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// درباره ما  
        /// </summary>
        public virtual AboutUs AboutUs { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
