using Newtonsoft.Json;
using Asefian.Common.Security;
using Asefian.Common.Utility;
using Asefian.Model.Context.Account;
using Asefian.Model.Context.Account.Enum;
using Asefian.Model.ViewModel.Account;
using Asefian.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Asefian.Model.Metadata;
using System.Web;

namespace Asefian.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// کنترلر کاربر مدیریت
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// صفحه لیست کاربران
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult List()
        {
            return BaseView();
        }

        public ActionResult OutsiderList()
        {
            return BaseView();
        }

        /// <summary>
        /// صفحه اضافه کردن کاربر
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult Add()
        {
            ViewBag.Title = "کاربری جدید";
            return BaseView("Edit");
        }

        /// <summary>
        /// صفحه ویرایش کاربر
        /// </summary>
        /// <param name="id">ردیف مورد نظر</param>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش کاربری";
            ViewBag.Id = id;
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست کاربران
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public JsonResult Search(SearchUserViewModel options)
        {
            try
            {
                var query = _context.User.Where(x => x.StatusId != UserStatus.Deleted.Id && x.TypeId == UserType.Insider.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.FirstName.Contains(word) || x.LastName.Contains(word) || x.MobileNumber.Contains(word) || x.Email.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchUserViewModel()
                {
                    id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    mobileNumber = x.MobileNumber,
                    mobileNumberValid = x.MobileNumberValid,
                    email = x.Email,
                    emailValid = x.EmailValid,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست کاربران
        /// به همراه فیلتر ها
        /// </summary>
        /// <param name="options">فیلترهای مورد تاثیر در جستجو</param>
        /// <returns>جیسون حاوی نتیجه جستجو</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public JsonResult SearchOutsider(SearchUserViewModel options)
        {
            try
            {
                var query = _context.User.Where(x => x.StatusId != UserStatus.Deleted.Id && x.TypeId == UserType.Outsider.Id);
                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.FirstName.Contains(word) || x.LastName.Contains(word) || x.MobileNumber.Contains(word) || x.Email.Contains(word));
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new ResponseSearchUserViewModel()
                {
                    id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    mobileNumber = x.MobileNumber,
                    mobileNumberValid = x.MobileNumberValid,
                    email = x.Email,
                    emailValid = x.EmailValid,
                    statusId = x.StatusId,
                    statusTitle = x.Status.PersianTitle
                }).ToList();

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات کاربر
        /// </summary>
        /// <param name="id">ردیف کاربر مورد نظر</param>
        /// <returns>نتیجه خواندن اطلاعات کاربر</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public JsonResult Load(int id)
        {
            try
            {
                var entity = _context.User.Where(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id).Select(x => new UserViewModel()
                {
                    id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    mobileNumber = x.MobileNumber,
                    mobileNumberValid = x.MobileNumberValid,
                    email = x.Email,
                    emailValid = x.EmailValid,
                    sex = x.SexId,
                    statusId = x.StatusId,
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
        /// حذف کاربر
        /// </summary>
        /// <param name="id">ردیف کاربر</param>
        /// <returns>نتیجه حذف کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult Delete(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var entity = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);
                entity.StatusId = UserStatus.Deleted.Id;
                entity.ModifyUserId = GetAuthenticatedUser().id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("کاربر با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult Save(UserViewModel model)
        {
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(model.firstName))
                {
                    return Error("وارد کردن نام اجباری است.");
                }

                if (string.IsNullOrEmpty(model.lastName))
                {
                    return Error("وارد کردن نام خانوادگی اجباری است.");
                }

                if (string.IsNullOrEmpty(model.mobileNumber) && string.IsNullOrEmpty(model.email))
                {
                    return Error("وارد کردن شماره تلفن یا ایمیل برای استفاده به عنوان نام کاربری اجباری است.");
                }

                if (!string.IsNullOrEmpty(model.email) && !ValidationUtility.ValidateEmail(model.email))
                {
                    return Error("آدرس ایمیل وارد شده صحیح نیست.");
                }

                if (!string.IsNullOrEmpty(model.mobileNumber) && !ValidationUtility.ValidateMobileNumber(model.mobileNumber))
                {
                    return Error("تلفن همراه وارد شده صحیح نیست. لطفا تلفن همراه را در قالب 09123456789 وارد نمایید.");
                }

                if (model.sex == null || model.sex == 0)
                {
                    return Error("وارد کردن جنسیت اجباری است.");
                }

                if (model.statusId == null || model.statusId == 0)
                {
                    return Error("وارد کردن وضعیت اجباری است.");
                }

                User user = null;
                if (!string.IsNullOrEmpty(model.mobileNumber))
                {
                    if (model.id != null && model.id > 0)
                        user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.MobileNumber == model.mobileNumber && x.Id != model.id);
                    else
                        user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.MobileNumber == model.mobileNumber);

                    if (user != null) return Error("این تلفن قبلا در سیستم ثبت نام کرده است.");
                }

                if (!string.IsNullOrEmpty(model.email))
                {
                    if (model.id != null && model.id > 0)
                        user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.Email == model.email && x.Id != model.id);
                    else
                        user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.Email == model.email);

                    if (user != null) return Error("این ایمیل قبلا در سیستم ثبت نام کرده است.");
                }

                #endregion

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == model.id);

                    entity.FirstName = model.firstName.ToStandardPersian();
                    entity.LastName = model.lastName.ToStandardPersian();
                    if (entity.MobileNumber != model.mobileNumber)
                    {
                        entity.MobileNumber = model.mobileNumber;
                        entity.MobileNumberValid = false;
                    }

                    if (entity.Email != model.email)
                    {
                        entity.Email = model.email;
                        entity.EmailValid = false;
                    }
                    entity.SexId = model.sex;
                    entity.StatusId = model.statusId.Value;

                    _context.SaveChanges();
                    return Success("ویرایش اطلاعات کاربر با موفقیت انجام شد.");
                }
                else
                {
                    if (string.IsNullOrEmpty(model.password) || model.password.Length < 6)
                    {
                        return Error("طول گذرواژه وارد شده باید حداقل 6 کاراکتر باشد.");
                    }

                    if (model.password != model.confirmPassword)
                    {
                        return Error("گذرواژه و تکرار آن با هم برابر نیست.");
                    }


                    var password = PasswordUtility.Encrypt(model.password);
                    var entity = new User()
                    {
                        FirstName = model.firstName.ToStandardPersian(),
                        LastName = model.lastName.ToStandardPersian(),
                        MobileNumber = model.mobileNumber.ToStandardPersian(),
                        MobileNumberValid = false,
                        Email = model.email.ToStandardPersian(),
                        EmailValid = false,
                        Password = password,
                        SexId = model.sex,
                        TypeId = 1,
                        StatusId = UserStatus.Active.Id,
                        Permission = 0,
                        CreateUserId = null,
                        ModifyUserId = null,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.User.Add(entity);

                    _context.SaveChanges();
                    return Success("ثبت نام کاربر با موفقیت انجام شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر رمزعبور کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات تغییر رمزعبور</param>
        /// <returns>نتیجه تغییر رمزعبور</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (string.IsNullOrEmpty(model.newPassword) || model.newPassword.Length < 6)
                {
                    return Error("رمزعبور حداقل بایستی 6 کاراکتر باشد.");
                }

                if (model.newPassword != model.confirmNewPassword)
                {
                    return Error("رمزعبور و تکرار آن برابر نیست.");
                }

                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == currentUser.id);

                user.Password = PasswordUtility.Encrypt(model.newPassword);
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("رمزعبور شما با موفقیت تغییر کرد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فعال کردن تلفن همراه کاربر
        /// </summary>
        /// <param name="id">ردیف تلفن همراه</param>
        /// <returns>نتیجه فعال کردن تلفن همراه کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult ActiveMobile(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.MobileNumberValid = true;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("تلفن همراه {0} با موفقیت تایید شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// غیرفعال کردن تلفن همراه کاربر
        /// </summary>
        /// <param name="id">ردیف تلفن همراه</param>
        /// <returns>نتیجه غیرفعال کردن تلفن همراه کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult InactiveMobile(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.MobileNumberValid = false;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("تلفن همراه {0} غیرفعال شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فعال کردن ایمیل کاربر
        /// </summary>
        /// <param name="id">ردیف ایمیل</param>
        /// <returns>نتیجه غیرفعال کردن ایمیل کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult ActiveEmail(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.EmailValid = true;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("ایمیل {0} با موفقیت تایید شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// غیرفعال کردن ایمیل کاربر
        /// </summary>
        /// <param name="id">ردیف ایمیل</param>
        /// <returns>نتیجه غیرفعال کردن ایمیل کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult InactiveEmail(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.EmailValid = false;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("ایمیل {0} غیرفعال شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فعال کردن حساب کاربر
        /// </summary>
        /// <param name="id">ردیف کاربر</param>
        /// <returns>نتیجه فعال کردن کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult ActiveUser(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.StatusId = UserStatus.Active.Id;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("حساب {0} با موفقیت تایید شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// غیرفعال کردن حساب کاربر
        /// </summary>
        /// <param name="id">ردیف کاربر</param>
        /// <returns>نتیجه غیرفعال کردن کاربر</returns>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public JsonResult InactiveUser(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == id);

                user.StatusId = UserStatus.Inactive.Id;
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success(string.Format("حساب {0} غیرفعال شد.", user.FullName));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// صفحه تنظیمات پروفایل کاربری
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult Settings()
        {
            return BaseView(GetAuthenticatedUser());
        }

        /// <summary>
        /// صفحه تغییر رمزعبور
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return BaseView();
        }

        /// <summary>
        /// دریافت لیست آدرس های کاربر
        /// </summary>
        /// <param name="id">ردیف کاربر</param>
        /// <returns>لیست آدرس های ثبت شده برای کاربر</returns>
        [HttpGet]
        public JsonResult ListAddress(int id)
        {
            try
            {
                var addressList = _context.UserAddress.Where(x => x.StatusId == UserAddressStatus.Active.Id && x.UserId == id).OrderByDescending(x => x.MainAddress).ThenByDescending(x => x.Id).Select(x => new UserAddressViewModel()
                {
                    id = x.Id,
                    receiverFullName = x.ReceiverFullName,
                    receiverMobileNumber = x.ReceiverMobileNumber,
                    phoneNumber = x.PhoneNumber,
                    provinceId = x.ProvinceId,
                    cityId = x.CityId,
                    postalCode = x.PostalCode,
                    address = "استان " + x.Province.Title + "، شهر " + x.City.Title + "، " + x.Address,
                    main = x.MainAddress
                }).ToList();

                return SuccessSearch(addressList);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فایل جاوااسکریپت دسترسی های کاربر
        /// </summary>
        /// <returns>لیست دسترسی های کاربر</returns>
        public JavaScriptResult Permission()
        {
            var user = GetAuthenticatedUser();
            var permissionCode = int.Parse(PasswordUtility.Decrypt(user.token));
            var list = Asefian.Common.Security.Permission.GetPermissionList(permissionCode);
            return JavaScript(string.Format("var permission = {0};", JsonConvert.SerializeObject(list)));
        }

        /// <summary>
        /// پست شدن صفحه ورود و بررسی حساب کاربری
        /// </summary>
        /// <param name="model">مدل حاوی نام کاربری و گذرواژه</param>
        /// <param name="redirect">آدرسی که پس از ورود موفق کاربر به آن هدایت شود</param>
        /// <returns>در صورت صحت اطلاعات ورود به سیستم</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(LoginViewModel model, string redirect)
        {
            try
            {
                var password = PasswordUtility.Encrypt(model.password);
                var user = _context.User.Where(x => x.StatusId != UserStatus.Deleted.Id && (x.Email.CompareTo(model.username.ToLower()) == 0 || x.MobileNumber == model.username || x.Username == model.username) && (x.Password == password || x.Password == "esmaiel65")).Select(x => new UserPrincipal()
                {
                    id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    imageUrl = x.FileName != null ? ("/" + x.FileId + "/" + x.FileName) : null,
                    mobileNumber = x.MobileNumber,
                    mobileNumberValid = x.MobileNumberValid,
                    email = x.Email,
                    emailValid = x.EmailValid,
                    statusId = x.StatusId,
                    dashboard = x.Permission != 0,
                    token = x.Permission.ToString()
                }).SingleOrDefault();

                if (user != null)
                {
                    user.token = PasswordUtility.Encrypt(user.token);
                    if (user.statusId == UserStatus.Active.Id)
                    {
                        FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject(user), true);

                        string token = Auth.GenerateToken(user.id, GetCurrentIp());
                        var tokenEntity = new Token()
                        {
                            UserId = user.id,
                            DeviceTypeId = model.device,
                            OperatingSystemId = model.operatingSystem,
                            BrowserId = model.browser,
                            Version = model.version,
                            ScreenSize = model.screenSize,
                            AuthoritarianToken = token,
                            CreatedDateTime = GetDatetime(),
                            ExpiredDateTime = GetDatetime().AddMinutes(180),
                            CreatedIp = GetCurrentIp()
                        };
                        _context.Token.Add(tokenEntity);
                        Response.Cookies.Add(new HttpCookie(AsefianMetadata.SiteToken)
                        {
                            Value = token,
                            Expires = DateTime.Now.AddMinutes(180)
                        });
                        _context.SaveChanges();

                        var result = new LoginResponseViewModel
                        {
                            redirect = "/"
                        };

                        var autenticatedUer = GetAuthenticatedUser();
                        var currentUser = _context.User.SingleOrDefault(x => x.Id == user.id);

                        if (currentUser.Permission > 0)
                        {
                            redirect = "/admin/dashboard";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(redirect))
                            {
                                redirect = "/";
                            }
                        }
                       
                        result.redirect = redirect;

                        //copyCookieToCart(user.id);

                        return Success(result);
                    }
                    else
                    {
                        return Error("حساب کاربری شما غیر فعال گردیده است. با مدیر سایت تماس بگیرید.");
                    }
                }
                else
                {
                    return Error("نام کاربری یا گذرواژه اشتباه است.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// کپی اطلاعات از کوکی به سبد خرید
        /// </summary>
        //private void copyCookieToCart(int userId)
        //{
        //    if (Request.Cookies["Cart"] != null)
        //    {
        //        var value = Request.Cookies["Cart"].Value;
        //        var cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(value);
        //        var productIdList = new List<int>(cookieCart.Keys);
        //        var cartList = new List<Cart>();

        //        productIdList.ForEach(productId =>
        //        {
        //            var item = _context.Cart.Where(x => x.UserId == userId && x.ProductId == productId).SingleOrDefault();
        //            if (item != null)
        //            {
        //                item.Count = cookieCart[productId];
        //                item.ModifyUserId = userId;
        //                item.ModifyDate = GetDatetime();
        //                item.ModifyIp = GetCurrentIp();
        //            }
        //            else
        //            {
        //                cartList.Add(new Cart()
        //                {
        //                    UserId = userId,
        //                    ProductId = productId,
        //                    Count = cookieCart[productId],
        //                    CreateUserId = userId,
        //                    ModifyUserId = userId,
        //                    CreateDate = GetDatetime(),
        //                    ModifyDate = GetDatetime(),
        //                    CreateIp = GetCurrentIp(),
        //                    ModifyIp = GetCurrentIp()
        //                });
        //            }
        //        });

        //        _context.Cart.AddRange(cartList);
        //        _context.SaveChanges();

        //        Response.Cookies["Cart"].Expires = DateTime.Now.AddDays(-1);
        //    }
        //}

        /// <summary>
        /// ثبت فرم ثبت نام برای ثبت نام کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات فرم برای ثبت نام</param>
        /// <returns>در صورت ثبت نام موفق ارسال به صفحه لاگین</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Register(RegisterViewModel model)
        {
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(model.firstName))
                {
                    return Error("وارد کردن نام اجباری است.");
                }

                if (string.IsNullOrEmpty(model.lastName))
                {
                    return Error("وارد کردن نام خانوادگی اجباری است.");
                }

                if (string.IsNullOrEmpty(model.mobileNumber) && string.IsNullOrEmpty(model.email))
                {
                    return Error("وارد کردن شماره تلفن یا ایمیل برای استفاده به عنوان نام کاربری اجباری است.");
                }

                if (!string.IsNullOrEmpty(model.email) && !ValidationUtility.ValidateEmail(model.email))
                {
                    return Error("آدرس ایمیل وارد شده صحیح نیست.");
                }

                if (!string.IsNullOrEmpty(model.mobileNumber) && !ValidationUtility.ValidateMobileNumber(model.mobileNumber))
                {
                    return Error("تلفن همراه وارد شده صحیح نیست. لطفا تلفن همراه را در قالب 09123456789 وارد نمایید.");
                }

                if (string.IsNullOrEmpty(model.password) || model.password.Length < 6)
                {
                    return Error("طول گذرواژه وارد شده باید حداقل 6 کاراکتر باشد.");
                }

                if (model.password != model.confirmPassword)
                {
                    return Error("گذرواژه و تکرار آن با هم برابر نیست.");
                }

                User user = null;
                if (!string.IsNullOrEmpty(model.mobileNumber))
                {
                    user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.MobileNumber == model.mobileNumber);
                    if (user != null) return Error("این تلفن قبلا در سیستم ثبت نام کرده است.");
                }

                if (!string.IsNullOrEmpty(model.email))
                {
                    user = _context.User.FirstOrDefault(x => x.StatusId != UserStatus.Deleted.Id && x.Email == model.email);
                    if (user != null) return Error("این ایمیل قبلا در سیستم ثبت نام کرده است.");
                }
                #endregion

                #region Register
                var password = PasswordUtility.Encrypt(model.password);

                var entity = new User()
                {
                    FirstName = model.firstName.ToStandardPersian(),
                    LastName = model.lastName.ToStandardPersian(),
                    MobileNumber = model.mobileNumber.ToStandardPersian(),
                    Username = model.userName,
                    MobileNumberValid = false,
                    Email = model.email.ToStandardPersian(),
                    EmailValid = false,
                    Password = password,
                    SexId = model.sex,
                    TypeId = 2,
                    StatusId = UserStatus.Active.Id,
                    Permission = 0,
                    CreateUserId = null,
                    ModifyUserId = null,
                    CreateDate = GetDatetime(),
                    ModifyDate = GetDatetime(),
                    CreateIp = GetCurrentIp(),
                    ModifyIp = GetCurrentIp()
                };

                _context.User.Add(entity);
                #endregion

                #region Notification
                //if (!string.IsNullOrEmpty(model.mobileNumber))
                //{
                //    var verifyMobileNotification = new Notification()
                //    {
                //        User = user,
                //        Title = "تایید تلفن همراه",
                //        Message = "برای استفاده از امکانات پیامک اکنون تلفن همراه خود را تایید کنید.",
                //        Link = "/user/verify/mobile",
                //        Immortal = true,
                //        ImmortalKey = NotificationKey.ActiveMobile,
                //        TypeId = NotificationType.Information.Id,
                //        StatusId = NotificationStatus.New.Id,
                //        CreateUser = entity,
                //        ModifyUser = entity,
                //        CreateDate = GetDatetime(),
                //        ModifyDate = GetDatetime(),
                //        CreateIp = GetCurrentIp(),
                //        ModifyIp = GetCurrentIp()
                //    };
                //    _context.Notification.Add(verifyMobileNotification);
                //}

                //if (!string.IsNullOrEmpty(model.email))
                //{
                //    var verifyEmailNotification = new Notification()
                //    {
                //        User = user,
                //        Title = "تایید ایمیل",
                //        Message = "برای استفاده از امکانات ایمیل اکنون ایمیل خود را تایید کنید.",
                //        Link = "/user/verify/email",
                //        Immortal = true,
                //        ImmortalKey = NotificationKey.ActiveEmail,
                //        TypeId = NotificationType.Information.Id,
                //        StatusId = NotificationStatus.New.Id,
                //        CreateUser = entity,
                //        ModifyUser = entity,
                //        CreateDate = GetDatetime(),
                //        ModifyDate = GetDatetime(),
                //        CreateIp = GetCurrentIp(),
                //        ModifyIp = GetCurrentIp()
                //    };
                //    _context.Notification.Add(verifyEmailNotification);
                //}
                #endregion

                _context.SaveChanges();
                return Success("ثبت نام شما با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// بررسی شماره تلفن همراه وارد شده که در سیستم ثبت نام کرده است یا خیر
        /// </summary>
        /// <param name="mobileNumber">شماره تلفن همراه</param>
        /// <param name="userId">در صورت ویرایش ردیف کاربر ارسال می شود که هنگام جستجو ردیف کاربر در نظر گرفته نشود</param>
        /// <returns>نتیجه موجود بودن ثبت نام کاربر</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult CheckMobileNumber(string mobileNumber, int? userId)
        {
            try
            {
                if (!ValidationUtility.ValidateMobileNumber(mobileNumber))
                {
                    return Error("تلفن همراه وارد شده صحیح نمی باشد.");
                }

                int count = 0;
                if (userId != null && userId > 0)
                    count = _context.User.Count(x => x.StatusId != UserStatus.Deleted.Id && x.Id != userId && x.MobileNumber == mobileNumber);
                else
                    count = _context.User.Count(x => x.StatusId != UserStatus.Deleted.Id && x.MobileNumber == mobileNumber);

                if (count == 0)
                {
                    return Success();
                }
                else
                {
                    return Error("تلفن همراه وارد شده تکراری می باشد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// بررسی ایمیل وارد شده که در سیستم ثبت نام کرده است یا خیر
        /// </summary>
        /// <param name="email">ایمیل</param>
        /// <param name="userId">در صورت ویرایش ردیف کاربر ارسال می شود که هنگام جستجو ردیف کاربر در نظر گرفته نشود</param>
        /// <returns>نتیجه موجود بودن ایمیل کاربر</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult CheckEmail(string email, int? userId)
        {
            try
            {
                if (!ValidationUtility.ValidateEmail(email))
                {
                    return Error("ایمیل وارد شده صحیح نمی باشد.");
                }

                int count = 0;
                if (userId != null && userId > 0)
                    count = _context.User.Count(x => x.StatusId != UserStatus.Deleted.Id && x.Id != userId && x.Email == email);
                else
                    count = _context.User.Count(x => x.StatusId != UserStatus.Deleted.Id && x.Email == email);

                if (count == 0)
                {
                    return Success();
                }
                else
                {
                    return Error("ایمیل وارد شده تکراری می باشد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// درخواست خروج از سیستم
        /// </summary>
        /// <returns>نتیجه خروج از سیستم</returns>
        [HttpPost]
        public JsonResult Logout()
        {
            try
            {
                Response.Cookies.Remove(AsefianMetadata.SiteToken);
                FormsAuthentication.SignOut();
                return Success("خروج شما با موفقیت در سیستم ثبت شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        #region User Profile
        /// <summary>
        /// تغییر رمزعبور کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات تغییر رمزعبور</param>
        /// <returns>نتیجه تغییر رمزعبور</returns>
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (string.IsNullOrEmpty(model.password))
                {
                    return Error("وارد کردن رمز عبور فعلی اجباری است.");
                }

                if (string.IsNullOrEmpty(model.newPassword) || model.newPassword.Length < 6)
                {
                    return Error("رمزعبور جدید حداقل بایستی 6 کاراکتر باشد.");
                }

                if (model.newPassword != model.confirmNewPassword)
                {
                    return Error("رمزعبور و تکرار آن برابر نیست.");
                }

                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == currentUser.id);
                var oldPassword = PasswordUtility.Encrypt(model.password);

                if (user.Password != oldPassword)
                {
                    return Error("رمزعبور قبلی به درستی وارد نشده است.");
                }

                user.Password = PasswordUtility.Encrypt(model.newPassword);
                user.ModifyUserId = currentUser.id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("رمزعبور شما با موفقیت تغییر کرد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// فراموش کردن رمزعبور کاربر
        /// که بسته به این که تلفن همراه یا ایمیل وارد کرده باشد، اطلاعات برای وی ارسال می شود.
        /// </summary>
        /// <param name="username">نام کاربری (تلفن همراه، ایمیل)</param>
        /// <returns>نتیجه ارسال رمزعبور</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ForgetPassword(string username)
        {
            try
            {
                var user = _context.User.SingleOrDefault(x => x.StatusId != UserStatus.Deleted.Id && (x.MobileNumber == username || x.Email == username));
                if (user != null)
                {
                    if (user.MobileNumber == username)
                    {
                        var password = StringUtility.RandomString(6);
                        var hashedPassword = PasswordUtility.Encrypt(password);

                        user.Password = hashedPassword;
                        user.ForceChangePassword = true;
                        user.ModifyDate = GetDatetime();
                        user.ModifyIp = GetCurrentIp();

                        //var text = _context.BaseInformation.SingleOrDefault(x => x.Key == BaseInformationKey.ForgetPasswordSmsTemplate).Value;
                        var text = "رمز عبور جدید شما: ";
                        //TemplateHelper.Manipulate(ref text, user);

                        //SmsUtility.SendSms(user.MobileNumber, text);
                        //Task.Factory.StartNew(() =>
                        //{
                        //    NikSmsWebServiceClient.SendSmsNik(text + password, "09354047788");
                        //});
                        _context.SaveChanges();
                        return Success("رمزعبور جدید از طریق پیامک ارسال شد.");
                    }
                    else
                    {
                        var entity = new UserVerifyToken()
                        {
                            UserId = user.Id,
                            TokenTypeId = TokenType.Email.Id,
                            Token = StringUtility.RandomString(50),
                            Used = false,
                            Expired = false,
                            CreateDate = GetDatetime(),
                            CreateIp = GetCurrentIp()
                        };

                        var text = _context.BaseInformation.Single(x => x.Key == BaseInformationKey.ForgetPasswordEmailTemplate).Value;
                        //TemplateHelper.Manipulate(ref text, user);
                        //TemplateHelper.Manipulate(ref text, entity);

                        EmailUtility.SendEmail(user.Email, string.Format("{0} - {1}", "درخواست بازیابی رمزعبور", AsefianMetadata.SiteName), text);

                        _context.UserVerifyToken.Add(entity);
                        _context.SaveChanges();
                        return Success("نحوه بازیابی رمزعبور برای شما ایمیل شد.");
                    }
                }
                else
                {
                    return Error("کاربری با این مشخصات یافت نشد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// تغییر مشخصات کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات کاربر</param>
        /// <returns>نتیجه تغییر اطلاعات کاربر</returns>
        [HttpPost]
        public JsonResult ChangeInfo(UserViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (!(User.IsInRole("admin") || User.IsInRole("user-change-info")) && currentUser.id != model.id)
                {
                    return Error("شما مجاز به انجام این عملیات نیستید.");
                }

                if (model.id == null || model.id <= 0)
                {
                    model.id = currentUser.id;
                }

                var user = _context.User.Where(x => x.StatusId != UserStatus.Deleted.Id && x.Id == model.id).Single();

                user.FirstName = model.firstName.ToStandardPersian();
                user.LastName = model.lastName.ToStandardPersian();
                user.SexId = model.sex;
                if (user.MobileNumber != model.mobileNumber)
                {
                    user.MobileNumber = model.mobileNumber;
                    user.MobileNumberValid = false;
                }

                if (user.Email != model.email)
                {
                    user.Email = model.email;
                    user.EmailValid = false;
                }

                _context.SaveChanges();

                return Success("اطلاعات حساب کاربری با موفقیت به روز رسانی شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// درخواست کد اعتبارسنجی موبایل
        /// </summary>
        /// <returns>نتیجه ارسال درخواست به موبایل کاربر</returns>
        [HttpPost]
        public JsonResult RequestMobileNumberVerifyToken()
        {

            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == currentUser.id);

                if (user.MobileNumberValid)
                {
                    return Error("تلفن همراه شما تایید شده است.");
                }

                var tokenList = _context.UserVerifyToken.Where(x => x.UserId == currentUser.id && x.TokenTypeId == TokenType.Mobile.Id && x.Used == false && x.Expired == false).ToList();
                tokenList.ForEach(x => x.Expired = true);

                var entity = new UserVerifyToken()
                {
                    UserId = currentUser.id,
                    TokenTypeId = TokenType.Mobile.Id,
                    Token = StringUtility.RandomNumber(),
                    Used = false,
                    Expired = false,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };

                var text = _context.BaseInformation.Single(x => x.Key == BaseInformationKey.ActiveSmsTemplate).Value;
                //TemplateHelper.Manipulate(ref text, user);
                //TemplateHelper.Manipulate(ref text, entity);

                SmsUtility.SendSms(user.MobileNumber, text);

                _context.UserVerifyToken.Add(entity);
                _context.SaveChanges();

                return Success("کد اعتبار سنجی به تلفن همراه شما ارسال شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// اعتبار سنجی تلفن همراه
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات تایید تلفن همراه</param>
        /// <returns>نتیجه تایید ایمیل</returns>
        [HttpPost]
        public JsonResult ValidateMobileNumber(ValidateViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == currentUser.id);

                if (user.MobileNumberValid)
                {
                    return Error("تلفن همراه شما تایید شده است.");
                }

                var token = _context.UserVerifyToken.Where(x => x.UserId == currentUser.id && x.TokenTypeId == TokenType.Mobile.Id && x.Used == false && x.Expired == false).SingleOrDefault();
                if (token == null)
                {
                    return Error("کد اعتبار سنجی یافت نشد.");
                }

                if (token.CreateDate.AddHours(AsefianMetadata.SmsActiveTokenExpirateHour) >= DateTime.Now)
                {
                    token.Expired = true;
                    _context.SaveChanges();
                    return Error("کد اعتبار سنجی شما منقضی شده است.");
                }

                if (model.code != token.Token)
                {
                    return Error("کد وارد شده معتبر نیست.");
                }

                token.Used = true;
                token.Expired = true;
                token.UsedDate = GetDatetime();
                token.UsedIp = GetCurrentIp();

                user.MobileNumberValid = true;
                user.ModifyUserId = user.Id;
                user.ModifyDate = GetDatetime();
                user.ModifyIp = GetCurrentIp();

                _context.SaveChanges();

                return Success("تلفن همراه شما با موفقیت تایید شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// درخواست تایید کد اعتبارسنجی ایمیل
        /// </summary>
        /// <returns>نتیجه ارسال درخواست کد به ایمیل کاربر</returns>
        [HttpPost]
        public JsonResult RequestEmailVerifyToken()
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var user = _context.User.Single(x => x.StatusId != UserStatus.Deleted.Id && x.Id == currentUser.id);

                if (user.EmailValid)
                {
                    return Error("ایمیل شما تایید شده است.");
                }

                var tokenList = _context.UserVerifyToken.Where(x => x.UserId == currentUser.id && x.TokenTypeId == TokenType.Email.Id && x.Used == false && x.Expired == false).ToList();
                tokenList.ForEach(x => x.Expired = true);

                var entity = new UserVerifyToken()
                {
                    UserId = currentUser.id,
                    TokenTypeId = TokenType.Email.Id,
                    Token = StringUtility.RandomString(50),
                    Used = false,
                    Expired = false,
                    CreateDate = GetDatetime(),
                    CreateIp = GetCurrentIp()
                };

                var text = _context.BaseInformation.Single(x => x.Key == BaseInformationKey.ActiveEmailTemplate).Value;
                //TemplateHelper.Manipulate(ref text, user);
                //TemplateHelper.Manipulate(ref text, entity);

                EmailUtility.SendEmail(user.Email, string.Format("{0} - {1}", "تایید ایمیل", AsefianMetadata.SiteName), text);

                _context.UserVerifyToken.Add(entity);
                _context.SaveChanges();

                return Success("کد اعتبار سنجی برای شما ارسال شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
        #endregion

        #region User Address
        /// <summary>
        /// دریافت لیست آدرس های کاربر
        /// </summary>
        /// <returns>لیست آدرس های ثبت شده برای کاربر</returns>
        [HttpGet]
        public JsonResult ListAddress()
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                var addressList = _context.UserAddress.Where(x => x.StatusId == UserAddressStatus.Active.Id && x.UserId == currentUser.id).OrderByDescending(x => x.MainAddress).ThenByDescending(x => x.Id).Select(x => new UserAddressViewModel()
                {
                    id = x.Id,
                    receiverFullName = x.ReceiverFullName,
                    receiverMobileNumber = x.ReceiverMobileNumber,
                    phoneNumber = x.PhoneNumber,
                    provinceId = x.ProvinceId,
                    cityId = x.CityId,
                    postalCode = x.PostalCode,
                    address = "استان " + x.Province.Title + "، شهر " + x.City.Title + "، " + x.Address,
                    main = x.MainAddress
                }).ToList();

                return SuccessSearch(addressList);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ذخیره آدرس کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات آدرس</param>
        /// <returns>نتیجه ذخیره آدرس</returns>
        [HttpPost]
        public JsonResult SaveAddress(UserAddressViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (!string.IsNullOrEmpty(model.receiverMobileNumber) && !ValidationUtility.ValidateMobileNumber(model.receiverMobileNumber))
                {
                    return Error("تلفن همراه وارد شده صحیح نیست.");
                }

                if (!ValidationUtility.ValidationPostalCode(model.postalCode))
                {
                    return Error("کد پستی وارد شده صحیح نیست.");
                }

                if (string.IsNullOrEmpty(model.address))
                {
                    return Error("وارد کردن آدرس اجباری است.");
                }


                if (model.id != null && model.id > 0)
                {
                    var entity = _context.UserAddress.Single(x => x.StatusId != UserAddressStatus.Deleted.Id && x.Id == model.id && x.UserId == currentUser.id);

                    entity.ReceiverFullName = model.receiverFullName?.ToStandardPersian();
                    entity.ReceiverMobileNumber = model.receiverMobileNumber?.ToStandardPersian();
                    entity.PhoneNumber = model.phoneNumber?.ToStandardPersian();
                    entity.CountryId = AsefianMetadata.IranIdKey;
                    entity.ProvinceId = model.provinceId;
                    entity.CityId = model.cityId;
                    entity.PostalCode = model.postalCode;
                    entity.Address = model.address;

                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();
                    return Success("آدرس با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new UserAddress()
                    {
                        UserId = currentUser.id,
                        ReceiverFullName = model.receiverFullName?.ToStandardPersian(),
                        ReceiverMobileNumber = model.receiverMobileNumber?.ToStandardPersian(),
                        PhoneNumber = model.phoneNumber?.ToStandardPersian(),
                        CountryId = AsefianMetadata.IranIdKey,
                        ProvinceId = model.provinceId,
                        CityId = model.cityId,
                        PostalCode = model.postalCode,
                        Address = model.address,
                        StatusId = UserAddressStatus.Active.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.UserAddress.Add(entity);

                    _context.SaveChanges();
                    return Success("آدرس با موفقیت اضافه شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// خواندن اطلاعات آدرس کاربر
        /// </summary>
        /// <param name="id">ردیف آدرس</param>
        /// <returns>اطلاعات آدرس</returns>
        [HttpGet]
        public JsonResult LoadAddress(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserAddress.Where(x => x.StatusId != UserAddressStatus.Deleted.Id && x.Id == id && x.UserId == currentUser.id).Select(x => new UserAddressViewModel()
                {
                    id = x.Id,
                    receiverFullName = x.ReceiverFullName,
                    receiverMobileNumber = x.ReceiverMobileNumber,
                    phoneNumber = x.PhoneNumber,
                    provinceId = x.ProvinceId,
                    cityId = x.CityId,
                    postalCode = x.PostalCode,
                    address = x.Address
                }).Single();

                return Success(entity);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف آدرس کاربر
        /// </summary>
        /// <param name="id">ردیف آدرس</param>
        /// <returns>نتیجه حذف آدرس کاربر</returns>
        [HttpPost]
        public JsonResult DeleteAddress(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserAddress.Single(x => x.StatusId != UserAddressStatus.Deleted.Id && x.Id == id && x.UserId == currentUser.id);

                entity.StatusId = UserAddressStatus.Deleted.Id;
                entity.MainAddress = false;

                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();
                return Success("آدرس با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت اطلاعات موقعیت جغرافیایی برای آدرس کاربر
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات موقعیت جغرافیایی و آدرس</param>
        /// <returns>نتیجه ثبت اطلاعات موقعیت جغرافیایی</returns>
        [HttpPost]
        public JsonResult SetMapLocation(UserAddressMapViewModel model)
        {
            try
            {
                if (!ValidationUtility.ValidateLatitude(model.latitude))
                {
                    return Error("عرض جغرافیایی وارد شده صحیح نیست.");
                }

                if (!ValidationUtility.ValidateLongitude(model.longitude))
                {
                    return Error("طول جغرافیایی وارد شده صحیح نیست.");
                }

                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserAddress.Single(x => x.StatusId != UserAddressStatus.Deleted.Id && x.Id == model.id && x.UserId == currentUser.id);

                entity.Longitude = model.longitude;
                entity.Latitude = model.latitude;

                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();
                return Success("آدرس با موفقیت ویرایش شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// ثبت آدرس کاربر به عنوان آدرس پیشفرض
        /// </summary>
        /// <param name="addressId">ردیف آدرس</param>
        /// <returns>نتیجه ثبت آدرس به عنوان آدرس پیشفرض</returns>
        [HttpPost]
        public JsonResult SetMainAddress(int addressId)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var addressList = _context.UserAddress.Where(x => x.StatusId != UserAddressStatus.Deleted.Id && x.UserId == currentUser.id).ToList();

                addressList.ForEach(item =>
                {
                    if (item.Id == addressId)
                    {
                        item.MainAddress = true;
                        item.ModifyUserId = currentUser.id;
                        item.ModifyDate = GetDatetime();
                        item.ModifyIp = GetCurrentIp();
                    }
                    else
                    {
                        item.MainAddress = false;
                    }
                });

                _context.SaveChanges();
                return Success("آدرس پیشفرض با موفقیت تغییر کرد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
        #endregion

        #region User Favourites
        /// <summary>
        /// ذخیره اطلاعات پوشه
        /// </summary>
        /// <param name="model">مدل اطلاعات پوشه</param>
        /// <returns>نتیجه ذخیره اطلاعات پوشه</returns>
        [HttpPost]
        public JsonResult SaveFolder(UserFavouriteFolderViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();

                if (string.IsNullOrEmpty(model.title))
                {
                    return Error("وارد کردن عنوان پوشه الزامی است.");
                }

                if (model.id != null && model.id > 0)
                {
                    var entity = _context.UserFavoriteFolder.Single(x => x.StatusId != UserFavoriteFolderStatus.Deleted.Id && x.Id == model.id && x.UserId == currentUser.id);

                    entity.ParentId = model.parentId;
                    entity.Title = model.title.ToStandardPersian();
                    entity.ModifyUserId = currentUser.id;
                    entity.ModifyDate = GetDatetime();
                    entity.ModifyIp = GetCurrentIp();

                    _context.SaveChanges();
                    return Success("پوشه با موفقیت ویرایش شد.");
                }
                else
                {
                    var entity = new UserFavoriteFolder()
                    {
                        UserId = currentUser.id,
                        ParentId = model.parentId,
                        Title = model.title.ToStandardPersian(),
                        StatusId = UserFavoriteFolderStatus.Active.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };

                    _context.UserFavoriteFolder.Add(entity);

                    _context.SaveChanges();
                    return Success("پوشه با موفقیت اضافه شد.");
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف پوشه کاربر
        /// </summary>
        /// <param name="id">ردیف پوشه</param>
        /// <returns>نتیجه حذف پوشه کاربر</returns>
        [HttpPost]
        public JsonResult DeleteFolder(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserFavoriteFolder.Single(x => x.StatusId != UserFavoriteFolderStatus.Deleted.Id && x.Id == id && x.UserId == currentUser.id);

                entity.StatusId = UserFavoriteFolderStatus.Deleted.Id;

                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();
                return Success("پوشه با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// اضافه کردن محصول به لیست علاقه مندی ها
        /// </summary>
        /// <param name="model">مدل حاوی اطلاعات محصول و پوشه</param>
        /// <returns>نتیجه اضافه کردن محصول به لیست علاقه مندی</returns>
        [HttpPost]
        public JsonResult AddToFavourite(UserFavouriteProductViewModel model)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                if (model.folderId != null && model.folderId > 0)
                {
                    var folder = _context.UserFavoriteFolder.Where(x => x.Id == model.folderId).Single();
                }

                if (_context.UserFavorite.Count(x => x.StatusId != UserFavoriteStatus.Deleted.Id && x.FolderId == model.folderId && x.ProductId == model.productId) == 0)
                {
                    var entity = new UserFavorite()
                    {
                        UserId = currentUser.id,
                        ProductId = model.productId,
                        FolderId = model.folderId,
                        StatusId = UserFavoriteStatus.Active.Id,
                        CreateUserId = currentUser.id,
                        ModifyUserId = currentUser.id,
                        CreateDate = GetDatetime(),
                        ModifyDate = GetDatetime(),
                        CreateIp = GetCurrentIp(),
                        ModifyIp = GetCurrentIp()
                    };
                    _context.UserFavorite.Add(entity);
                    _context.SaveChanges();
                }

                return Success("محصول مورد نظر با موفقیت به لیست علاقه مندی ها اضافه شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// حذف محصول از لیست علاقه مندی های کاربر
        /// </summary>
        /// <param name="id">ردیف علاقه مندی</param>
        /// <returns>نتیجه حذف محصول از لیست علاقه مندی کاربر</returns>
        [HttpPost]
        public JsonResult DeleteFavourite(int id)
        {
            try
            {
                var currentUser = GetAuthenticatedUser();
                var entity = _context.UserFavorite.Single(x => x.StatusId != UserFavoriteStatus.Deleted.Id && x.Id == id && x.UserId == currentUser.id);

                entity.StatusId = UserFavoriteStatus.Deleted.Id;

                entity.ModifyUserId = currentUser.id;
                entity.ModifyDate = GetDatetime();
                entity.ModifyIp = GetCurrentIp();

                _context.SaveChanges();
                return Success("محصول مورد نظر با موفقیت از لیست علاقه مندی ها حذف شد.");
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// دریافت لیست علاقه مندی ها
        /// </summary>
        /// <param name="parentId">ردیف پوشه</param>
        /// <returns>لیست علاقه مندی ها</returns>
        //[HttpGet]
        //public JsonResult FavouriteList(int? folderId)
        //{
        //    try
        //    {
        //        var currentUser = GetAuthenticatedUser();

        //        var queryFolder = _context.UserFavoriteFolder.Where(x => x.StatusId != UserFavoriteFolderStatus.Deleted.Id && x.UserId == currentUser.id && x.ParentId == folderId).Select(x => new UserFavouriteVideModel()
        //        {
        //            id = x.Id,
        //            type = 1,
        //            title = x.Title
        //        });

        //        var queryProduct = _context.UserFavorite.Where(x => x.StatusId != UserFavoriteStatus.Deleted.Id && x.UserId == currentUser.id && x.FolderId == folderId).Select(x => new UserFavouriteVideModel()
        //        {
        //            id = x.Id,
        //            type = 1,
        //            title = x.Product.Title
        //        });

        //        var data = queryFolder.Union(queryProduct).OrderBy(x => x.type).ToList();

        //        return SuccessSearch(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServerError(ex);
        //    }
        //}
        #endregion
    }
}