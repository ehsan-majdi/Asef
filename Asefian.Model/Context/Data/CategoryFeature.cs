﻿using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    /// <summary>
    /// ویژگی های تعریف شده برای هر دسته بندی که به محصولات متصل شود
    /// </summary>
    [Table("CategoryFeature", Schema = "data")]
    public class CategoryFeature
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(500)]
        public string Sku { get; set; }
        /// <summary>
        /// ردیف نوع ویژگی دسته بندی
        /// </summary>
        public int TypeId { get; set; }
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
        /// نوع ویژگی
        /// </summary>
        public virtual CategoryFeatureType Type { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual CategoryFeatureStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }

        /// <summary>
        /// لیست مقادیر ویژگی
        /// </summary>
        public virtual List<CategoryFeatureValue> ValueList { get; set; }
    }
}
