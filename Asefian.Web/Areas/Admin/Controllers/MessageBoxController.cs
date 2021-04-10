using Asefian.Model.ViewModel.Support;
using Asefian.Web.Controllers;
using Asefian.Common.Utility;
using Asefian.Model.Context.Support.Enum;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر صندوق پیام ها
    /// </summary>
    [Authorize(Roles = "admin, message")]
    public class MessageBoxController : BaseController
    {
        /// <summary>
        /// صفحه صندوق پیام ها
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult ContactUsList()
        {
            return BaseView();
        }

        [HttpGet]
        public ActionResult DesignerList()
        {
            return BaseView();
        }

        [HttpGet]
        public ActionResult WorkWithUsList()
        {
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست پیام ها
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchMessageBoxViewModel options)
        {
            try
            {
                var query = _context.MessageBox.Where(x => x.StatusId != MessageBoxStatus.Deleted.Id);
                if(options.typeId > 0 && options.typeId == 3)
                {
                    query = query.Where(x => x.MessageTypeId == MessageType.ContactUs.Id);
                }
                if (options.typeId > 0 && options.typeId == 2)
                {
                    query = query.Where(x => x.MessageTypeId == MessageType.WorkWithUs.Id);
                }
                if (options.typeId > 0 && options.typeId == 1)
                {
                    query = query.Where(x => x.MessageTypeId == MessageType.Designer.Id);
                }
                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.FullName.Contains(word) || x.Email.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.CreateDate)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new MessageBoxViewModel()
                {
                    id = x.Id,
                    fullName = x.FullName,
                    mobileNumber = x.MobileNumber,
                    email = x.Email,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                    createDate = x.CreateDate
                }).ToList();

                data.ForEach(x => x.PersianCreateDate = DateUtility.GetPersianDateTime(x.createDate));

                return SuccessSearch(data, options.page + 1, options.count, count, "createDate", "asc");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن پیام
        /// </summary>
        /// <param name="id">ردیف پیام</param>
        /// <returns>نتیجه ثبت اطلاعات برای خواندن پیام</returns>
        [HttpGet]
        public JsonResult GetMessage(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.MessageBox.Where(x => x.StatusId != MessageBoxStatus.Deleted.Id && x.Id == id).Select(x => new MessageBoxViewModel()
                {
                    id = x.Id,
                    fullName = x.FullName,
                    mobileNumber = x.MobileNumber,
                    email = x.Email,
                    statusId = x.StatusId,
                    text = x.Text,
                    statusTitle = x.Status.PersianTitle
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن پیام
        /// </summary>
        /// <param name="id">ردیف پیام</param>
        /// <returns>نتیجه ثبت اطلاعات برای خواندن پیام</returns>
        [HttpPost]
        public JsonResult Read(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.MessageBox.Single(x => x.StatusId != MessageBoxStatus.Deleted.Id && x.Id == id);

                entity.StatusId = MessageBoxStatus.Read.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();
                _context.SaveChanges();

                return Success("اطلاعات پیام با موفقیت به روز رسانی شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف پیام
        /// </summary>
        /// <param name="id">ردیف پیام</param>
        /// <returns>نتیجه حذف پیام</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.MessageBox.Single(x => x.StatusId != MessageBoxStatus.Deleted.Id && x.Id == id);

                entity.StatusId = MessageBoxStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();
                _context.SaveChanges();

                return Success("پیام با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}