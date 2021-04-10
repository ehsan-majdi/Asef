using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    /// <summary>
    /// شعب
    /// </summary>
    [Table("BranchTranslation", Schema = "core")]
    public class BranchTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(100)]
        public string Title { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [MaxLength(2000)]
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Branch Branch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }

    }
}
