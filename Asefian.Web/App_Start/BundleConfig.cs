using System.Web.Optimization;

namespace Asefian.Web.App_Start
{
    /// <summary>
    /// پیکره بندی باندل های استفاده شده در سایت
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// ثبت باندل های استفاده شده در برنامه
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles/uikit-rtl")
                .Include("~/Styles/uikit-rtl.css", new CssRewriteUrlTransform())
            );
            bundles.Add(new StyleBundle("~/styles/uikit")
               .Include("~/Styles/uikit.css", new CssRewriteUrlTransform())
           );
            bundles.Add(new StyleBundle("~/styles/core")
                .Include("~/Styles/uikit-rtl.css", new CssRewriteUrlTransform())
                .Include("~/Styles/uikit-override.css", new CssRewriteUrlTransform())
                .Include("~/Fonts/fontawsome/all.css", new CssRewriteUrlTransform())
                .Include("~/Fonts/sans/fontiran.css", new CssRewriteUrlTransform())
                .Include("~/Fonts/yekan/fontiran.css", new CssRewriteUrlTransform())
                .Include("~/Styles/general.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/admin")
                .Include("~/Styles/site.css", new CssRewriteUrlTransform())
                .Include("~/Styles/admin.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/web")
                .Include("~/Styles/site.css", new CssRewriteUrlTransform())
                .Include("~/Styles/theme.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/editor")
                .Include("~/Scripts/froala.editor.2.8.4/css/froala_editor.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/froala_style.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/plugins/code_view.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/plugins/image_manager.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/plugins/image.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/plugins/table.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/froala.editor.2.8.4/css/froala_editor.css", new CssRewriteUrlTransform())
                .Include("~/Fonts/fontawsome/v4-shims.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/fancy")
                .Include("~/Scripts/fancybox.3.5.7/dist/jquery.fancybox.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/auto")
                .Include("~/Styles/jqueryui/jquery-ui.css", new CssRewriteUrlTransform())
            );
            bundles.Add(new StyleBundle("~/styles/scroll")
                .Include("~/Scripts/scrollbar-3.1.5/jquery.mCustomScrollbar.css", new CssRewriteUrlTransform())
            );
            bundles.Add(new StyleBundle("~/styles/leaflet")
                .Include("~/Scripts/leaflet-1.5.1/leaflet.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new ScriptBundle("~/scripts/web").Include(
               "~/Scripts/jquery-3.4.1.js",
               "~/Scripts/uikit.js",
               "~/Scripts/jsrender.js",
               "~/Scripts/template.js",
               "~/Scripts/jquery.general.js",
               "~/Scripts/app/jquery.validate.js",
               "~/Scripts/app/jquery.list.js",
               "~/Scripts/app/jquery.form.js",
               "~/Scripts/app/jquery.select.js",
               "~/Scripts/number-only.js",
               "~/Scripts/number-text.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/upload").Include(
                "~/Scripts/file.upload.9.21.0/js/vendor/jquery.ui.widget.js",
                "~/Scripts/file.upload.9.21.0/js/jquery.iframe-transport.js",
                "~/Scripts/file.upload.9.21.0/js/jquery.fileupload.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/editor").Include(
               "~/Scripts/froala.editor.2.8.4/js/froala_editor.min.js",
               "~/Scripts/froala.editor.2.8.4/js/languages/fa.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/align.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/code_beautifier.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/code_view.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/draggable.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/image.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/image_manager.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/link.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/lists.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/paragraph_format.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/paragraph_style.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/table.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/url.min.js",
               "~/Scripts/froala.editor.2.8.4/js/plugins/entities.min.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/fancy").Include(
               "~/Scripts/fancybox.3.5.7/dist/jquery.fancybox.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/auto").Include(
               "~/Scripts/jqueryui/jquery-ui.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/scroll").Include(
               "~/Scripts/scrollbar-3.1.5/jquery.mCustomScrollbar.concat.min.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/leaflet").Include(
               "~/Scripts/leaflet-1.5.1/leaflet.js"
            ));
        }
    }
}