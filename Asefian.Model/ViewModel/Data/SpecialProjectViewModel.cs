using System.Collections.Generic;

namespace Asefian.Model.ViewModel.Data
{
    public class SpecialProjectViewModel
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
        /// 
        /// </summary>
        public int galleryCount { get; set; }
        /// <summary>
        /// شناسه فایل
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// ردیف وضعیت
        /// </summary>
        public int statusId { get; set; }
        /// <summary>
        /// عنوان وضعیت
        /// </summary>
        public string statusTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SpecialProjectTranslationViewModel> translations { get; set; }
    }

    public class SpecialProjectTranslationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int specialProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int languageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
    }
    public class SpecialProjectFileViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// ردیف پروژ های خاص
        /// </summary>
        public int specialProjectId { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// شناسه فایل در تلگرام
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// لینک
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sku { get; set; }
    }
    public class AddSpecialProjectFileViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int id { get; set; }
        
        public string title { get; set; }

        public List<AddSpecialProjectFileTranslationViewModel> translations { get; set; }
    }
    public class AddSpecialProjectFileTranslationViewModel
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int specialProjectFileId { get; set; }
        public int languageId { get; set; }
        public string title { get; set; }
        public string description { get; set; }

    }

}
