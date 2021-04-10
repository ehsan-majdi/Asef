using Asefian.Model.Context.Account;
using Asefian.Model.Context.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    /// <summary>
    /// شعب
    /// </summary>
    [Table("Branch", Schema = "core")]
    public class Branch
    {
        public Branch()
        {
            TranslationList = new List<BranchTranslation>();
        }

        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        [Column("OrderNo")]
        public int Order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(100)]
        public string Sku { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [MaxLength(256)]
        public string Email { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        [MaxLength(100)]
        public string Tel { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        [MaxLength(100)]
        public string Fax { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        [MaxLength(38)]
        public string Latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        [MaxLength(38)]
        public string Longitude { get; set; }
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
        /// وضعیت
        /// </summary>
        public virtual BranchStatus Status { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreateUser { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<BranchTranslation> TranslationList { get; set; }
    }
}
