using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Context.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// مقدار ثبت شده فیلتر های محصولات
    /// </summary>
    [Table("ProductFilterData", Schema = "data")]
    public class ProductFilterData
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
        /// ردیف فیلتر محصول
        /// </summary>
        public int ProductFilterId { get; set; }
        /// <summary>
        /// ردیف مقدار فیلتر محصول
        /// </summary>
        public int? ProductFilterValueId { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// مقدار بولین
        /// </summary>
        public bool? ValueBoolean { get; set; }

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
        /// فبلتر محصول
        /// </summary>
        public virtual ProductFilter ProductFilter { get; set; }
        /// <summary>
        /// مقدار فیلتر محصول
        /// </summary>
        public virtual ProductFilterValue ProductFilterValue { get; set; }
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
