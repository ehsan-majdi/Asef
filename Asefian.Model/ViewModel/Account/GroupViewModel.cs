using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Data
{
    /// <summary>
    /// مدل گروه های کاربری
    /// </summary>
    public class GroupViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// دسترسی های کاربر
        /// </summary>
        public long permission { get; set; }
        /// <summary>
        /// دسترسی های کاربر
        /// </summary>
        public List<int> permissionList { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
    }

    /// <summary>
    /// مدل جستجوی گروه های کاربری
    /// </summary>
    public class SearchGroupViewModel
    {
        /// <summary>
        /// شماره صفحه
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// تعداد نمایش
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// عبارت مورد جستجو
        /// </summary>
        public string word { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی گروه های کاربری
    /// </summary>
    public class ResponseSearchGroupViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
    }

    /// <summary>
    /// کلاس برای دخیره و ارسال لیست دسترسی های گروه
    /// </summary>
    public class PermissionList
    {
        /// <summary>
        /// ردیف گروه کاربری
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// لیست دسترسی های گروه
        /// </summary>
        public List<int> permission { get; set; }
    }
}
