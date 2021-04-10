using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data
{

    /// <summary>
    /// مقدار فیلتر های محصولات
    /// </summary>
    [Table("ProductFilterValueTranslation", Schema = "data")]
    public class ProductFilterValueTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف فیلتر محصول
        /// </summary>
        public int ProductFilterValueId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// مفدار
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ProductFilterValue ProductFilterValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
