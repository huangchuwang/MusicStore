using System.Web;
using System.Web.Optimization;

namespace NetMvcMusicStory.Client
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //----------------------------Home-----------------------

            bundles.Add(new StyleBundle("~/Content/ClientHome/css").Include(
                    "~/Content/Ui/Home/css/pageloader.css",
                    "~/Content/Ui/Home/Swiper/swiper.css",
                    "~/Content/Ui/Home/css/MyStyle.css",
                    "~/Content/Ui/Home_plus/boostrap/bootstrap-4.5.0/css/bootstrap.css",
                    "~/Content/Ui/Home_plus/testStyle/Selection.css",
                    "~/Content/Ui/Home_plus/search/c1.css"
                    ));

                bundles.Add(new StyleBundle("~/Content/ClientHome/js").Include(
                "~/Content/Ui/Home/js/jquery-3.4.1.js",
                "~/Content/Ui/Home/boostrap/bootstrap.bundle.js",
                "~/Content/Ui/Home/boostrap/bootstrap.js",
                "~/Content/Ui/Home/Swiper/swiper.js",
                "~/Content/Ui/Home/js/lazyload.js",
                "~/Content/Ui/Home/js/external.js",
                "~/Content/pu_static/jquery.cookie.min1.4.1.js",
                "~/Content/Ui/Home/js/pageloader.js",
                "~/Content/Ui/Home/js/external.js"
                ));


            //主页尾部
                bundles.Add(new StyleBundle("~/Content/ClientHomes/js").Include(
                "~/Content/Ui/Home/js/external.js",
                "~/Content/pu_static/jquery.cookie.min1.4.1.js"

                ));


            bundles.Add(new StyleBundle("~/Content/Layout/js").Include(
            "~/Content/pu_static/jquery.cookie.min1.4.1.js"

            ));




            //------------------------------Admin静态资源引入----------------------------------------------------
            bundles.Add(new StyleBundle("~/Content/Home/css").Include(
           "~/Content/layuimini/images/favicon.ico",
           "~/Content/layuimini/lib/layui-v2.5.5/css/layui.css",
           "~/Content/layuimini/css/layuimini.css?v=2.0.4.2",
           "~/Content/layuimini/css/themes/default.css",
           "~/Content/layuimini/lib/font-awesome-4.7.0/css/font-awesome.min.css"
           ));

            //css
            bundles.Add(new StyleBundle("~/Content/layui/css").Include(
          "~/Content/layuimini/lib/layui-v2.5.5/css/layui.css",
          "~/Content/layuimini/css/public.css"
         ));

            //js
            bundles.Add(new StyleBundle("~/Content/layui/js").Include(
            "~/Content/layuimini/lib/layui-v2.5.5/layui.js",
            "~/Content/layuimini/js/lay-config.js"
           ));


            //Login
            bundles.Add(new StyleBundle("~/Content/login/css").Include(
            "~/Content/layuimini/login/style.css"
           ));







        }
    }
}
