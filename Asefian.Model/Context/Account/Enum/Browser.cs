using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// مرورگر
    /// </summary>
    [Table(name: "Browser", Schema = "enum")]
    public class Browser : BaseEnum<Browser>
    {
        public Browser(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// گوگل کروم
        /// </summary>
        public readonly static Browser GoogleChrome = new Browser(1, "Google Chrome", "گوگل کروم");
        /// <summary>
        /// موزیلا فایرفاکس
        /// </summary>
        public readonly static Browser MozillaFirefox = new Browser(2, "Mozilla Firefox", "موزیلا فایرفاکس");
        /// <summary>
        /// سافاری
        /// </summary>
        public readonly static Browser Safari = new Browser(3, "Safari", "سافاری");
        /// <summary>
        /// ادج
        /// </summary>
        public readonly static Browser Edge = new Browser(4, "Edge", "ادج");
        /// <summary>
        /// اینترنت اکسپلورر
        /// </summary>
        public readonly static Browser InternetExplorer = new Browser(5, "Internet Explorer", "اینترنت اکسپلورر");
        /// <summary>
        /// اوپرا
        /// </summary>
        public readonly static Browser Opera = new Browser(6, "Opera", "اوپرا");
        /// <summary>
        /// سایر
        /// </summary>
        public readonly static Browser Other = new Browser(10, "Other", "سایر");

        /// <summary>
        /// دریافت تمام مقدارهای مرورگر ها به صورت لیست
        /// </summary>
        /// <returns>لیست مرورگر ها</returns>
        public new static IEnumerable<Browser> GetList()
        {
            return new Browser[] {
                GoogleChrome, MozillaFirefox, Safari, Edge, InternetExplorer, Opera, Other
            };
        }
    }
}
