using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Financial
{
    /// <summary>
    /// مدل سبد استعلام محصول
    /// </summary>
    public class InquiryViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف مشتری درخواست کننده استعلام
        /// </summary>
        public int? userId { get; set; }
        /// <summary>
        /// کد رهگیری استعلام
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// کاربر درخواست کننده استعلام
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن تماس استعلام گیرنده
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// توضیحات درخواست استعلام
        /// </summary>
        public string description { get; set; }
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
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string persianDate { get; set; }

        /// <summary>
        /// لیست محصولات استعلام گرفته شده
        /// </summary>
        public virtual List<InquiryItemViewModel> inquiryItemList { get; set; }
    }

    /// <summary>
    /// محصولات استعلام گرفته شده
    /// </summary>
    public class InquiryItemViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف استعلام مورد نظر
        /// </summary>
        public int inquiryId { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// عنوان محصول
        /// </summary>
        public string productTitle { get; set; }
        /// <summary>
        /// عنوان آدرس محصول
        /// </summary>
        public string productUrlTitle { get; set; }
        /// <summary>
        /// ردیف تصویر محصول
        /// </summary>
        public string productFileId { get; set; }
        /// <summary>
        /// عنوان تصویر محصول
        /// </summary>
        public string productFileName { get; set; }
        /// <summary>
        /// عنوان دسته بندی محصول
        /// </summary>
        public string productCategoryTitle { get; set; }
        /// <summary>
        /// عنوان برند محصول
        /// </summary>
        public string productBrandTitle { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// توضیحات لازم
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// مدل سبد استعلام محصول
    /// </summary>
    public class InquiryCartViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// نام محصول
        /// </summary>
        public string productTitle { get; set; }
        /// <summary>
        /// نام برای استفاده در آدرس محصول
        /// </summary>
        public string productUrlTitle { get; set; }
        /// <summary>
        /// ردیف فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// نام دسته بندی
        /// </summary>
        public string categoryTitle { get; set; }
        /// <summary>
        /// نام برند
        /// </summary>
        public string brandTitle { get; set; }
    }

    /// <summary>
    /// مدل سبد استعلام محصول
    /// </summary>
    public class InquiryCookieViewModel
    {
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// ردیف ویژگی انتخاب شده برای محصول
        /// </summary>
        public int? productFeatureId { get; set; }
        /// <summary>
        /// عنوان انتخاب شده ویژگی
        /// </summary>
        public string productFeatureTitle { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// مدل ساخت استعلام محصول که برای افرادی که لاگین نیستند در سیستم استفاده می شود
    /// </summary>
    public class MakeInquiryViewModel
    {
        /// <summary>
        /// نام کامل
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// متن عبارت امنیتی
        /// </summary>
        public string captcha { get; set; }
    }

    /// <summary>
    /// مدل جستجوی استعلام ها
    /// </summary>
    public class SearchInquiryViewModel
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
        /// لیست وضعیت های صفارشات
        /// </summary>
        public List<int> statusList { get; set; }
    }

    /// <summary>
    /// پاسخ جستجوی لیست استعلام ثبت شده خود کاربر
    /// </summary>
    public class ResponseSearchInquiryViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره استعلام
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// نام استعلام گیرنده
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianDate { get; set; }
    }

    /// <summary>
    /// پاسخ جستجوی لیست استعلام ثبت شده خود کاربر
    /// </summary>
    public class ResponseUserSearchInquiryViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره استعلام
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string PersianDate { get; set; }
    }

    /// <summary>
    /// مدل تغییر وضعیت استعلام
    /// </summary>
    public class InquiryChangeStatusViewModel
    {
        /// <summary>
        /// ردیف استعلام
        /// </summary>
        public int inquiryId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// توضیحات اضافی
        /// </summary>
        public string description { get; set; }
    }

}
