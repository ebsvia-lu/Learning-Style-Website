using System.Web;
using System.Web.Optimization;

namespace UOS.LearningStyle.FinalYear.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/qTip/jquery.qtip.js",
                "~/Scripts/moment.js",
                "~/Scripts/fullcalendar*",
                "~/Scripts/locale-all.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/Game1/alert.js",
                "~/Scripts/Game1/app.js",
                "~/Scripts/Game1/script.js"
              
                ));
            bundles.Add(new ScriptBundle("~/bundless/scripts").Include(
                "~/Scripts/Game3/Flashcards.js"
                ));
            bundles.Add(new ScriptBundle("~/bundlesss/scripts").Include(
                "~/Scripts/Game2/memorygame2.js",
                "~/Scripts/Game2/Event.js"

 ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/course/bootstrap.css",
                      "~/Content/course/site.css"
                
                    ));

            bundles.Add(new StyleBundle("~/Contentssss/css").Include(
                      "~/Content/Game1/alert.css",
                      "~/Content/Game1/fonts.css",
                      "~/Content/Game1/style.css"
));

            bundles.Add(new StyleBundle("~/Contents/css").Include(
                "~/Content/Game3/Flashcards.css"
                ));

            bundles.Add(new StyleBundle("~/Contentss/css").Include(
                "~/Content/Game2/memorygame2.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                //"~/Content/calendar/bootstrap.css",
                //"~/Content/calendar/site.css",
                "~/Content/calendar/themes/flat/jquery-ui-{version}.css",
                "~/Content/calendar/fullcalendar.css"
                
                ));
        }
    }
}
