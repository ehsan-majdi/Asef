using Asefian.Model.Context.Account;
using Asefian.Model.Context.Financial.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// جدول لیست استعلام
    /// </summary>
    [Table("Inquiry", Schema = "financial")]
    public class Inquiry
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف مشتری درخواست کننده استعلام
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// کد رهگیری استعلام
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// کاربر درخواست کننده استعلام
        /// </summary>
        [MaxLength(100)]
        public string FullName { get; set; }
        /// <summary>
        /// تلفن تماس استعلام گیرنده
        /// </summary>
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// توضیحات درخواست استعلام
        /// </summary>
        [MaxLength(1500)]
        public string Description { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }

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
        /// وضعیت
        /// </summary>
        public virtual InquiryStatus Status { get; set; }
        /// <summary>
        /// مشتری درخواست کننده استعلام
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست محصولات استعلام گرفته شده
        /// </summary>
        public virtual List<InquiryItem> InquiryItemList { get; set; }
    }
}
