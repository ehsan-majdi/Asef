using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Financial.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// جزئیات فاکتور
    /// </summary>
    [Table("InvoiceDetail", Schema = "financial")]
    public class InvoiceDetail
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int InvoiceId { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// ردیف ویژگی انتخاب شده برای محصول
        /// </summary>
        public int? ProductFeatureId { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal Price { get; set; }
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
        /// فاکتور
        /// </summary>
        public virtual Invoice Invoice { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// ویژگی انتخاب شده برای محصول
        /// </summary>
        public virtual ProductFeature ProductFeature { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual InvoiceDetailStatus Status { get; set; }
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
