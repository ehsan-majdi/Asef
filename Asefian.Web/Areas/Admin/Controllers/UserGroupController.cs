using Asefian.Common.Security;
using Asefian.Model.Context.Account;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.ViewModel.Account;
using Asefian.Model.ViewModel.Data;
using Asefian.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر صفحات گروه های کاربری
    /// </summary>
    [Authorize(Roles = "admin, group")]
    public class UserGroupController : BaseController
    {
        /// <summary>
        /// صفحه لیست گروه های کاربری
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult List()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن گروه های کاربری
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "کاربر جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش گروه کاربری
        /// </summary>
        /// <param name="id">ردیف گروه مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش کاربر";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دسترسی های گروه کاربری گروه کاربری
        /// </summary>
        /// <param name="id">ردیف گروه مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Access(int id)
        {
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست گروه های کاربری
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        public JsonResult Search(SearchGroupViewModel options)
        {
            try
            {
                var query = _context.Group.Where(x => x.StatusId != GroupStatus.Deleted.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Title.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchGroupViewModel()
                {
                    id = x.Id,
                    title = x.Title,
                    order = x.Order,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle,
                }).ToList();

                return SuccessSearch(data, options.page, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات گروه های کاربری
        /// </summary>
        /// <param name="id">ردیف گروه های کاربری مورد نظر</param>
        /// <returns>نتیجه خواندن گروه های کاربری</returns>
        [HttpGet]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.Group.Where(x => x.StatusId != GroupStatus.Deleted.Id && x.Id == id).Select(x => new GroupViewModel()
                {
                    id = x.Id,
                    title = x.Title,
                    order = x.Order,
                    permission = x.Permission,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).Single();

                entity.permissionList = Permission.GetPermissionCodeList(entity.permission);

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        [HttpPost]
        public JsonResult LoadAssign(int id)
        {
            try
            {
                var entity = _context.UserGroup.Where(x => x.UserId == id).Select(x => new GroupViewModel()
                {
                    id = x.GroupId,

                }).SingleOrDefault();
                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت نام گروه های کاربری
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات گروه های کاربری جهت ثبت</param>
        /// <returns>نتیجه ثبت گروه های کاربری به صورت آبجکت پاسخ استاندارد</returns>
        [HttpPost]
        public JsonResult Save(GroupViewModel model)
        {
            var currentUser = GetAuthenticatedUser();
            try
            {
                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن نام گروه های کاربری اجباری است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.Group.Single(x => x.StatusId != GroupStatus.Deleted.Id && x.Id == model.id);
                    entity.Title = model.title.ToStandardPersian();
                    entity.Order = model.order;
                    entity.StatusId = model.statusId;
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();

                    return Success("اطلاعات گروه های کاربری با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new Group()
                    {
                        Title = model.title.ToStandardPersian(),
                        Order = model.order,
                        StatusId = model.statusId,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.Group.Add(entity);
                    _context.SaveChanges();

                    return Success("گروه های کاربری با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAssign(AssignGroupViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserGroup.Where(x => x.UserId == model.userId).SingleOrDefault();
                if(entity != null)
                {
                    entity.GroupId = model.id;
                }
                else
                {
                    var item = new UserGroup()
                    {
                        UserId = model.userId,
                        GroupId = model.id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.UserGroup.Add(item);
                }
                _context.SaveChanges();
                return Success();
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }


        /// <summary>
        /// حذف گروه های کاربری
        /// </summary>
        /// <param name="id">ردیف گروه های کاربری</param>
        /// <returns>نتیجه حذف گروه های کاربری</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.Group.Single(x => x.StatusId != GroupStatus.Deleted.Id && x.Id == id);
                entity.StatusId = GroupStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("گروه های کاربری با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست دسترسی های گروه های کاربری
        /// </summary>
        /// <param name="id">ردیف گروه کاربری</param>
        /// <returns>نتیجه درخواست لیست کد دسترسی ها</returns>
        [HttpGet]
        public JsonResult AccessList(int id)
        {
            try
            {
                var item = _context.Group.Single(x => x.StatusId != GroupStatus.Deleted.Id && x.Id == id);
                var permissionList = Permission.GetPermissionCodeList(item.Permission);
                var result = new PermissionList()
                {
                    id = id,
                    permission = permissionList
                };
                return Success(result);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره دسترسی های گروه کاربری
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAccessList(PermissionList model)
        {
            try
            {
                var item = _context.Group.Single(x => x.StatusId != GroupStatus.Deleted.Id && x.Id == model.id);
                item.Permission = Permission.GetPermissionCode(model.permission);
                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        public ActionResult AssignGroup(int id)
        {
            ViewBag.Id = id;
            return BaseView();
        }

        [HttpPost]
        public JsonResult AssignGroup(AssignGroupViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                foreach (var item in model.groupId)
                {
                    var entity = new UserGroup()
                    {
                        UserId = model.userId,
                        GroupId = item,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp(),
                    };
                    _context.UserGroup.Add(entity);
                }
                _context.SaveChanges();

                return Success("اطلاعات با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}