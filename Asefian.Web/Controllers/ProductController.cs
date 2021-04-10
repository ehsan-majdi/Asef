using Asefian.Model.Context.Data.Enum;
using Asefian.Model.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asefian.Web.Controllers
{
    /// <summary>
    /// کنترلر محصولات
    /// </summary>
    [AllowAnonymous]
    public class ProductController : BaseController
    {
        /// <summary>
        /// مشاهده یک محصول
        /// </summary>
        /// <param name="id">ردیف محصول</param>
        /// <returns>صفحه محصول</returns>
        [Route("product/{id}/{title}")]
        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            var product = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.StatusId != ProductStatus.Inactive.Id && x.Id == id).SingleOrDefault();
            return View(product);
        }

        /// <summary>
        /// جستجوی لیست محصولات
        /// </summary>
        /// <param name="options">پارامتر های مورد تاثیر در جستجو</param>
        /// <returns>نتیجه یافت شده از محصولات به صورت جیسون</returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Search(SearchProductViewModel options)
        {
            try
            {
                var query = _context.Product.Where(x => x.StatusId != ProductStatus.Deleted.Id && x.StatusId != ProductStatus.Inactive.Id && x.Category.StatusId == CategoryStatus.Active.Id && x.Brand.StatusId == BrandStatus.Active.Id);

                if (!string.IsNullOrEmpty(options.word))
                {
                    var word = options.word.ToStandardPersian();
                    query = query.Where(x => x.Sku.Contains(word) || x.Code.Contains(word));
                }

                if (options.brand != null && options.brand.Count(x => x > 0) > 0)
                {
                    var brand = options.brand.Where(x => x > 0).ToArray();
                    query = query.Where(x => brand.Any(y => y == x.BrandId));
                }

                if (options.category != null && options.category.Count(x => x > 0) > 0)
                {
                    var category = options.category.Where(x => x > 0).ToArray();
                    query = query.Where(x => category.Any(y => y == x.CategoryId || y == x.Category.ParentId || y == x.Category.Parent.ParentId));
                }

                if (options.categoryId != null && options.categoryId > 0)
                {
                    query = query.Where(x => x.CategoryId == options.categoryId || x.Category.ParentId == options.categoryId || x.Category.Parent.ParentId == options.categoryId);
                }

                if (options.filterValue != null && options.filterValue.Count > 0)
                {
                    foreach (var item in options.filterValue)
                    {
                        if (item.valueList != null && item.valueList.Count > 0)
                        {
                            query = query.Where(x => x.ProductFilterDataList.Any(y => y.ProductFilterId == item.id && item.valueList.Any(z => z == y.ProductFilterValueId)));
                        }
                        else if (item.valueBoolean != null)
                        {
                            query = query.Where(x => x.ProductFilterDataList.Any(y => y.ProductFilterId == item.id && y.ValueBoolean == item.valueBoolean));
                        }
                        else if (!string.IsNullOrEmpty(item.value))
                        {
                            query = query.Where(x => x.ProductFilterDataList.Any(y => y.ProductFilterId == item.id && y.Value.Contains(item.value)));
                        }
                    }
                }

                var count = query.Count();

                var data = query.OrderByDescending(x => x.Id)
                .Skip(options.page * options.count)
                .Take(options.count)
                .Select(x => new SiteSearchProductViewModel()
                {
                    id = x.Id,
                    sku = x.Sku,
                    category = x.Category.Sku,
                    count = x.StatusId == ProductStatus.Stock.Id ? (x.Count + ((int?)x.ProductFeatureList.Sum(y => y.Count) ?? 0)) : 0,
                    price = x.Price > 0 ? x.Price : ((long?)x.ProductFeatureList.OrderBy(y => y.Price).FirstOrDefault().Price ?? 0),
                    discount = x.Discount > 0 ? x.Discount : ((long?)x.ProductFeatureList.OrderBy(y => y.Price).FirstOrDefault().Discount ?? 0),
                    fileId = x.FileId,
                    width = x.Width,
                    height = x.Height,
                    code = x.Code,
                    fileName = x.FileName,
                    statusId = x.StatusId,
                    rate = x.Rate
                }).ToList();

                data.ForEach(x =>
                {
                    x.titleUrl = x.sku.ToUrlString();
                });

                return SuccessSearch(data, options.page + 1, options.count, count);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

    }
}