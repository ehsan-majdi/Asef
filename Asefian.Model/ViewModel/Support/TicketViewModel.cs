namespace Asefian.Model.ViewModel.Support
{
    /// <summary>
    /// مدل تیکت
    /// </summary>
    public class TicketViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ردیف کاربر
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// ردیف اولویت تیکت
        /// </summary>
        public int priorityId { get; set; }
        /// <summary>
        /// اولویت تیکت
        /// </summary>
        public string priorityTitle { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// شماره تیکت
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public string statusTitle { get; set; }
    }


    /// <summary>
    /// مدل جستجوی تیکت
    /// </summary>
    public class SearchTicketViewModel
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
    /// مدل نتیجه جستجوی تیکت
    /// </summary>
    public class ResponseTicketFaqViewModel
    {

    }

}
