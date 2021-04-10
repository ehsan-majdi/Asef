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
    /// فیلتر های محصولات
    /// </summary>
    [Table("ProductFilterTranslation", Schema = "data")]
    public class ProductFilterTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int ProductFilterId { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(1500)]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ProductFilter ProductFilter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual  Language Language { get; set; }
    }
}
