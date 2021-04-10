using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Financial
{
    /// <summary>
    /// مدل سبد خرید
    /// </summary>
    public class CartViewModel
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
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// تعداد موجودی کالا در انبار
        /// </summary>
        public int inventory { get; set; }
    }

    public class CartTranslationViewModel
    {
        public int cartId { get; set; }
        public int languageId { get; set; }
        public int itle { get; set; }
        public int t { get; set; }
    }


    /// <summary>
    /// مدل اطلاعات ارسال و تایید فاکتور
    /// </summary>
    public class ShippingViewModel
    {
        /// <summary>
        /// ردیف آدرس تحویل گیرنده
        /// </summary>
        public int addressId { get; set; }
        /// <summary>
        /// نحوه دریافت
        /// </summary>
        public int deliveryType { get; set; }
        /// <summary>
        /// تاریخ و ساعت دریافت
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// کد کوپن مورد استفاده
        /// </summary>
        public string coupon { get; set; }
    }

    /// <summary>
    /// مدل سبد خرید
    /// </summary>
    public class CartCookieViewModel
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

}
