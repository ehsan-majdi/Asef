using Nig.Model.Context.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Blog
{
    public class NewsTranslation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NewsId { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(100)]
        public string Title { get; set; }
        /// <summary>
        /// خلاصه خبر
        /// </summary>
        [MaxLength(1000)]
        public string AbstractText { get; set; }
        /// <summary>
        /// متن خبر
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// کلمات کلیدی
        /// </summary>
        [MaxLength(500)]
        public string Keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual News News { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
