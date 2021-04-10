using Asefian.Model.Context.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    /// <summary>
    /// اطلاعات پایه
    /// </summary>
    [Table("BaseInformation", Schema = "core")]
    public class BaseInformation
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? LanguageId { get; set; }
        /// <summary>
        /// کلید
        /// </summary>
        [MaxLength(200)]
        public string Key { get; set; }
        /// <summary>
        /// مقدار
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ردیف کاربر ویرایش کننده
        /// </summary>
        public int? ModifyUserId { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// آی پی آخرین تغییرات
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string ModifyIp { get; set; }

        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifyUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
