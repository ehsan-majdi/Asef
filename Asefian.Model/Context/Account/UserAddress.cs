using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Context.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// آدرس های کاربر
    /// </summary>
    [Table("UserAddress", Schema = "account")]
    public class UserAddress
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
        /// نام کامل دریافت کننده بسته
        /// </summary>
        [MaxLength(100)]
        public string ReceiverFullName { get; set; }
        /// <summary>
        /// تلفن همراه دریافت کننده بسته
        /// </summary>
        [MaxLength(14)]
        public string ReceiverMobileNumber { get; set; }
        /// <summary>
        /// تلفن محل دریافت بسته
        /// </summary>
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// ردیف کشور
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// ردیف استان
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// ردیف شهر
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [MaxLength(10)]
        public string PostalCode { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [MaxLength(1000)]
        public string Address { get; set; }
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
        /// آدرس پیشفرض
        /// </summary>
        public bool MainAddress { get; set; }
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
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// کشور
        /// </summary>
        public virtual Location Country { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public virtual Location Province { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public virtual Location City { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual UserAddressStatus Status { get; set; }
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
