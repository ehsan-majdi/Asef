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
    /// محصول
    /// </summary>
    [Table("ProductTranslation", Schema = "data")]
    public class ProductTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int ProductId { get; set; }
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
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// نقد و بررسی
        /// </summary>
        public string Review { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
