using Asefian.Model.FileContext.Entity;
using Asefian.Model.FileContext.Enum;
using System.Data.Entity;

namespace Asefian.Model.FileContext
{
    /// <summary>
    /// مدیریت اتصال به پایگاه داده فایل 
    /// </summary>
    public class AsefianFileContext : DbContext
    {
        public AsefianFileContext() : base("AsefianFileContext")
        {
            Database.SetInitializer(new AsefianFileContextInitializer());
        }

        /// <summary>
        /// جدول فایل
        /// </summary>
        public DbSet<File> File { get; set; }

        /// <summary>
        /// جدول وضعیت فایل
        /// </summary>
        public DbSet<FileStatus> FileStatus { get; set; }

        /// <summary>
        /// ساختار رابطه بین موجودیت های دیتابیس
        /// </summary>
        /// <param name="model">سازنده مدل دیتابیس</param>
        protected override void OnModelCreating(DbModelBuilder model)
        {
            model.Entity<FileStatus>().HasMany(x => x.FileList).WithRequired(x => x.Status).HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);

            base.OnModelCreating(model);
        }

    }
}
