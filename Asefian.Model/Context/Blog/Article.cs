using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Blog.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nig.Model.Context.Blog
{
    /// <summary>
    /// مقالات
    /// </summary>
    [Table("Article", Schema = "blog")]
    public class Article
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// پوستر
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(500)]
        public string Sku { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public float Rate { get; set; }
        /// <summary>
        /// تاریخ انتشار
        /// </summary>
        public DateTime PublishDate { get; set; }
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
        /// دسته بندی
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual ArticleStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
    }
}
