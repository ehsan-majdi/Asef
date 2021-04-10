using Asefian.Common;
using Asefian.Common.Utility;
using Asefian.Model.FileContext;
using Asefian.Web.Controllers;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookieBox.Web.Controllers
{
    /// <summary>
    /// کنترلر فایل
    /// </summary>
    public class FileController : BaseController
    {
        /// <summary>
        /// دریافت فایل های عمومی
        /// </summary>
        /// <param name="id">ردیف فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns>نتیجه جستجوی فایل</returns>
        [Route("upload/file/{id}/{fileName}")]
        [AllowAnonymous]
        public ActionResult Download(string id, string fileName)
        {
            if (!string.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
            {
                Response.StatusCode = 304;
                Response.StatusDescription = "Not Modified";
                Response.AddHeader("Content-Length", "0");
                return Content(String.Empty);
            }

            var file = AsefianFileContextHelper.GetFile(id, fileName);

            try
            {
                MemoryStream outStream = new MemoryStream();

                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    ISupportedImageFormat format = new JpegFormat { Quality = 70 };

                    imageFactory.Load(file.Data)
                        .Format(format)
                        .Save(outStream);

                    return File(outStream, MimeMapping.GetMimeMapping(fileName));
                }
            }
            catch (Exception)
            {
                return File(file.Data, file.MimeType);
            }
        }

        /// <summary>
        /// تغییر سایز تصویر و ذخیره در کش
        /// </summary>
        /// <param name="size">ابعاد خواسته شده</param>
        /// <param name="fileId">شناسه فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns>فایل تصویر تغییر سایز داده شده</returns>
        [AllowAnonymous]
        public ActionResult ResizeImage(string size, string fileId, string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
                {
                    Response.StatusCode = 304;
                    Response.StatusDescription = "Not Modified";
                    Response.AddHeader("Content-Length", "0");
                    return Content(string.Empty);
                }

                #region Temp Path
                var tempPath = Server.MapPath("~/Temp/Thumb/resize");
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                var filePath = $"{tempPath}\\{size}-{fileId}{Path.GetExtension(fileName)}";
                if (System.IO.File.Exists(filePath))
                {
                    byte[] filedata = System.IO.File.ReadAllBytes(filePath);
                    string contentType = MimeMapping.GetMimeMapping(filePath);

                    return File(filedata, contentType);
                }
                #endregion

                var sizes = size.Split('x').Select(x => int.Parse(x)).ToArray();
                var file = AsefianFileContextHelper.GetFile(fileId, fileName);

                MemoryStream outStream = new MemoryStream();
                ISupportedImageFormat format = new JpegFormat { Quality = 70 };

                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    var resizeLayer = new ResizeLayer(new Size(sizes[0], sizes[1]), ResizeMode.Min);

                    imageFactory.Load(file.Data)
                        .Resize(resizeLayer)
                        .Format(format)
                        .Save(filePath)
                        .Save(outStream);

                    return File(outStream, MimeMapping.GetMimeMapping(fileName));
                }
            }
            catch (Exception ex)
            {
                ServerError(ex);
                return HttpNotFound();
            }
        }

        /// <summary>
        /// تغییر سایز تصویر و ذخیره در کش
        /// </summary>
        /// <param name="size">ابعاد خواسته شده</param>
        /// <param name="fileId">شناسه فایل</param>
        /// <param name="fileName">نام فایل</param>
        /// <returns>فایل تصویر تغییر سایز داده شده</returns>
        [AllowAnonymous]
        public ActionResult CropImage(string size, string fileId, string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
                {
                    Response.StatusCode = 304;
                    Response.StatusDescription = "Not Modified";
                    Response.AddHeader("Content-Length", "0");
                    return Content(string.Empty);
                }

                #region Temp Path
                var tempPath = Server.MapPath("~/Temp/Thumb/crop");
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                var filePath = $"{tempPath}\\{size}-{fileId}{Path.GetExtension(fileName)}";
                if (System.IO.File.Exists(filePath))
                {
                    byte[] filedata = System.IO.File.ReadAllBytes(filePath);
                    string contentType = MimeMapping.GetMimeMapping(filePath);

                    return File(filedata, contentType);
                }
                #endregion

                var sizes = size.Split('x').Select(x => int.Parse(x)).ToArray();
                var file = AsefianFileContextHelper.GetFile(fileId, fileName);

                MemoryStream outStream = new MemoryStream();
                ISupportedImageFormat format = new JpegFormat { Quality = 70 };

                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    var resizeLayer = new ResizeLayer(new Size(sizes[0], sizes[1]), ResizeMode.Crop);

                    imageFactory.Load(file.Data)
                        .Resize(resizeLayer)
                        .Format(format)
                        .Save(filePath)
                        .Save(outStream);

                    return File(outStream, MimeMapping.GetMimeMapping(fileName));
                }
            }
            catch (Exception ex)
            {
                ServerError(ex);
                return HttpNotFound();
            }
        }
    }
}