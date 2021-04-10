using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// محصولات استعلام گرفته شده
    /// </summary>
    [Table("InquiryItem", Schema = "financial")]
    public class InquiryItem
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف استعلام مورد نظر
        /// </summary>
        public int InquiryId { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// توضیحات لازم
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; }
        /// <summary>
        /// ردیف کاربر ایجاد کننده
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// ردیف کاربر ویرایش کننده
        /// </summary>
        public int? ModifyUserId { get; set; }
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
        /// استعلام
        /// </summary>
        public virtual Inquiry Inquiry { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
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
