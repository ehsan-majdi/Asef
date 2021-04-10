using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// تصاویر گالری محصول
    /// </summary>
    [Table("ProductFile", Schema = "data")]
    public class ProductFile
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// ردیف ویژگی محصول که برای این مورد استفاده می شود که فایل مربوط به آن ویژگی را نگه دارد
        /// به طور مثال رنگ کیف قرمز
        /// </summary>
        public int? ProductFeatureId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(500)]
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }
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
        /// ردیف نوع فایل
        /// </summary>
        public int TypeId { get; set; }
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
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// ویژگی محصول
        /// </summary>
        public virtual ProductFeature ProductFeature { get; set; }
        /// <summary>
        /// نوع فایل
        /// </summary>
        public virtual ProductFileType Type { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual ProductFileStatus Status { get; set; }
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
