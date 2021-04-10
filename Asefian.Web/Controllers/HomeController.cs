using Asefian.Model.Context.Core.Enum;
using Asefian.Model.Context.Data.Enum;
using Asefian.Model.Metadata;
using Asefian.Model.ViewModel.Data;
using System.Linq;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر صفحه اصلی
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// مشاهده جزئیات صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// دریافت لینک تغییر زبان سایت
        /// </summary>
        /// <returns>صفحه مورد نظر</returns>
        [AllowAnonymous]
        public ActionResult _LanguageRequestPath()
        {
            return PartialView();
        }

        /// <summary>
        /// مشاهده لیست محصولات
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult _Product()
        {
            var langId = GetLanguage().Id;
            var query = _context.CategoryTranslation.Where(x => x.Category.StatusId != CategoryStatus.Deleted.Id && x.Category.ParentId != null && x.LanguageId == langId);
            var categoryList = query.OrderBy(x => x.Category.Order).Select(x => new CategoryViewModel()
            {
                id = x.Id,
                title = x.Title,
                productList = x.Category.ProductList.Where(y => y.StatusId != ProductStatus.Deleted.Id && y.StatusId != ProductStatus.Inactive.Id).OrderByDescending(y => y.Id).Select(y => new ProductViewModel()
                {
                    id = y.Id,
                    sku = y.TranslationList.FirstOrDefault(z => z.LanguageId == langId).Title,
                    code = y.Code,
                    price = y.Price,
                    fileId = y.FileId,
                    fileName = y.FileName,
                    statusId = y.StatusId,
                    category = y.Category.Sku,
                }).Take(10).ToList()
            }).ToList();
            return PartialView(categoryList);
        }

        /// <summary>
        /// قسمت برند ها در میان صفحه
        /// </summary>
        /// <returns>صفحه قسمت برند ها</returns>
        [AllowAnonymous]
        public ActionResult _SiteInfoFooter()
        {
            var lang = GetLanguage().Id;
            var variable = new string[] {
                BaseInformationKey.SiteAddress,
                BaseInformationKey.SitePhoneNumber,
                BaseInformationKey.SiteFax,
                BaseInformationKey.SiteEmail,
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return PartialView(data);
        }

        /// <summary>
        /// لیست شبکه های اجتماعی در فوتر صفحه
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult _SiteSocialNetwork()
        {
            var variable = new string[] {
                BaseInformationKey.SiteInstagram,
                BaseInformationKey.SiteTelegram,
                BaseInformationKey.SiteFacebook,
                BaseInformationKey.SiteYoutube,
                BaseInformationKey.SiteAparat,
                BaseInformationKey.SiteTwitter
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return PartialView(data);
        }
        /// <summary>
        /// نمایش صفحه صادرات
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Export()
        {
            var variable = new string[] {
                BaseInformationKey.ExportProduct,
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return BaseView(data);
        }
        /// <summary>
        /// نمایش صفحه من طراح هستم
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Designer()
        {
            var variable = new string[] {
                BaseInformationKey.Solicitorship,
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return BaseView(data);
        }
        /// <summary>
        /// نمایش صفحه همکاری با ما
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult WithWorkUs()
        {
            var variable = new string[] {
                BaseInformationKey.Solicitorship,
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).ToList();
            return BaseView(data);
        }
        /// <summary>
        /// لیست دسته بندی ها و گروه محصولات برای نمایش در صفحه اول
        /// </summary>
        /// <returns>صحفه مورد نظر</returns>
        [AllowAnonymous]
        public ActionResult _Map()
        {
            var variable = new string[] {
                BaseInformationKey.SiteLocation
            };
            var data = _context.BaseInformation.Where(x => variable.Any(y => y == x.Key)).SingleOrDefault();
            return PartialView(data);
        }

        /// <summary>
        /// نمایش اسلایدرها در صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult _Slider()
        {
            var sliderList = _context.Slider.Where(x => x.StatusId != SliderStatus.Deleted.Id && x.TypeId == SliderType.Slider.Id).OrderByDescending(x => x.Id).ToList();
            return PartialView(sliderList);

        }
        /// <summary>
        /// نمایش اسلایدر تکی در وسط صفحه اصلی
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult _BackgroundImage()
        {
            var sliderList = _context.Slider.Where(x => x.StatusId != SliderStatus.Deleted.Id && x.TypeId == SliderType.BackgroundImage.Id).OrderByDescending(x => x.Id).FirstOrDefault();
            return PartialView(sliderList);

        }
        [AllowAnonymous]
        public ActionResult _Category()
        {
            var langId = GetLanguage().Id;
            var query = _context.CategoryTranslation.Where(x => x.Category.StatusId != CategoryStatus.Deleted.Id && x.Category.ParentId != null && x.LanguageId == langId);
            var categoryList = query.OrderBy(x => x.Category.Order).Select(x => new CategoryViewModel()
            {
                id = x.Id,
                title = x.Title,
                categoryId = x.CategoryId,
                fileId = x.Category.FileId,
                fileName = x.Category.FileName

            }).Take(4).ToList();
            ViewBag.NewProduct = _context.Product.Where(x=>x.StatusId != ProductStatus.Deleted.Id && x.StatusId != ProductStatus.Inactive.Id).OrderByDescending(x => x.Id).FirstOrDefault();
            return PartialView(categoryList);

        }
    }
}