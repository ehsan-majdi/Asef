using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Data
{
    public class AddProductImageViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// نام فابل
        /// </summary>
        public string productFileName { get; set; }
        /// <summary>
        /// ردیف فایل
        /// </summary>
        public string productFileId { get; set; }
    }
}
