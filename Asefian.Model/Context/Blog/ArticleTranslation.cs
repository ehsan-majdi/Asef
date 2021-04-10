using Nig.Model.Context.Blog;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Blog
{

    /// <summary>
    /// مقالات
    /// </summary>
    [Table("ArticleTranslation", Schema = "blog")]
    public class ArticleTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ArticleId { get; set; }
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
        /// خلاصه
        /// </summary>
        [MaxLength(1500)]
        public string Abstract { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// برچسب ها
        /// </summary>
        [MaxLength(1500)]
        public string Tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Article Article { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
