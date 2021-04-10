using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    [Table("SpecialProjectTranslation", Schema = "data")]
    public class SpecialProjectTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SpecialProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual SpecialProject SpecialProject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }

    }
}
