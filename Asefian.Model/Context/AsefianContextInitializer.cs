using Asefian.Common.Security;
using Asefian.Model.Context.Account;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.Context.Blog.Enum;
using Asefian.Model.Context.Core.Enum;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Context.Financial.Enum;
using Asefian.Model.Context.Support.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.Context
{
    /// <summary>
    /// سازنده دیتابیس اصلی برنامه و تغذیه داده های enum
    /// </summary>
    public class AsefianContextInitializer : DropCreateDatabaseIfModelChanges<AsefianContext>
    {
        /// <summary>
        /// درج مقادیر اولیه در دیتابیس به هنگام ساخت دیتابیس
        /// </summary>
        /// <param name="context">شی دیتابیس اصلی برنامه</param>
        protected override void Seed(AsefianContext context)
        {
            foreach (var item in Language.GetList())
            {
                context.Language.Add(item);
            }
            #region Account
            foreach (var item in UserType.GetList())
            {
                context.UserType.Add(item);
            }
            foreach (var item in BalanceType.GetList())
            {
                context.BalanceType.Add(item);
            }
            foreach (var item in Browser.GetList())
            {
                context.Browser.Add(item);
            }
            foreach (var item in DeviceType.GetList())
            {
                context.DeviceType.Add(item);
            }
            foreach (var item in GroupStatus.GetList())
            {
                context.GroupStatus.Add(item);
            }
            foreach (var item in NotificationStatus.GetList())
            {
                context.NotificationStatus.Add(item);
            }
            foreach (var item in NotificationType.GetList())
            {
                context.NotificationType.Add(item);
            }
            foreach (var item in Account.Enum.OperatingSystem.GetList())
            {
                context.OperatingSystem.Add(item);
            }
            foreach (var item in Sex.GetList())
            {
                context.Sex.Add(item);
            }
            foreach (var item in TokenType.GetList())
            {
                context.TokenType.Add(item);
            }
            foreach (var item in UserAddressStatus.GetList())
            {
                context.UserAddressStatus.Add(item);
            }
            foreach (var item in UserFavoriteFolderStatus.GetList())
            {
                context.UserFavoriteFolderStatus.Add(item);
            }
            foreach (var item in UserFavoriteStatus.GetList())
            {
                context.UserFavoriteStatus.Add(item);
            }
            foreach (var item in UserStatus.GetList())
            {
                context.UserStatus.Add(item);
            }
            #endregion

            #region Blog
           
            foreach (var item in ArticleFileStatus.GetList())
            {
                context.ArticleFileStatus.Add(item);
            }
           
            foreach (var item in ArticleStatus.GetList())
            {
                context.ArticleStatus.Add(item);
            }
           
            foreach (var item in NewsStatus.GetList())
            {
                context.NewsStatus.Add(item);
            }
            #endregion

            #region Core
            foreach (var item in BranchStatus.GetList())
            {
                context.BranchStatus.Add(item);
            }
            foreach (var item in LocationType.GetList())
            {
                context.LocationType.Add(item);
            }
            foreach (var item in SliderContentStatus.GetList())
            {
                context.SliderContentStatus.Add(item);
            }
            foreach (var item in SliderStatus.GetList())
            {
                context.SliderStatus.Add(item);
            }
            foreach (var item in SliderType.GetList())
            {
                context.SliderType.Add(item);
            }
            #endregion

            #region Data
            foreach (var item in ExportStatus.GetList())
            {
                context.ExportStatus.Add(item);
            }
            foreach (var item in ContactUsStatus.GetList())
            {
                context.ContactUsStatus.Add(item);
            }
            foreach (var item in SpecialProjectStatus.GetList())
            {
                context.SpecialProjectStatus.Add(item);
            }
            foreach (var item in SpecialProjectFileStatus.GetList())
            {
                context.SpecialProjectFileStatus.Add(item);
            }
            foreach (var item in BrandStatus.GetList())
            {
                context.BrandStatus.Add(item);
            }
            foreach (var item in CategoryFeatureStatus.GetList())
            {
                context.CategoryFeatureStatus.Add(item);
            }
            foreach (var item in CategoryFeatureType.GetList())
            {
                context.CategoryFeatureType.Add(item);
            }
            foreach (var item in CategoryStatus.GetList())
            {
                context.CategoryStatus.Add(item);
            }
            foreach (var item in CategoryType.GetList())
            {
                context.CategoryType.Add(item);
            }
            foreach (var item in DownloadCenterStatus.GetList())
            {
                context.DownloadCenterStatus.Add(item);
            }
            foreach (var item in ProductFileStatus.GetList())
            {
                context.ProductFileStatus.Add(item);
            }
            foreach (var item in ProductFileType.GetList())
            {
                context.ProductFileType.Add(item);
            }
            foreach (var item in ProductFilterStatus.GetList())
            {
                context.ProductFilterStatus.Add(item);
            }
            foreach (var item in ProductFilterType.GetList())
            {
                context.ProductFilterType.Add(item);
            }
            foreach (var item in ProductFilterValueStatus.GetList())
            {
                context.ProductFilterValueStatus.Add(item);
            }
            foreach (var item in ProductStatus.GetList())
            {
                context.ProductStatus.Add(item);
            }
            foreach (var item in ServiceStatus.GetList())
            {
                context.ServiceStatus.Add(item);
            }
            #endregion

            #region Financial
            foreach (var item in CouponStatus.GetList())
            {
                context.CouponStatus.Add(item);
            }
            foreach (var item in CouponType.GetList())
            {
                context.CouponType.Add(item);
            }
            foreach (var item in DeliveryType.GetList())
            {
                context.DeliveryType.Add(item);
            }
            foreach (var item in InquiryStatus.GetList())
            {
                context.InquiryStatus.Add(item);
            }
            foreach (var item in InvoiceDetailStatus.GetList())
            {
                context.InvoiceDetailStatus.Add(item);
            }
            foreach (var item in InvoiceStatus.GetList())
            {
                context.InvoiceStatus.Add(item);
            }
            foreach (var item in PaymentType.GetList())
            {
                context.PaymentType.Add(item);
            }
            #endregion

            #region Support
            foreach (var item in FaqCategoryStatus.GetList())
            {
                context.FaqCategoryStatus.Add(item);
            }
            foreach (var item in FaqStatus.GetList())
            {
                context.FaqStatus.Add(item);
            }
            foreach (var item in MessageBoxStatus.GetList())
            {
                context.MessageBoxStatus.Add(item);
            }
            foreach (var item in NewsLetterStatus.GetList())
            {
                context.NewsLetterStatus.Add(item);
            }
            foreach (var item in TicketMessageType.GetList())
            {
                context.TicketMessageType.Add(item);
            }
            foreach (var item in TicketPriority.GetList())
            {
                context.TicketPriority.Add(item);
            }
            foreach (var item in TicketStatus.GetList())
            {
                context.TicketStatus.Add(item);
            }
            foreach (var item in MessageType.GetList())
            {
                context.MessageType.Add(item);
            }
            #endregion

            var admin = new User()
            {
                FirstName = "مدیر",
                LastName = "سیستم",
                MobileNumber = "09122424519",
                MobileNumberValid = true,
                Email = "esmaiel.abedie@gmail.com",
                EmailValid = true,
                SexId = Sex.Male.Id,
                TypeId = UserType.Insider.Id,
                Password = PasswordUtility.Encrypt("alialiali"),
                StatusId = UserStatus.Active.Id,
                Permission = 1,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                CreateIp = "::1",
                ModifyIp = "::1"
            };
            context.User.Add(admin);
        }
    }
}
