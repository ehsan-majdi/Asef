using Asefian.Model.Context.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Core
{
    /// <summary>
    /// موقعیت جغرافیایی
    /// </summary>
    [Table("Location", Schema = "core")]
    public class Location
    {
        /// <summary>
        /// ردیف
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ردیف والد
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// ردیف نوع موقعیت جغرافیایی
        /// </summary>
        public int LocationTypeId { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// والد
        /// </summary>
        public virtual Location Parent { get; set; }
        /// <summary>
        /// نوع موقعیت جغرافیایی
        /// </summary>
        public virtual LocationType LocationType { get; set; }
    }
}
