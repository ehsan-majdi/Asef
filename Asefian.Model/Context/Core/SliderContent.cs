using Asefian.Model.Context.Account;
using Asefian.Model.Context.Core.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    /// <summary>
    /// محتوای اسلایدر
    /// </summary>
    [Table("SliderContent", Schema = "core")]
    public class SliderContent
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف اسلایدر
        /// </summary>
        public int SliderId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Column("OrderNo")]
        public int Order { get; set; }
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
        /// متن
        /// </summary>
        [MaxLength(1500)]
        public string Text { get; set; }
        /// <summary>
        /// لینک
        /// </summary>
        [MaxLength(1500)]
        public string Link { get; set; }
        /// <summary>
        /// عرض
        /// </summary>
        public int Width { get; set; }
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
        public virtual SliderContentStatus Status { get; set; }
        /// <summary>
        /// اسلایدر
        /// </summary>
        public virtual Slider Slider { get; set; }
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
