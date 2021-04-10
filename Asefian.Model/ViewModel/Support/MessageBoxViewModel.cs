using Asefian.Model.Context.Support.Enum;
using System;

namespace Asefian.Model.ViewModel.Support
{
    /// <summary>
    /// مدل پیام صندوق پیام ها
    /// </summary>
    public class MessageBoxViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianCreateDate { get; set; }
    }

    /// <summary>
    /// مدل جستجوی پیام صندوق پیام ها
    /// </summary>
    public class SearchMessageBoxViewModel
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
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات تماس با ما
    /// </summary>
    public class ContactUsViewModel
    {
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// متن کپچا
        /// </summary>
        public string captcha { get; set; }
    }

    /// <summary>
    /// مدل اطلاعات همکاری با ما
    /// </summary>
    public class WorkWithUsViewModel
    {
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// وضعیت مسیج
        /// </summary>
        public MessageType messageStatus { get; set; }
        /// <summary>
        /// وضعیت مسیج
        /// </summary>
        public int messageTypeId { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// متن کپچا
        /// </summary>
        public string captcha { get; set; }
    }
    /// <summary>
    /// مدل اطلاعات همکاری با ما
    /// </summary>
    public class DesignerViewModel
    {
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string mobileNumber { get; set; }
        /// <summary>
        /// وضعیت مسیج
        /// </summary>
        public int messageTypeId { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// متن پیام
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// متن کپچا
        /// </summary>
        public string captcha { get; set; }
    }
}
