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
    /// محصول
    /// </summary>
    [Table("Product", Schema = "data")]
    public class Product
    {

        public Product()
        {
            TranslationList = new List<ProductTranslation>();
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
        /// ردیف ویژگی دسته بندی
        /// </summary>
        public int? CategoryFeatureId { get; set; }
        /// <summary>
        /// ردیف برند
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// کد
        /// </summary>
        [MaxLength(30)]
        public string Code { get; set; }
        /// <summary>
        /// طول عکس
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// ارتفاع عکس
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(500)]
        public string Sku { get; set; }
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
        /// تعداد موجود در انبار
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int View { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public float Rate { get; set; }
        /// <summary>
        /// تعداد روز هایی که از تاریخ ویرایش با برچسب محصول جدید نمایش داده می شود.
        /// </summary>
        public DateTime? NewProductExpiredDate { get; set; }
        /// <summary>
        /// نمایش به عنوان پیشنهاد ویژه
        /// </summary>
        public bool Offer { get; set; }

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
        /// ویژگی دسته بندی
        /// </summary>
        public virtual CategoryFeature CategoryFeature { get; set; }
        /// <summary>
        /// برند
        /// </summary>
        public virtual Brand Brand { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual ProductStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست تصاویر گالری محصولات
        /// </summary>
        public virtual List<ProductFile> ProductFileList { get; set; }
        /// <summary>
        /// لیست مقدار ثبت شده فیلتر های محصولات
        /// </summary>
        public virtual List<ProductFilterData> ProductFilterDataList { get; set; }
        /// <summary>
        /// مقدارهای ویژگی های محصول و تعداد آن ها
        /// </summary>
        public virtual List<ProductFeature> ProductFeatureList { get; set; }
        public virtual List<ProductTranslation> TranslationList { get; set; }
    }
}
