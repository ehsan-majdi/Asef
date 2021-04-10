using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Core
{
    /// <summary>
    /// مدل اسلایدر
    /// </summary>
    public class SliderViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// لینک
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// ردیف نوع اسلایدر
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SliderTranslationViewModel> translations { get; set; }
    }

    public class SliderTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int sliderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
    }
}
