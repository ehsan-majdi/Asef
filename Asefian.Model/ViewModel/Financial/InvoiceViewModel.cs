using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Financial
{
    /// <summary>
    /// مدل فاکتور های صادر شده
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف مشتری
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// نام کاربر
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// ردیف وضعیت فاکتور
        /// </summary>
        public int invoiceStatusId { get; set; }
        /// <summary>
        /// عنوان وضعیت فاکتور
        /// </summary>
        public string invoiceStatusTitle { get; set; }
        /// <summary>
        /// ردیف نوع پرداخت
        /// </summary>
        public int paymentTypeId { get; set; }
        /// <summary>
        /// عنوان نوع پرداخت
        /// </summary>
        public string paymentTypeTitle { get; set; }
        /// <summary>
        /// ردیف نوع ارسال
        /// </summary>
        public int deliveryTypeId { get; set; }
        /// <summary>
        /// عنوان نوع ارسال
        /// </summary>
        public string deliveryTypeTitle { get; set; }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// مبلغ فاکتور
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// مبلغ قابل پرداخت فاکتور
        /// </summary>
        public decimal unpaidPrice { get; set; }
        /// <summary>
        /// هزینه حمل
        /// </summary>
        public decimal deliveryPrice { get; set; }
        /// <summary>
        /// ردیف آدرس تحویل گیرنده
        /// </summary>
        public int addressId { get; set; }
        /// <summary>
        /// نام تحویل گیرنده
        /// </summary>
        public string receiverFullName { get; set; }
        /// <summary>
        /// تلفن همراه دریافت کننده بسته
        /// </summary>
        public string receiverMobileNumber { get; set; }
        /// <summary>
        /// تلفن محل دریافت بسته
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// ردیف استان
        /// </summary>
        public int provinceId { get; set; }
        /// <summary>
        /// عنوان استان
        /// </summary>
        public string provinceTitle { get; set; }
        /// <summary>
        /// ردیف شهر
        /// </summary>
        public int cityId { get; set; }
        /// <summary>
        /// عنوان شهر
        /// </summary>
        public string cityTitle { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string postalCode { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// تاریخ تحویل
        /// </summary>
        public DateTime? deliveryDate { get; set; }
        /// <summary>
        /// تاریخ تحویل
        /// </summary>
        public string PersianDeliveryDate { get; set; }
        /// <summary>
        /// زمان تحویل
        /// </summary>
        public string deliveryTime { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// ردیف کوپن تخفیف
        /// </summary>
        public int? couponId { get; set; }
        /// <summary>
        /// کد کوپن تخفیف
        /// </summary>
        public string couponCode { get; set; }
        /// <summary>
        /// تاریخ ثبت سفارش
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// تاریخ شمسی ثبت سفارش
        /// </summary>
        public string PersianCreateDate { get; set; }

        /// <summary>
        /// لیست محصولات یک فاکتور
        /// </summary>
        public List<InvoiceDetailViewModel> detailList { get; set; }
        /// <summary>
        /// سوابق یک فاکتور
        /// </summary>
        public List<InvoiceLogViewModel> logList { get; set; }
    }

    /// <summary>
    /// جزئیات فاکتور
    /// </summary>
    public class InvoiceDetailViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
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
        /// عنوان ویژگی
        /// </summary>
        public string featureTitle { get; set; }
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
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
    }

    /// <summary>
    /// سابقه فاکتور
    /// </summary>
    public class InvoiceLogViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// ردیف وضعیت فاکتور
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت فاکتور
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// تاریخ
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ شمسی
        /// </summary>
        public string PersianDate { get; set; }
        /// <summary>
        /// نام کاربر
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        public string lastName { get; set; }
    }

    /// <summary>
    /// مدل جستجوی فاکتورهای صادره
    /// </summary>
    public class SearchInvoiceViewModel
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
    /// پاسخ جستجوی لیست فاکتور
    /// </summary>
    public class ResponseSearchInvoiceViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// نام خریدار
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public int invoiceStatusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string invoiceStatusTitle { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// تاریخ ایجاد شمسی
        /// </summary>
        public string persianDate { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// مبلغ قابل پرداخت
        /// </summary>
        public decimal unpaidPrice { get; set; }
    }

    /// <summary>
    /// پاسخ جستجوی لیست فاکتور ثبت شده خود کاربر
    /// </summary>
    public class ResponseUserSearchInvoiceViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public int invoiceStatusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string invoiceStatusTitle { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// مبلغ قابل پرداخت
        /// </summary>
        public decimal unpaidPrice { get; set; }
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
    /// مدل تغییر وضعیت فاکتور
    /// </summary>
    public class InvoiceChangeStatusViewModel
    {
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// توضیحات اضافی
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// مدل اضافه کردن یا حذف کردن محصول به فاکتور
    /// </summary>
    public class InvoiceChangeProductViewModel
    {
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// ردیف ویژگی انتخاب شده برای محصول
        /// </summary>
        public int? productFeatureId { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// مدل تغییر آدرس
    /// </summary>
    public class InvoiceChangeAddressViewModel
    {
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// ردیف آدرس جدید
        /// </summary>
        public int addressId { get; set; }
    }

    /// <summary>
    /// مدل تایید نحوه پرداخت فاکتور
    /// </summary>
    public class SubmitPaymentViewModel
    {
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// نوع پرداخت
        /// </summary>
        public int paymentType { get; set; }
        /// <summary>
        /// کد کوپن مورد استفاده
        /// </summary>
        public string coupon { get; set; }
    }

    /// <summary>
    /// مدل ثبت پرداخت فاکتور
    /// </summary>
    public class InvoicePaidViewModel
    {
        /// <summary>
        /// ردیف فاکتور
        /// </summary>
        public int invoiceId { get; set; }
        /// <summary>
        /// مبلغ پرداخت شده
        /// </summary>
        public decimal price { get; set; }
    }

}
