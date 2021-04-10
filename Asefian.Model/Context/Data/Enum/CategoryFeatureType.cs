using Asefian.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asefian.Model.Context.Data.Enum
{
    /// <summary>
    /// نوع ویژگی دسته بندی
    /// </summary>
    [Table(name: "CategoryFeatureType", Schema = "enum")]
    public class CategoryFeatureType : BaseEnum<CategoryFeatureType>
    {
        public CategoryFeatureType(int id, string title, string PersianTitle) : base(id, title, PersianTitle)
        {
        }

        /// <summary>
        /// انتخابی
        /// </summary>
        public readonly static CategoryFeatureType Selective = new CategoryFeatureType(1, "Selective", "انتخابی");
        /// <summary>
        /// کشویی
        /// </summary>
        public readonly static CategoryFeatureType DropDown = new CategoryFeatureType(2, "DropDown", "کشویی");
        /// <summary>
        /// رنگ
        /// </summary>
        public readonly static CategoryFeatureType Color = new CategoryFeatureType(3, "Color", "رنگ");

        /// <summary>
        /// دریافت تمام مقدارهای نوع ویژگی دسته بندی به صورت لیست
        /// </summary>
        /// <returns>لیست نوع ویژگی دسته بندی</returns>
        public new static IEnumerable<CategoryFeatureType> GetList()
        {
            return new CategoryFeatureType[] {
                Selective, DropDown, Color
            };
        }
    }
}
