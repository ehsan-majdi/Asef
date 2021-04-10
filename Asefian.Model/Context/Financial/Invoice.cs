using Asefian.Model.Context.Account;
using Asefian.Model.Context.Financial.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// فاکتور
    /// </summary>
    [Table("Invoice", Schema = "financial")]
    public class Invoice
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف مشتری
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ردیف وضعیت فاکتور
        /// </summary>
        public int InvoiceStatusId { get; set; }
        /// <summary>
        /// ردیف نوع پرداخت
        /// </summary>
        public int PaymentTypeId { get; set; }
        /// <summary>
        /// ردیف نوع ارسال
        /// </summary>
        public int DeliveryTypeId { get; set; }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        [MaxLength(20)]
        public string InvoiceNo { get; set; }
        /// <summary>
        /// مبلغ فاکتور
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// مبلغ قابل پرداخت فاکتور
        /// </summary>
        public decimal UnpaidPrice { get; set; }
        /// <summary>
        /// هزینه حمل
        /// </summary>
        public decimal DeliveryPrice { get; set; }
        /// <summary>
        /// ردیف آدرس تحویل گیرنده
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// تاریخ تحویل
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// زمان تحویل
        /// </summary>
        [MaxLength(20)]
        public string DeliveryTime { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }
        /// <summary>
        /// ردیف کوپن تخفیف
        /// </summary>
        public int? CouponId { get; set; }
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
        /// مشتری
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// وضعیت فاکتور
        /// </summary>
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        /// <summary>
        /// نوع پرداخت
        /// </summary>
        public virtual PaymentType PaymentType { get; set; }
        /// <summary>
        /// نوع ارسال
        /// </summary>
        public virtual DeliveryType DeliveryType { get; set; }
        /// <summary>
        /// آدرس تحویل
        /// </summary>
        public virtual UserAddress Address { get; set; }
        /// <summary>
        /// کوپن تخفیف
        /// </summary>
        public virtual Coupon Coupon { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست جزئیات فاکتور محصولات
        /// </summary>
        public virtual List<InvoiceDetail> DetailList { get; set; }

        /// <summary>
        /// لیست سوابق فاکتور
        /// </summary>
        public virtual List<InvoiceLog> InvoiceLogList { get; set; }
    }
}
