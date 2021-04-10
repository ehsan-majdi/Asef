using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data
{
    [Table("SpecialProjectFileTranslation", Schema = "data")]
    public class SpecialProjectFileTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SpecialProjectFileId { get; set; }
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
        public virtual SpecialProjectFile SpecialProjectFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
