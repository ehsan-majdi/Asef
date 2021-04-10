using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data
{
    [Table("ExportTranslation", Schema = "data")]
    public class ExportTranslation
    {
        public int Id { get; set; }
        public int ExportId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }
        public virtual Export Export { get; set; }
        public virtual Language Language { get; set; }
    }
}
