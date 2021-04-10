using Asefian.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context.Data.Enum
{
    public class ContactUsStatus : BaseEnum<ContactUsStatus>
    {
        public ContactUsStatus(int id, string title, string persianTitle) : base(id, title, persianTitle)
        {
        }

        /// فعال
        /// </summary>
        public readonly static ContactUsStatus Active = new ContactUsStatus(1, "Active", "فعال");
        /// <summary>
        /// غیرفعال
        /// </summary>
        public readonly static ContactUsStatus Inactive = new ContactUsStatus(2, "Inactive", "غیرفعال");
        /// <summary>
        /// حذف شده
        /// </summary>
        public readonly static ContactUsStatus Deleted = new ContactUsStatus(3, "Stock", "حذف شده");

        /// <summary>
        /// دریافت تمام مقدارهای وضعیت تماس با ما به صورت لیست
        /// </summary>
        /// <returns>لیست وضعیت تماس با ما</returns>
        public new static IEnumerable<ContactUsStatus> GetList()
        {
            return new ContactUsStatus[] {
                Active, Inactive, Deleted
            };
        }

    }
}
