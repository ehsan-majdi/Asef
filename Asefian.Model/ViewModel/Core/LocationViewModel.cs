using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Core
{
    /// <summary>
    /// مدل موقعیت جغرافیایی
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف والد
        /// </summary>
        public int? parentId { get; set; }
        /// <summary>
        /// ردیف نوع موقعیت جغرافیایی
        /// </summary>
        public int locationTypeId { get; set; }
        /// <summary>
        /// عنوان نوع موقعیت جغرافیایی
        /// </summary>
        public string locationTypeTitle { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
    }
}
