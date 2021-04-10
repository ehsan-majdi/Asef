using Asefian.Model.Context.Account;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Context.Blog;
using Asefian.Model.Context.Blog.Enum;
using Asefian.Model.Context.Core;
using Asefian.Model.Context.Core.Enum;
using Asefian.Model.Context.Data;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Context.Financial;
using Asefian.Model.Context.Financial.Enum;
using Asefian.Model.Context.Support;
using Asefian.Model.Context.Support.Enum;
using Nig.Model.Context.Blog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context
{
    /// <summary>
    /// مدیریت اتصال به پایگاه داده 
    /// </summary>
    public class AsefianContext : DbContext
    {

        public AsefianContext() : base("AsefianContext")
        {
            Database.SetInitializer(new AsefianContextInitializer());
        }

        public DbSet<Language> Language { get; set; }


        #region Account
        #region Enum
        /// <summary>
        ///جدول نوع کاربر
        /// </summary>
        public DbSet<UserType> UserType { get; set; }
        /// <summary>
        /// جدول نوع بالانس
        /// </summary>
        public DbSet<BalanceType> BalanceType { get; set; }
        /// <summary>
        /// جدول مرورگر
        /// </summary>
        public DbSet<Browser> Browser { get; set; }
        /// <summary>
        /// جدول نوع دستگاه
        /// </summary>
        public DbSet<DeviceType> DeviceType { get; set; }
        /// <summary>
        /// جدول وضعیت گروه
        /// </summary>
        public DbSet<GroupStatus> GroupStatus { get; set; }
        /// <summary>
        /// جدول وضعیت اعلانات
        /// </summary>
        public DbSet<NotificationStatus> NotificationStatus { get; set; }
        /// <summary>
        /// جدول نوع اعلانات
        /// </summary>
        public DbSet<NotificationType> NotificationType { get; set; }
        /// <summary>
        /// جدول نوع سیستم عامل
        /// </summary>
        public DbSet<Account.Enum.OperatingSystem> OperatingSystem { get; set; }
        /// <summary>
        /// جدول جنسیت
        /// </summary>
        public DbSet<Sex> Sex { get; set; }
        /// <summary>
        /// جدول نوع توکن ها
        /// </summary>
        public DbSet<TokenType> TokenType { get; set; }
        /// <summary>
        /// جدول وضعیت آدرس کاربر
        /// </summary>
        public DbSet<UserAddressStatus> UserAddressStatus { get; set; }
        /// <summary>
        /// جدول وضعیت پوشه علاقه مندی کاربر
        /// </summary>
        public DbSet<UserFavoriteFolderStatus> UserFavoriteFolderStatus { get; set; }
        /// <summary>
        /// جدول وضعیت علاقه مندی کاربر
        /// </summary>
        public DbSet<UserFavoriteStatus> UserFavoriteStatus { get; set; }
        /// <summary>
        /// جدول وضعیت کاربران
        /// </summary>
        public DbSet<UserStatus> UserStatus { get; set; }
        #endregion

        /// <summary>
        /// جدول گروه کاربری
        /// </summary>
        public DbSet<Group> Group { get; set; }
        /// <summary>
        /// اعلان های فرستاده شده به کاربر
        /// </summary>
        public DbSet<UserNotification> UserNotification { get; set; }
        /// <summary>
        /// جدول اعلانات
        /// </summary>
        public DbSet<Notification> Notification { get; set; }
        /// <summary>
        /// جدول توکن های اعتبار سنجی
        /// </summary>
        public DbSet<Token> Token { get; set; }
        /// <summary>
        /// جدول آدرس های کاربر
        /// </summary>
        public DbSet<UserAddress> UserAddress { get; set; }
        /// <summary>
        /// جدول سوابق بالانس مالی کاربر
        /// </summary>
        public DbSet<UserBalanceLog> UserBalanceLog { get; set; }
        /// <summary>
        /// جدول علاقه مندی های کاربر
        /// </summary>
        public DbSet<UserFavorite> UserFavorite { get; set; }
        /// <summary>
        /// جدول پوشه های علاقه مندی های کاربر
        /// </summary>
        public DbSet<UserFavoriteFolder> UserFavoriteFolder { get; set; }
        /// <summary>
        /// جدول کاربران
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// جدول گروه های کاربر
        /// </summary>
        public DbSet<UserGroup> UserGroup { get; set; }
        /// <summary>
        /// جدول نشان توکن برای احراز هویت کاربر
        /// </summary>
        public DbSet<UserVerifyToken> UserVerifyToken { get; set; }
        #endregion

        #region Blog
        #region Enum
        /// <summary>
        /// جدول وضعیت پست
        /// </summary>
        public DbSet<ArticleFileStatus> ArticleFileStatus { get; set; }
        /// <summary>
        /// جدول وضعیت مقالات
        /// </summary>
        public DbSet<ArticleStatus> ArticleStatus { get; set; }
        /// <summary>
        /// جدول وضعیت اخبار
        /// </summary>
        public DbSet<NewsStatus> NewsStatus { get; set; }
        #endregion

        /// <summary>
        /// جدول مقالات
        /// </summary>
        public DbSet<Article> Article { get; set; }
        /// <summary>
        /// جدول فیلتر های مقالات
        /// </summary>
        public DbSet<ArticleFile> ArticleFile { get; set; }
        /// <summary>
        /// مقالات
        /// </summary>
        public DbSet<ArticleTranslation> ArticleTranslation { get; set; }
        /// <summary>
        /// جدول اخبار
        /// </summary>
        public DbSet<News> News { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<NewsTranslation> NewsTranslation { get; set; }
        #endregion

        #region Core
        #region Enum
        /// <summary>
        /// جدول وضعیت شعبه
        /// </summary>
        public DbSet<BranchStatus> BranchStatus { get; set; }
        /// <summary>
        /// جدول نوع موقعیت جغرافیایی
        /// </summary>
        public DbSet<LocationType> LocationType { get; set; }
        /// <summary>
        /// جدول وضعیت محتوی اسلایدر
        /// </summary>
        public DbSet<SliderContentStatus> SliderContentStatus { get; set; }
        /// <summary>
        /// جدول وضعیت اسلایدر
        /// </summary>
        public DbSet<SliderStatus> SliderStatus { get; set; }
        /// <summary>
        /// جدول وضعیت نوع اسلایدر
        /// </summary>
        public DbSet<SliderType> SliderType { get; set; }
        #endregion

        /// <summary>
        /// جدول اطلاعات پایه
        /// </summary>
        public DbSet<BaseInformation> BaseInformation { get; set; }
        /// <summary>
        /// جدول شعب
        /// </summary>
        public DbSet<Branch> Branch { get; set; }
        /// <summary>
        /// جدول موقعیت جغرافیایی
        /// </summary>
        public DbSet<Location> Location { get; set; }
        /// <summary>
        /// جدول اسلایدر
        /// </summary>
        public DbSet<Slider> Slider { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SliderTranslation> SliderTranslation { get; set; }
        /// <summary>
        /// جدول محتوای اسلایدر
        /// </summary>
        public DbSet<SliderContent> SliderContent { get; set; }
        #endregion

        #region Data
        #region Enum
        /// <summary>
        /// 
        /// </summary>
        public DbSet<AboutUsStatus> AboutUsStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ExportStatus> ExportStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ContactUsStatus> ContactUsStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProjectFileStatus> SpecialProjectFileStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProjectStatus> SpecialProjectStatus { get; set; }
        /// <summary>
        /// جدول وضعیت برند
        /// </summary>
        public DbSet<BrandStatus> BrandStatus { get; set; }
        /// <summary>
        /// جدول وضعیت ویژگی دسته بندی
        /// </summary>
        public DbSet<CategoryFeatureStatus> CategoryFeatureStatus { get; set; }
        /// <summary>
        /// جدول نوع ویژگی دسته بندی
        /// </summary>
        public DbSet<CategoryFeatureType> CategoryFeatureType { get; set; }
        /// <summary>
        /// جدول وضعیت دسته بندی
        /// </summary>
        public DbSet<CategoryStatus> CategoryStatus { get; set; }
        /// <summary>
        /// جدول نوع دسته بندی
        /// </summary>
        public DbSet<CategoryType> CategoryType { get; set; }
        /// <summary>
        /// جدول وضعیت فایل در مرکز دانلود فایل
        /// </summary>
        public DbSet<DownloadCenterStatus> DownloadCenterStatus { get; set; }
        /// <summary>
        /// جدول وضعیت فایل های محصول
        /// </summary>
        public DbSet<ProductFileStatus> ProductFileStatus { get; set; }
        /// <summary>
        /// جدول نوع فایل های محصول
        /// </summary>
        public DbSet<ProductFileType> ProductFileType { get; set; }
        /// <summary>
        /// جدول وضعیت فیلتر محصول
        /// </summary>
        public DbSet<ProductFilterStatus> ProductFilterStatus { get; set; }
        /// <summary>
        /// جدول نوع فیلتر محصول
        /// </summary>
        public DbSet<ProductFilterType> ProductFilterType { get; set; }
        /// <summary>
        /// جدول وضعیت مقدار فیلتر محصول
        /// </summary>
        public DbSet<ProductFilterValueStatus> ProductFilterValueStatus { get; set; }
        /// <summary>
        /// جدول وضعیت محصول
        /// </summary>
        public DbSet<ProductStatus> ProductStatus { get; set; }
        /// <summary>
        /// جدول وضعیت خدمات
        /// </summary>
        public DbSet<ServiceStatus> ServiceStatus { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public DbSet<AboutUs> AboutUs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<AboutUsTranslation> AboutUsTranslation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Export> Export { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ExportTranslation> ExportTranslation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ContactUs> ContactUs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ContactUsTranslation> ContactUsTranslation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProjectFile> SpecialProjectFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProjectFileTranslation> SpecialProjectFileTranslation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProject> SpecialProject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SpecialProjectTranslation> SpecialProjectTranslation { get; set; }
        /// <summary>
        /// جدول برند
        /// </summary>
        public DbSet<Brand> Brand { get; set; }
        /// <summary>
        /// ترجمه برند
        /// </summary>
        public DbSet<BrandTranslation> BrandTranslation { get; set; }
        /// <summary>
        /// جدول دسته بندی
        /// </summary>
        public DbSet<Category> Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<CategoryTranslation> CategoryTranslation { get; set; }
        /// <summary>
        /// جدول ویژگی دسته بندی
        /// </summary>
        public DbSet<CategoryFeature> CategoryFeature { get; set; }
        /// <summary>
        /// جدول مقدارهای ویژگی دسته بندی
        /// </summary>
        public DbSet<CategoryFeatureValue> CategoryFeatureValue { get; set; }
        /// <summary>
        /// جدول محصول
        /// </summary>
        public DbSet<Product> Product { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ProductTranslation> ProductTranslation { get; set; }
        /// <summary>
        /// جدول ویژگی محصول
        /// </summary>
        public DbSet<ProductFeature> ProductFeature { get; set; }
        /// <summary>
        /// جدول فیلتر های محصولات
        /// </summary>
        public DbSet<ProductFilter> ProductFilter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ProductFilterTranslation> ProductFilterTranslation { get; set; }
        /// <summary>
        /// جدول مقدار ثبت شده فیلتر های محصولات
        /// </summary>
        public DbSet<ProductFilterData> ProductFilterData { get; set; }
        /// <summary>
        /// جدول مقدار فیلتر های محصولات
        /// </summary>
        public DbSet<ProductFilterValue> ProductFilterValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ProductFilterValueTranslation> ProductFilterValueTranslation { get; set; }
        /// <summary>
        /// جدول فایل های محصول
        /// </summary>
        public DbSet<ProductFile> ProductFile { get; set; }
        /// <summary>
        /// جدول خدمات
        /// </summary>
        public DbSet<Service> Service { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ServiceTranslation> ServiceTranslation { get; set; }

        #endregion

        #region Financial
        #region Enum
        /// <summary>
        /// جدول وضعیت کوپن
        /// </summary>
        public DbSet<CouponStatus> CouponStatus { get; set; }
        /// <summary>
        /// جدول نوع کوپن
        /// </summary>
        public DbSet<CouponType> CouponType { get; set; }
        /// <summary>
        /// جدول نوع ارسال
        /// </summary>
        public DbSet<DeliveryType> DeliveryType { get; set; }
        /// <summary>
        /// جدول وضعیت استعلام
        /// </summary>
        public DbSet<InquiryStatus> InquiryStatus { get; set; }
        /// <summary>
        /// جدول وضعیت محصول داخل فاکتور
        /// </summary>
        public DbSet<InvoiceDetailStatus> InvoiceDetailStatus { get; set; }
        /// <summary>
        /// جدول وضعیت فاکتور
        /// </summary>
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        /// <summary>
        /// جدول نوع پرداخت
        /// </summary>
        public DbSet<PaymentType> PaymentType { get; set; }
        #endregion

        /// <summary>
        /// جدول سبد خرید
        /// </summary>
        public DbSet<Cart> Cart { get; set; }
        /// <summary>
        /// جدول کوپن تخفیف
        /// </summary>
        public DbSet<Coupon> Coupon { get; set; }
        /// <summary>
        /// جدول استعلام
        /// </summary>
        public DbSet<Inquiry> Inquiry { get; set; }
        /// <summary>
        /// سبد استعلام محصول
        /// </summary>
        public DbSet<InquiryCart> InquiryCart { get; set; }
        /// <summary>
        /// جدول لیست محصول استعلام
        /// </summary>
        public DbSet<InquiryItem> InquiryItem { get; set; }
        /// <summary>
        /// جدول فاکتور
        /// </summary>
        public DbSet<Invoice> Invoice { get; set; }
        /// <summary>
        /// جدول جزئیات فاکتور
        /// </summary>
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        /// <summary>
        /// جدول سابقه فاکتور
        /// </summary>
        public DbSet<InvoiceLog> InvoiceLog { get; set; }
        /// <summary>
        /// جدول پرداخت های ثبت شده فاکتور
        /// </summary>
        public DbSet<InvoiceTransaction> InvoiceTransaction { get; set; }
        #endregion

        #region Support
        #region Enum
        /// <summary>
        /// جدول وضعیت دسته بندی سوالات متداول
        /// </summary>
        public DbSet<FaqCategoryStatus> FaqCategoryStatus { get; set; }
        /// <summary>
        /// جدول وضعیت سوالات متداول
        /// </summary>
        public DbSet<FaqStatus> FaqStatus { get; set; }
        /// <summary>
        /// جدول وضعیت پیام های داخل صندوق پیام
        /// </summary>
        public DbSet<MessageBoxStatus> MessageBoxStatus { get; set; }
        /// <summary>
        /// جدول نوع پیام
        /// </summary>
        public DbSet<MessageType> MessageType { get; set; }
        /// <summary>
        /// جدول وضعیت ایمیل خبرنامه
        /// </summary>
        public DbSet<NewsLetterStatus> NewsLetterStatus { get; set; }
        /// <summary>
        /// جدول نوع پیام های تیکت
        /// </summary>
        public DbSet<TicketMessageType> TicketMessageType { get; set; }
        /// <summary>
        /// جدول اولویت تیکت
        /// </summary>
        public DbSet<TicketPriority> TicketPriority { get; set; }
        /// <summary>
        /// جدول وضعیت تیکت ها
        /// </summary>
        public DbSet<TicketStatus> TicketStatus { get; set; }
        #endregion

        /// <summary>
        /// جدول سوالات متداول
        /// </summary>
        public DbSet<Faq> Faq { get; set; }
        /// <summary>
        /// جدول دسته بندی سوالات متداول
        /// </summary>
        public DbSet<FaqCategory> FaqCategory { get; set; }
        /// <summary>
        /// جدول صندوق پیام ها
        /// </summary>
        public DbSet<MessageBox> MessageBox { get; set; }
        /// <summary>
        /// جدول خبرنامه
        /// </summary>
        public DbSet<NewsLetter> NewsLetter { get; set; }
        /// <summary>
        /// جدول تیکت پشتیبانی
        /// </summary>
        public DbSet<Ticket> Ticket { get; set; }
        /// <summary>
        /// جدول پیام های تیکت
        /// </summary>
        public DbSet<TicketMessage> TicketMessage { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder model)
        {
            #region Account
            model.Entity<Group>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Group>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Group>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserNotification>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserNotification>().HasRequired(x => x.Notification).WithMany().HasForeignKey(x => x.NotificationId).WillCascadeOnDelete(false);
            model.Entity<UserNotification>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<UserNotification>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<UserNotification>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Notification>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Notification>().HasRequired(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<Notification>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Notification>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Notification>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Token>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Token>().HasOptional(x => x.DeviceType).WithMany().HasForeignKey(x => x.DeviceTypeId).WillCascadeOnDelete(false);
            model.Entity<Token>().HasOptional(x => x.OperatingSystem).WithMany().HasForeignKey(x => x.OperatingSystemId).WillCascadeOnDelete(false);
            model.Entity<Token>().HasOptional(x => x.Browser).WithMany().HasForeignKey(x => x.BrowserId).WillCascadeOnDelete(false);

            model.Entity<User>().HasOptional(x => x.Sex).WithMany().HasForeignKey(x => x.SexId).WillCascadeOnDelete(false);
            model.Entity<User>().HasRequired(x => x.UserType).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<User>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<User>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<User>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserAddress>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.Province).WithMany().HasForeignKey(x => x.ProvinceId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.City).WithMany().HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<UserAddress>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserBalanceLog>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserBalanceLog>().HasRequired(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<UserBalanceLog>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            model.Entity<UserFavorite>().HasRequired(x => x.User).WithMany(x => x.UserFavoriteList).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserFavorite>().HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<UserFavorite>().HasOptional(x => x.Folder).WithMany().HasForeignKey(x => x.FolderId).WillCascadeOnDelete(false);
            model.Entity<UserFavorite>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<UserFavorite>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<UserFavorite>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserFavoriteFolder>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserFavoriteFolder>().HasOptional(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);
            model.Entity<UserFavoriteFolder>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<UserFavoriteFolder>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<UserFavoriteFolder>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserGroup>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserGroup>().HasRequired(x => x.Group).WithMany(x => x.UserGroupList).HasForeignKey(x => x.GroupId).WillCascadeOnDelete(false);
            model.Entity<UserGroup>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<UserGroup>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<UserVerifyToken>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<UserVerifyToken>().HasRequired(x => x.TokenType).WithMany().HasForeignKey(x => x.TokenTypeId).WillCascadeOnDelete(false);
            #endregion

            #region Blog
            model.Entity<Article>().HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).WillCascadeOnDelete(false);
            model.Entity<Article>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Article>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Article>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<ArticleFile>().HasRequired(x => x.Article).WithMany().HasForeignKey(x => x.ArticleId).WillCascadeOnDelete(false);
            model.Entity<ArticleFile>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<ArticleFile>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ArticleFile>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);


            model.Entity<News>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<News>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<News>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<NewsTranslation>().HasRequired(x => x.News).WithMany(x=>x.TranslationList).HasForeignKey(x => x.NewsId).WillCascadeOnDelete(false);


            #endregion

            #region Core
            model.Entity<BaseInformation>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Branch>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Branch>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Branch>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Location>().HasOptional(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);
            model.Entity<Location>().HasRequired(x => x.LocationType).WithMany().HasForeignKey(x => x.LocationTypeId).WillCascadeOnDelete(false);



            model.Entity<Slider>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Slider>().HasRequired(x => x.SliderType).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<Slider>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Slider>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<SliderContent>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<SliderContent>().HasRequired(x => x.Slider).WithMany(x => x.SliderContentList).HasForeignKey(x => x.SliderId).WillCascadeOnDelete(false);
            model.Entity<SliderContent>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<SliderContent>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<SliderTranslation>().HasRequired(x => x.Slider).WithMany(x=>x.TranslationList).HasForeignKey(x => x.SliderId).WillCascadeOnDelete(false);

            #endregion

            #region Data
            model.Entity<AboutUs>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<AboutUs>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<AboutUs>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<AboutUsTranslation>().HasRequired(x => x.AboutUs).WithMany(x => x.TranslationList).HasForeignKey(x => x.AboutUsId).WillCascadeOnDelete(false);

            model.Entity<Export>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Export>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Export>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<ExportTranslation>().HasRequired(x => x.Export).WithMany(x => x.TranslationList).HasForeignKey(x => x.ExportId).WillCascadeOnDelete(false);

            model.Entity<ContactUs>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<ContactUs>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ContactUs>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<ContactUsTranslation>().HasRequired(x => x.ContactUs).WithMany(x => x.TranslationList).HasForeignKey(x => x.ContactUsId).WillCascadeOnDelete(false);


            model.Entity<SpecialProject>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<SpecialProject>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<SpecialProject>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<SpecialProjectTranslation>().HasRequired(x => x.SpecialProject).WithMany(x => x.TranslationList).HasForeignKey(x => x.SpecialProjectId).WillCascadeOnDelete(false);

            model.Entity<SpecialProjectFile>().HasRequired(x => x.SpecialProject).WithMany(x => x.SpecialProjectFileList).HasForeignKey(x => x.SpecialProjectId).WillCascadeOnDelete(false);
            model.Entity<SpecialProjectFile>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<SpecialProjectFile>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<SpecialProjectFile>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<SpecialProjectFileTranslation>().HasRequired(x => x.SpecialProjectFile).WithMany(x => x.TranslationList).HasForeignKey(x => x.SpecialProjectFileId).WillCascadeOnDelete(false);

            model.Entity<Brand>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Brand>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Brand>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<BrandTranslation>().HasRequired(x => x.Brand).WithMany(x => x.TranslationList).HasForeignKey(x => x.BrandId).WillCascadeOnDelete(false);
            model.Entity<BrandTranslation>().HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);

            model.Entity<Category>().HasOptional(x => x.Parent).WithMany(x => x.ChildList).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);
            model.Entity<Category>().HasRequired(x => x.CategoryType).WithMany().HasForeignKey(x => x.CategoryTypeId).WillCascadeOnDelete(false);
            model.Entity<Category>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Category>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Category>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<CategoryTranslation>().HasRequired(x => x.Category).WithMany(x => x.TranslationList).HasForeignKey(x => x.CategoryId).WillCascadeOnDelete(false);
            model.Entity<CategoryTranslation>().HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);

            model.Entity<CategoryFeature>().HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeature>().HasRequired(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeature>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeature>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeature>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<CategoryFeatureValue>().HasRequired(x => x.CategoryFeature).WithMany(x => x.ValueList).HasForeignKey(x => x.CategoryFeatureId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeatureValue>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<CategoryFeatureValue>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Product>().HasRequired(x => x.Category).WithMany(x => x.ProductList).HasForeignKey(x => x.CategoryId).WillCascadeOnDelete(false);
            model.Entity<Product>().HasOptional(x => x.CategoryFeature).WithMany().HasForeignKey(x => x.CategoryFeatureId).WillCascadeOnDelete(false);
            model.Entity<Product>().HasRequired(x => x.Brand).WithMany().HasForeignKey(x => x.BrandId).WillCascadeOnDelete(false);
            model.Entity<Product>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Product>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Product>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            model.Entity<ProductTranslation>().HasRequired(x => x.Product).WithMany(x => x.TranslationList).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<ProductTranslation>().HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);

            model.Entity<ProductFeature>().HasRequired(x => x.Product).WithMany(x => x.ProductFeatureList).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<ProductFeature>().HasRequired(x => x.CategoryFeatureValue).WithMany().HasForeignKey(x => x.CategoryFeatureValueId).WillCascadeOnDelete(false);
            model.Entity<ProductFeature>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ProductFeature>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<ProductFilter>().HasRequired(x => x.Category).WithMany(x => x.ProductFilterList).HasForeignKey(x => x.CategoryId).WillCascadeOnDelete(false);
            model.Entity<ProductFilter>().HasRequired(x => x.FilterType).WithMany().HasForeignKey(x => x.FilterTypeId).WillCascadeOnDelete(false);
            model.Entity<ProductFilter>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<ProductFilter>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ProductFilter>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<ProductFilterData>().HasRequired(x => x.Product).WithMany(x => x.ProductFilterDataList).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterData>().HasRequired(x => x.ProductFilter).WithMany().HasForeignKey(x => x.ProductFilterId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterData>().HasOptional(x => x.ProductFilterValue).WithMany().HasForeignKey(x => x.ProductFilterValueId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterData>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterData>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<ProductFilterValue>().HasRequired(x => x.ProductFilter).WithMany(x => x.ValueList).HasForeignKey(x => x.ProductFilterId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterValue>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterValue>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ProductFilterValue>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<ProductFile>().HasRequired(x => x.Product).WithMany(x => x.ProductFileList).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<ProductFile>().HasOptional(x => x.ProductFeature).WithMany().HasForeignKey(x => x.ProductFeatureId).WillCascadeOnDelete(false);
            model.Entity<ProductFile>().HasRequired(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<ProductFile>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<ProductFile>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<ProductFile>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Service>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Service>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Service>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            #endregion

            #region Financial
            model.Entity<Cart>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Cart>().HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<Cart>().HasOptional(x => x.ProductFeature).WithMany().HasForeignKey(x => x.ProductFeatureId).WillCascadeOnDelete(false);
            model.Entity<Cart>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Cart>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Coupon>().HasRequired(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).WillCascadeOnDelete(false);
            model.Entity<Coupon>().HasOptional(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Coupon>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Coupon>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Coupon>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Inquiry>().HasOptional(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Inquiry>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Inquiry>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Inquiry>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<InquiryCart>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<InquiryCart>().HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<InquiryCart>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<InquiryCart>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<InquiryItem>().HasRequired(x => x.Inquiry).WithMany(x => x.InquiryItemList).HasForeignKey(x => x.InquiryId).WillCascadeOnDelete(false);
            model.Entity<InquiryItem>().HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<InquiryItem>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<InquiryItem>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<Invoice>().HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.InvoiceStatus).WithMany().HasForeignKey(x => x.InvoiceStatusId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.PaymentType).WithMany().HasForeignKey(x => x.PaymentTypeId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.DeliveryType).WithMany().HasForeignKey(x => x.DeliveryTypeId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.Address).WithMany().HasForeignKey(x => x.AddressId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasOptional(x => x.Coupon).WithMany().HasForeignKey(x => x.CouponId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Invoice>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<InvoiceDetail>().HasRequired(x => x.Invoice).WithMany(x => x.DetailList).HasForeignKey(x => x.InvoiceId).WillCascadeOnDelete(false);
            model.Entity<InvoiceDetail>().HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
            model.Entity<InvoiceDetail>().HasOptional(x => x.ProductFeature).WithMany().HasForeignKey(x => x.ProductFeatureId).WillCascadeOnDelete(false);
            model.Entity<InvoiceDetail>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<InvoiceDetail>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<InvoiceDetail>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<InvoiceLog>().HasRequired(x => x.Invoice).WithMany(x => x.InvoiceLogList).HasForeignKey(x => x.InvoiceId).WillCascadeOnDelete(false);
            model.Entity<InvoiceLog>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<InvoiceLog>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            model.Entity<InvoiceTransaction>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<InvoiceTransaction>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);
            #endregion

            #region Support
            model.Entity<Faq>().HasRequired(x => x.FaqCategory).WithMany(x => x.FaqList).HasForeignKey(x => x.FaqCategoryId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<FaqCategory>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<FaqCategory>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<FaqCategory>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<MessageBox>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<MessageBox>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<MessageBox>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<NewsLetter>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<NewsLetter>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            model.Entity<Ticket>().HasRequired(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.Priority).WithMany().HasForeignKey(x => x.PriorityId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<TicketMessage>().HasRequired(x => x.Ticket).WithMany().HasForeignKey(x => x.TicketId).WillCascadeOnDelete(false);
            model.Entity<TicketMessage>().HasRequired(x => x.TicketMessageType).WithMany().HasForeignKey(x => x.TicketMessageTypeId).WillCascadeOnDelete(false);
            model.Entity<TicketMessage>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            #endregion

            #region Support
            model.Entity<Faq>().HasRequired(x => x.FaqCategory).WithMany(x => x.FaqList).HasForeignKey(x => x.FaqCategoryId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Faq>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<FaqCategory>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<FaqCategory>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<FaqCategory>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<MessageBox>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<MessageBox>().HasRequired(x => x.MessageType).WithMany().HasForeignKey(x => x.MessageTypeId).WillCascadeOnDelete(false);
            model.Entity<MessageBox>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<MessageBox>().HasOptional(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<NewsLetter>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<NewsLetter>().HasOptional(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            model.Entity<Ticket>().HasRequired(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.Priority).WithMany().HasForeignKey(x => x.PriorityId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);
            model.Entity<Ticket>().HasRequired(x => x.ModifyUser).WithMany().HasForeignKey(x => x.ModifyUserId).WillCascadeOnDelete(false);

            model.Entity<TicketMessage>().HasRequired(x => x.Ticket).WithMany().HasForeignKey(x => x.TicketId).WillCascadeOnDelete(false);
            model.Entity<TicketMessage>().HasRequired(x => x.TicketMessageType).WithMany().HasForeignKey(x => x.TicketMessageTypeId).WillCascadeOnDelete(false);
            model.Entity<TicketMessage>().HasRequired(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).WillCascadeOnDelete(false);

            #endregion

            base.OnModelCreating(model);
        }

    }
}
