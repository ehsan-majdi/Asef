using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Blog
{
    /// <summary>
    /// مدل مقالات
    /// </summary>
    public class ArticleViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// پوستر
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// خلاصه
        /// </summary>
        public string abstractText { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// برچسب ها
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public float rate { get; set; }
        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime publishDate { get; set; }
    }
}
