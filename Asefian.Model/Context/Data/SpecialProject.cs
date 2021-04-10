using Asefian.Model.Context.Account;
using Asefian.Model.Context.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data
{
    [Table("SpecialProject", Schema = "data")]
    public class SpecialProject
    {
        public SpecialProject()
        {
            TranslationList = new List<SpecialProjectTranslation>();
            SpecialProjectFileList = new List<SpecialProjectFile>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public string FileId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }
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
        /// 
        /// </summary>
        public virtual SpecialProjectStatus Status { get; set; }
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
        public virtual List<SpecialProjectTranslation> TranslationList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<SpecialProjectFile> SpecialProjectFileList { get; set; }
    }
}
