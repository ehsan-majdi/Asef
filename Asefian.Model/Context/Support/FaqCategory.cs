using Asefian.Model.Context.Account;
using Asefian.Model.Context.Support.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Support
{
    /// <summary>
    /// دسته بندی سوالات متداول
    /// </summary>
    [Table("FaqCategory", Schema = "support")]
    public class FaqCategory
    {
        public FaqCategory()
        {
            FaqList = new List<Faq>();
        }
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Column("OrderNo")]
        public int Order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }
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
        public virtual FaqCategoryStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست سوالات متداول
        /// </summary>
        public virtual List<Faq> FaqList { get; set; }
    }

}
