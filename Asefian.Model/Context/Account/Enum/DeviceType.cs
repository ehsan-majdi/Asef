using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account.Enum
{
    /// <summary>
    /// نوع دستگاه
    /// </summary>
    [Table(name: "DeviceType", Schema = "enum")]
    public class DeviceType : BaseEnum<DeviceType>
    {
        public DeviceType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// وب
        /// </summary>
        public readonly static DeviceType Web = new DeviceType(1, "Web", "وب");
        /// <summary>
        /// موبایل
        /// </summary>
        public readonly static DeviceType Mobile = new DeviceType(2, "Mobile", "موبایل");
        /// <summary>
        /// دسکتاپ
        /// </summary>
        public readonly static DeviceType Desktop = new DeviceType(3, "Desktop", "دسکتاپ");

        /// <summary>
        /// دریافت تمام مقدارهای نوع دستگاه ها به صورت لیست
        /// </summary>
        /// <returns>لیست نوع دستگاه ها</returns>
        public new static IEnumerable<DeviceType> GetList()
        {
            return new DeviceType[] {
                Web, Mobile, Desktop
            };
        }
    }
}
