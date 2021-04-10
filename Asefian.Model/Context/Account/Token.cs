using Asefian.Model.Context.Account.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// توکن های اعتبار سنجی کاربر
    /// </summary>
    [Table("Token", Schema = "account")]
    public class Token
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
        /// ردیف نوع دستگاه
        /// </summary>
        public int? DeviceTypeId { get; set; }
        /// <summary>
        /// ردیف سیستم عامل
        /// </summary>
        public int? OperatingSystemId { get; set; }
        /// <summary>
        /// ردیف مرورگر
        /// </summary>
        public int? BrowserId { get; set; }
        /// <summary>
        /// ورژن
        /// </summary>
        [MaxLength(100)]
        public string Version { get; set; }
        /// <summary>
        /// ابعاد صفحه نمایش
        /// </summary>
        [MaxLength(50)]
        public string ScreenSize { get; set; }
        /// <summary>
        /// مدل دستگاه
        /// </summary>
        [MaxLength(1000)]
        public string DeviceModel { get; set; }
        /// <summary>
        /// توکن اعتبار سنجی
        /// </summary>
        [MaxLength(1024)]
        public string AuthoritarianToken { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
        /// <summary>
        /// تاریخ انقضاء
        /// </summary>
        public DateTime ExpiredDateTime { get; set; }
        /// <summary>
        /// آی پی ایجاد کننده
        /// </summary>
        [Required]
        [MaxLength(48)]
        public string CreatedIp { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// نوع دستگاه
        /// </summary>
        public virtual DeviceType DeviceType { get; set; }
        /// <summary>
        /// سیستم عامل
        /// </summary>
        public virtual Enum.OperatingSystem OperatingSystem { get; set; }
        /// <summary>
        /// مرورگر
        /// </summary>
        public virtual Browser Browser { get; set; }
    }
}
