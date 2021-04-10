using Asefian.Model.FileContext.Enum;
using System.Data.Entity;

namespace Asefian.Model.FileContext
{
    /// <summary>
    /// سازنده دیتابیس فایل
    /// از این کلاس برای درج مقادیر پیش فرض به جدول های مورد نظر استفاده می شود.
    /// </summary>
    public class AsefianFileContextInitializer : DropCreateDatabaseIfModelChanges<AsefianFileContext>
    {
        /// <summary>
        /// درج مقادیر مورد نیاز در دیتابیس به هنگام ساخت دیتابیس
        /// </summary>
        /// <param name="context">شی دیتابیس</param>
        protected override void Seed(AsefianFileContext context)
        {
            foreach (var item in FileStatus.GetList())
            {
                context.FileStatus.Add(item);
            }

            base.Seed(context);
        }
    }
}
