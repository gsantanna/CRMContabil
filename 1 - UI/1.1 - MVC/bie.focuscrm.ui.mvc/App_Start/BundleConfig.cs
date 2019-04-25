using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace bie.focuscrm.ui.mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {









            // Adicionando Validação, e Globalização
            // Utilizando ordenação manual
            var valBundle = new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
              "~/Content/lib/jquery.validation.globalize/globalize.js",
              "~/Content/lib/jquery.validation.globalize/cultures/globalize.culture.pt-BR.js",
              "~/Scripts/validacao-data.js"
              );

            valBundle.Orderer = new AsIsBundleOrderer();

            bundles.Add(valBundle);








            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }





        public class AsIsBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }


    }
}
