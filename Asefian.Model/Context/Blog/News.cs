using Asefian.Model.Context.Account;
using Asefian.Model.Context.Blog;
using Asefian.Model.Context.Blog.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nig.Model.Context.Blog
{
    /// <summary>
    /// اخبار
    /// </summary>
    [Table("News", Schema = "blog")]
    public class News
    {
        public News()
        {
            TranslationList = new List<NewsTranslation>();
        }
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(100)]
        public string Sku { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }
        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int? View { get; set; }
        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// ردیف کاربر ویرایش کننده
        /// </summary>
        public int ModifyUserId { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// آی پی ایجاد کننده
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreateIp { get; set; }
        /// <summary>
        /// آی پی آخرین تغییرات
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string ModifyIp { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual NewsStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<NewsTranslation> TranslationList { get; set; }
    }
}
