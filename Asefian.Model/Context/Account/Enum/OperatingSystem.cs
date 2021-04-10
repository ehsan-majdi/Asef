using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// سیستم عامل
    /// </summary>
    [Table(name: "OperatingSystem", Schema = "enum")]
    public class OperatingSystem : BaseEnum<OperatingSystem>
    {
        public OperatingSystem(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// ویندوز
        /// </summary>
        public readonly static OperatingSystem Windows = new OperatingSystem(1, "Windows", "ویندوز");
        /// <summary>
        /// مک
        /// </summary>
        public readonly static OperatingSystem MacOs = new OperatingSystem(2, "MacOs", "مک");
        /// <summary>
        /// آندروید
        /// </summary>
        public readonly static OperatingSystem Android = new OperatingSystem(3, "Android", "آندروید");
        /// <summary>
        /// آی او اس
        /// </summary>
        public readonly static OperatingSystem iOs = new OperatingSystem(4, "iOs", "آی او اس");
        /// <summary>
        /// لینوکس
        /// </summary>
        public readonly static OperatingSystem Linux = new OperatingSystem(5, "Linux", "لینوکس");
        /// <summary>
        /// ویندوز فون
        /// </summary>
        public readonly static OperatingSystem WindowsPhone = new OperatingSystem(6, "WindowsPhone", "ویندوز فون");
        /// <summary>
        /// سایر
        /// </summary>
        public readonly static OperatingSystem Other = new OperatingSystem(7, "Other", "سایر");
        
        /// <summary>
        /// دریافت تمام مقدارهای سیستم عامل ها به صورت لیست
        /// </summary>
        /// <returns>لیست سیستم عامل ها</returns>
        public new static IEnumerable<OperatingSystem> GetList()
        {
            return new OperatingSystem[] {
                Windows, MacOs, Android, iOs, Linux, WindowsPhone, Other
            };
        }
    }
}
