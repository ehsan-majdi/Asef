using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Core
{
    /// <summary>
    /// مدل شعبه
    /// </summary>
    public class BranchViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// شناسه
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BranchTranslationViewModel> translations { get; set; }
    }

    public class BranchTranslationViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف دسته بندی
        /// </summary>
        public int branchId { get; set; }
        /// <summary>
        /// ردیف زبان
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string address { get; set; }

    }

    /// <summary>
    /// مدل جستجوی شعبه
    /// </summary>
    public class SearchBranchViewModel
    {
        /// <summary>
        /// عبارت مورد جستجو
        /// </summary>
        public string word { get; set; }
    }

    /// <summary>
    /// مدل پاسخ جستجوی شعبه
    /// </summary>
    public class ResponseSearchBranchViewModel
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

}
