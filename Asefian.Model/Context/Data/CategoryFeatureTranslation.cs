using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// ویژگی های تعریف شده برای هر دسته بندی که به محصولات متصل شود
    /// </summary>
    [Table("CategoryFeatureTranslation", Schema = "data")]
    public class CategoryFeatureTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف ویژگی های تعریف شده برای هر دسته بندی که به محصولات متصل شود
        /// </summary>
        public int CategoryFeatureId { get; set; }
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
        /// ویژگی های تعریف شده برای هر دسته بندی که به محصولات متصل شود
        /// </summary>
        public virtual CategoryFeature CategoryFeature { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }

    }
}
