using System.Web;
using System.Web.Optimization;

namespace ZSTUZCGLC
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
             // bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
             //           "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/bootstrap-hover-dropdown.js"));
            bundles.Add(new ScriptBundle("~/bundles/Slide").Include(
                      "~/Scripts/flickerplate.min.js",      
                      "~/Scripts/jquery.SuperSlide.2.1.1.js"));

            bundles.Add(new StyleBundle("~/Content/Bcss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/BSite.css",
                      "~/Content/bootstrap-select.css"));
            bundles.Add(new StyleBundle("~/Content/Fcss").Include(
          "~/Content/bootstrap.min.css",
          "~/Content/FSite.css",
          "~/Content/banner.css"));
            bundles.Add(new StyleBundle("~/Content/wangEditor").Include(
                      "~/Content/wangEditor.css"));
            bundles.Add(new ScriptBundle("~/bundles/wangEditor").Include(
                      "~/Scripts/wangEditor.js"));
            bundles.Add(new ScriptBundle("~/bundles/j-ui").Include(
          "~/Scripts/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/plupload").Include(
            "~/Scripts/plupload/plupload.full.min.js"));
        }        
    }
}
