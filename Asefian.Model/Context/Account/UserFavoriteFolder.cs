using Asefian.Model.Context.Account.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Account
{
    /// <summary>
    /// پوشه های علاقه مندی های کاربر
    /// </summary>
    [Table("UserFavoriteFolder", Schema = "account")]
    public class UserFavoriteFolder
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
        /// پوشه پدر
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(255)]
        public string Title { get; set; }
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
        /// پوشه پدر
        /// </summary>
        public virtual UserFavoriteFolder Parent { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public virtual UserFavoriteFolderStatus Status { get; set; }
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
