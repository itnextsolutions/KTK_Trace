using System.Web;
using System.Web.Optimization;

namespace KotakTracePortal
{
    public class BundleConfig
    {
        
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            var versionstr = "";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/switch").Include(
                        "~/octopus/assets/vendor/ios7-switch/ios7-switch.js" + versionstr));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/octopus/assets/vendor/modernizr/modernizr.js" + versionstr,
                       "~/octopus/assets/vendor/jquery/jquery.js" + versionstr));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
             "~/octopus/assets/vendor/jquery-browser-mobile/jquery.browser.mobile.js" + versionstr,
             "~/octopus/assets/vendor/bootstrap/js/bootstrap.js" + versionstr,
             "~/octopus/assets/vendor/nanoscroller/nanoscroller.js" + versionstr,
             "~/octopus/assets/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js" + versionstr,
             "~/octopus/assets/vendor/magnific-popup/magnific-popup.js" + versionstr,
             "~/octopus/assets/vendor/jquery-placeholder/jquery.placeholder.js" + versionstr));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
               "~/octopus/assets/javascripts/tables/examples.datatables.default.js" + versionstr,
               "~/Scripts/datatables.net/js/jquery.dataTables.min.js" + versionstr,
               "~/Scripts/datatables.net/js/dataTables.select.min.js" + versionstr,
               "~/Scripts/datatables.net-bs/js/dataTables.bootstrap.min.js" + versionstr,
               "~/Scripts/datatablebutton/dataTables.buttons.min.js" + versionstr,
               "~/Scripts/datatablebutton/jszip.min.js" + versionstr,
               "~/Scripts/datatablebutton/pdfmake.min.js" + versionstr,
               "~/Scripts/datatablebutton/vfs_fonts.js" + versionstr,
               "~/Scripts/datatablebutton/buttons.html5.min.js" + versionstr,
               "~/Scripts/datatablebutton/buttons.print.min.js" + versionstr));

            bundles.Add(new ScriptBundle("~/bundles/commonscript").Include(
                "~/Scripts/js/moment.js" + versionstr,
                "~/octopus/assets/javascripts/sweetalert.min.js" + versionstr,
                "~/Scripts/jquery.raty.js" + versionstr,
                "~/Scripts/js/date-uk.js" + versionstr,
                "~/octopus/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.js" + versionstr,
                "~/octopus/assets/javascripts/theme.js" + versionstr,
                "~/octopus/assets/vendor/jquery-ui/js/jquery-ui-1.10.4.custom.js" + versionstr,
                "~/octopus/assets/javascripts/theme.init.js" + versionstr,
                "~/octopus/assets/javascripts/svgembedder.min.js" + versionstr,
                "~/octopus/assets/javascripts/ui-elements/examples.modals.js" + versionstr));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
              "~/Scripts/jquery.validate.min.js" + versionstr,
              "~/Scripts/jquery.validate.unobtrusive.min.js" + versionstr));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
              "~/octopus/assets/vendor/bootstrap/css/bootstrap.min.css" + versionstr
             ));

            bundles.Add(new StyleBundle("~/Content/Commoncss").Include(
                 "~/octopus/assets/vendor/font-awesome/css/font-awesome.css" + versionstr,
              "~/octopus/assets/vendor/magnific-popup/magnific-popup.css" + versionstr,
              "~/octopus/assets/vendor/bootstrap-datepicker/css/datepicker3.css" + versionstr));

            bundles.Add(new StyleBundle("~/Content/Vendorcss").Include(
                "~/octopus/assets/vendor/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.css" + versionstr,
                "~/octopus/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.css" + versionstr,
                "~/octopus/assets/vendor/morris/morris.css" + versionstr,
                "~/Scripts/datatables.net-bs/css/dataTables.bootstrap.min.css" + versionstr,
                "~/octopus/assets/stylesheets/jquery.raty.css" + versionstr,
                "~/octopus/assets/stylesheets/theme.css" + versionstr,
                "~/octopus/assets/stylesheets/skins/default.css" + versionstr));

            bundles.Add(new StyleBundle("~/Content/ThemeCustomcss").Include(
                "~/octopus/assets/stylesheets/theme-custom.css" + versionstr,
                "~/octopus/assets/stylesheets/skins/icon-font.min.css" + versionstr,
                "~/octopus/assets/stylesheets/skins/line-awesome.css" + versionstr,
                "~/octopus/assets/stylesheets/sweetalert.min.css" + versionstr));

        }
    }
}
