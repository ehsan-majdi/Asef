using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Context.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// فیلتر های محصولات
    /// </summary>
    [Table("ProductFilter", Schema = "data")]
    public class ProductFilter
    {
        public ProductFilter()
        {
            ValueList = new List<ProductFilterValue>();
            TranslationList = new List<ProductFilterTranslation>();
        }
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Column("OrderNo")]
        public int Order { get; set; }
        /// <summary>
        /// ردیف نوع فیلتر
        /// </summary>
        public int FilterTypeId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Sku { get; set; }
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
        /// دسته بندی
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// نوع فیلتر
        /// </summary>
        public virtual ProductFilterType FilterType { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual ProductFilterStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست مقدار های فیلتر ها
        /// </summary>
        public virtual List<ProductFilterValue> ValueList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<ProductFilterTranslation> TranslationList { get; set; }
    }
}
