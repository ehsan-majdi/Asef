using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Financial
{
    /// <summary>
    /// سبد خرید
    /// </summary>
    [Table("Cart", Schema = "financial")]
    public class Cart
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف کاربر
        /// </summary>
        public int UserId { get; set; }
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
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// ویژگی انتخاب شده برای محصول
        /// </summary>
        public virtual ProductFeature ProductFeature { get; set; }
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
