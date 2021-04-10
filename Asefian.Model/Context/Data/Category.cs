using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// دسته بندی
    /// </summary>
    [Table("Category", Schema = "data")]
    public class Category
    {
        public Category()
        {
            ChildList = new List<Category>();
            TranslationList = new List<CategoryTranslation>();
        }

        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف والد
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(200)]
        public string Sku { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Column("OrderNo")]
        public int Order { get; set; }
        /// <summary>
        /// ردیف نوع دسته بندی
        /// </summary>
        public int CategoryTypeId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }

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
        /// والد
        /// </summary>
        public virtual Category Parent { get; set; }
        /// <summary>
        /// نوع دسته بندی
        /// </summary>
        public virtual CategoryType CategoryType { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual CategoryStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست دسته بندی های فرزند
        /// </summary>
        public virtual List<Category> ChildList { get; set; }
        /// <summary>
        /// لیست محصولات
        /// </summary>
        public virtual List<Product> ProductList { get; set; }
        /// <summary>
        /// لیست فیلتر های دسته بندی
        /// </summary>
        public virtual List<ProductFilter> ProductFilterList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<CategoryTranslation> TranslationList { get; set; }

    }
}
