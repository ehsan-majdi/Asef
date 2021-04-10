using Asefian.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context
{
    /// <summary>
    /// نوع زبان
    /// </summary>
    [Table(name: "LanguageType", Schema = "enum")]
    public class Language : BaseEnum<Language>
    {
        public Language(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        public readonly static Language Persian = new Language(1, "Persian", "فارسی");

        public readonly static Language English = new Language(2, "English", "انگلیسی");

        public new static IEnumerable<Language> GetList()
        {
            return new Language[] {
                Persian, English
            };
        }
    }
}
