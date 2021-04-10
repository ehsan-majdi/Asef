using Asefian.Model.Context.Account;
using Asefian.Model.Context.Financial.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// کوپن تخفیف
    /// </summary>
    [Table("Coupon", Schema = "financial")]
    public class Coupon
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// گروه های کوپن ساخته شده
        /// </summary>
        public int Group { get; set; }
        /// <summary>
        /// کد کوپن
        /// </summary>
        [MaxLength(15)]
        [Index("Code", IsUnique = true)]
        public string Code { get; set; }
        /// <summary>
        /// ردیف نوع کوپن
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// تعداد استفاده شده
        /// </summary>
        public int UsedCount { get; set; }
        /// <summary>
        /// تعداد قابل استفاده
        /// </summary>
        public int? UsableCount { get; set; }
        /// <summary>
        /// ردیف کاربر برای استفاده از کوپن
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// از تاریخ
        /// </summary>
        public DateTime? FromDate { get; set; }
        /// <summary>
        /// تا تاریخ
        /// </summary>
        public DateTime? ToDate { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [MaxLength(1500)]
        public string Description { get; set; }
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
        /// نوع کوپن
        /// </summary>
        public virtual CouponType Type { get; set; }
        /// <summary>
        /// کاربر برای استفاده از کوپن
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual CouponStatus Status { get; set; }
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
