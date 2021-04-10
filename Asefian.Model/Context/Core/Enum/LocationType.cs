using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core.Enum
{
    /// <summary>
    /// نوع موقعیت جغرافیایی
    /// </summary>
    [Table(name: "LocationType", Schema = "enum")]
    public class LocationType : BaseEnum<LocationType>
    {
        public LocationType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// کشور
        /// </summary>
        public readonly static LocationType Country = new LocationType(1, "Country", "کشور");
        /// <summary>
        /// استان
        /// </summary>
        public readonly static LocationType Province = new LocationType(2, "Province", "استان");
        /// <summary>
        /// شهر
        /// </summary>
        public readonly static LocationType City = new LocationType(3, "City", "شهر");

        /// <summary>
        /// دریافت تمام مقدارهای نوع موقعیت جغرافیایی به صورت لیست
        /// </summary>
        /// <returns>لیست نوع موقعیت جغرافیایی</returns>
        public new static IEnumerable<LocationType> GetList()
        {
            return new LocationType[] {
                Country, Province, City
            };
        }
    }
}
