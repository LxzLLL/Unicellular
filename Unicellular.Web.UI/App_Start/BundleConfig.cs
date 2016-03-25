using System.Web;
using System.Web.Optimization;

namespace Unicellular.Web.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles( BundleCollection bundles )
        {
            bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
                        "~/Scripts/jquery-{version}.js" ) );

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
                        "~/Scripts/modernizr-*" ) );

            bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js" ) );
            //引用jQuery2.1.4
            bundles.Add( new ScriptBundle( "~/bundles/plugins/jQuery2.1.4" ).Include(
                "~/AdminLTE-2.3.0/plugins/jQuery/jQuery-2.1.4.min.js"
                ) );
            //引用AdminLTE的app样式
            bundles.Add( new ScriptBundle( "~/bundles/dist/app" ).Include(
                "~/AdminLTE-2.3.0/dist/js/app.min.js"
                ) );
            //页面主题样式
            bundles.Add( new StyleBundle( "~/Content/css" ).Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/AdminLTE-2.3.0/dist/css/AdminLTE.min.css",
                      "~/AdminLTE-2.3.0/dist/css/skins/skin-blue.min.css" ) );
        }
    }
}
