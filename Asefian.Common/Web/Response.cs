namespace Asefian.Common.Web
{
    /// <summary>
    /// شی بازگشتی برای درخواست های اجکس
    /// </summary>
    public class Response
    {
        /// <summary>
        /// وضعیت
        /// </summary>
        public ResponseStatus status { get; set; }
        /// <summary>
        /// پیام سیستم
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// پاسخ بازگشتی
        /// </summary>
        public object data { get; set; }
    }

    /// <summary>
    /// شی بازگشتی برای درخواست های اجکس
    /// </summary>
    public class Response<T>
    {
        /// <summary>
        /// وضعیت
        /// </summary>
        public ResponseStatus status { get; set; }
        /// <summary>
        /// پیام سیستم
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// پاسخ بازگشتی
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// پاسخ جستجو با صفحه بندی
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// لیست نتیجه
        /// </summary>
        public object list { get; set; }
        /// <summary>
        /// صفحه
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// تعداد هر صفحه
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// تعداد کل
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// تعداد کل صفحات
        /// </summary>
        public int totalPage { get; set; }
        /// <summary>
        /// فیلد مرتب سازی شده
        /// </summary>
        public string orderBy { get; set; }
        /// <summary>
        /// نوع مرتب سازی
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// ردیف محصول
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int specialProjectId { get; set; }
    }

    /// <summary>
    /// وضعیت های پاسخ
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// همه چیز به درستی دربافت شد
        /// </summary>
        Ok = 200,
        /// <summary>
        /// محتوای جزئی
        /// </summary>
        PartialContent = 206,
        /// <summary>
        /// اصلاح نشده
        /// </summary>
        NotModified = 304,
        /// <summary>
        /// غیر مجاز
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// ممنوع
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// یافت نشد
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// خطایی سمت سرور
        /// </summary>
        InternalServerError = 500
    }

}
