using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data
{
    [Table("ContactUsTranslation", Schema = "data")]

    public class ContactUsTranslation
    {
        public int Id { get; set; }
        public int ContactUsId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }
        public virtual ContactUs ContactUs { get; set; }
        public virtual Language Language { get; set; }
    }
}
