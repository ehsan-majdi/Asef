using Asefian.Model.Context.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// پرداخت های ثبت شده فاکتور
    /// </summary>
    [Table("InvoiceTransaction", Schema = "financial")]
    public class InvoiceTransaction
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
        /// شماره درخواست
        /// </summary>
        public long Code { get; set; }
        /// <summary>
        /// مبلغ خرید
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// اطلاعات توضيحي كه پذيرنده مايل به حفظ آنها براي هر تراكنش مي باشد
        /// </summary>
        public string AdditionalData { get; set; }
        /// <summary>
        /// كد مرجع درخواست پرداخت كه همراه با درخواست bpPayRequest توليد شده و به پذيرنده اختصاص يافته است
        /// </summary>
        public string RefId { get; set; }
        /// <summary>
        /// وضعيت خريد با توجه به جدول
        /// </summary>
        public string ResCodePayRequest { get; set; }
        /// <summary>
        /// وضعيت خريد با توجه به جدول
        /// </summary>
        public string ResCodeCallBackUrl { get; set; }
        /// <summary>
        /// كد مرجع تراكنش خريد كه از سايت بانك به پذيرنده داده می شود
        /// </summary>
        public long? SaleReferenceId { get; set; }
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
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
    }
}
