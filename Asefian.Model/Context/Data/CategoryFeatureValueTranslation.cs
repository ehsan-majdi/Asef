using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// مقدار های هر ویژگی تعریف شده
    /// </summary>
    [Table("CategoryFeatureValueTranslation", Schema = "data")]
    public class CategoryFeatureValueTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف ویژگی
        /// </summary>
        public int CategoryFeatureValueId { get; set; }
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
        /// مقدار های هر ویژگی تعریف شده
        /// </summary>
        public virtual CategoryFeatureValue CategoryFeatureValue { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
