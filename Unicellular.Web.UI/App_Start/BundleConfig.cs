using System.Web;
using System.Web.Optimization;

namespace Unicellular.Web.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles( BundleCollection bundles )
        {
            //bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
            //            "~/Scripts/jquery-{version}.js" ) );

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
            //            "~/Scripts/modernizr-*" ) );

            #region bootstrap css js
            //bootstrap样式
            bundles.Add( new StyleBundle( "~/Bootstrap/css" ).Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css" ) );
            //bootstrapjs
            bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include(
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js" ) );
            #endregion

            //页面主题样式
            bundles.Add( new StyleBundle( "~/Content/css" ).Include(
                      "~/AdminLTE-2.3.0/dist/css/AdminLTE.min.css",
                      "~/AdminLTE-2.3.0/dist/css/skins/skin-blue.min.css" ) );

            #region 有关Jquery ui datatable table页面所需的css和js资源
            //有关Jquery table页面所需的css和js资源
            bundles.Add( new StyleBundle( "~/bundles/plugins/datatablecss" ).Include(
                "~/AdminLTE-2.3.0/plugins/datatables/dataTables.bootstrap.css",
                "~/AdminLTE-2.3.0/plugins/datatables/extensions/TableTools/css/dataTables.tableTools.min.css"
                ) );
            bundles.Add( new ScriptBundle( "~/bundles/plugins/datatablejs" ).Include(
                "~/AdminLTE-2.3.0/plugins/datatables/jquery.dataTables.min.js",
                "~/AdminLTE-2.3.0/plugins/datatables/dataTables.bootstrap.min.js",
                "~/AdminLTE-2.3.0/plugins/datatables/extensions/TableTools/js/dataTables.tableTools.min.js",
                "~/AdminLTE-2.3.0/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/AdminLTE-2.3.0/plugins/fastclick/fastclick.min.js"
                ) );
            #endregion

            #region 有关Bootstrap table页面所需的css和js资源
            //有关Bootstrap table页面所需的css和js资源
            bundles.Add( new StyleBundle( "~/bundles/plugins/bstablecss" ).Include(
                "~/Scripts/Plugins/bstable/bootstrap-table.min.css"
                ) );
            bundles.Add( new ScriptBundle( "~/bundles/plugins/bstablejs" ).Include(
                "~/Scripts/Plugins/bstable/bootstrap-table.js",
                 "~/Scripts/Plugins/bstable/locale/bootstrap-table-zh-CN.js"
                ) );
            #endregion

            #region 有关Bootstrap dialog modal页面所需的css和js资源（封装）
            //有关Bootstrap table页面所需的css和js资源
            bundles.Add( new StyleBundle( "~/bundles/plugins/bsDialogcss" ).Include(
                "~/Scripts/Plugins/bsDialog/css/bootstrap-dialog.min.css"
                ) );
            bundles.Add( new ScriptBundle( "~/bundles/plugins/bsDialogjs" ).Include(
                "~/Scripts/Plugins/bsDialog/css/bootstrap-dialog.js"
                ) );
            #endregion

            //引用jQuery2.1.4
            bundles.Add( new ScriptBundle( "~/bundles/plugins/jQuery2.1.4" ).Include(
                "~/AdminLTE-2.3.0/plugins/jQuery/jQuery-2.1.4.min.js"
                //"~/Scripts/jquery-1.11.0.min.js"
                ) );

            //引用AdminLTE的app样式
            bundles.Add( new ScriptBundle( "~/bundles/dist/app" ).Include(
                "~/AdminLTE-2.3.0/dist/js/app.js"
                ) );

            #region knockoutjs的资源引用
            bundles.Add( new ScriptBundle( "~/bundles/plugins/knockoutjs" ).Include(
                "~/Scripts/knockout-3.4.0.js"
                ) );
            #endregion

            #region  bootstrap validator的资源引用
            bundles.Add( new StyleBundle( "~/bundles/plugins/bsvalidatorcss" ).Include(
                "~/Scripts/Plugins/bsValidator/css/bootstrapValidator.min.css"
                ) );
            bundles.Add( new ScriptBundle( "~/bundles/plugins/bsvalidatorjs" ).Include(
                "~/Scripts/Plugins/bsValidator/js/bootstrapValidator.js",
                 "~/Scripts/Plugins/bsValidator/js/language/zh_CN.js"
                ) );
            #endregion

        }
    }
}
