using NetMvcMusicStory.Admin.Unity.Invoke;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
/*
*  name:hcw
*  time: 2020/12/17
*  content：访问拦截
*  return：string
*/
public class MyFilter1Attribute : System.Web.Mvc.ActionFilterAttribute
{
    //实现接口方法
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //控制器名称
        string controller = filterContext.RouteData.Values["controller"].ToString();
        string action = filterContext.RouteData.Values["action"].ToString();
        string AdminUser = IsSessionNull.Get("AdminUser");//管理员
        string User = IsSessionNull.Get("User");//普通用户
        List<string> list = null;
        string access ="/"+controller+"/"+action;


        //通过就放行
        if (controller.Equals("Home") || controller.Equals("home"))
        {
            base.OnActionExecuting(filterContext);
            return;
        }
        //通过就放行
        if (controller.Equals("AdminLogin") || controller.Equals("adminlogin"))
        {
                base.OnActionExecuting(filterContext);
                return;
        }

        if (User==null&&AdminUser==null)
        {
            filterContext.Result = new System.Web.Mvc.RedirectResult("/Home");
            return;
        }

        if (!string.IsNullOrEmpty(AdminUser))
        {
            if (controller.Equals("AdminHome")|| controller.Equals("HomePage"))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            var menu = Invoke.PostRequestApi("http://localhost:8848/api/GetRuleMenu?Account=" + AdminUser);
            //dynamic rootmenu = JsonConvert.DeserializeObject(menu);
            //list = new List<string>();
            //foreach (var item in rootmenu)
            //{
            //    string menuurl = item.MenuUrl;
            //    list.Add(menuurl);
            //}
            //bool b = list.Contains(access);
            if (menu!=null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }else{
                filterContext.Result = new System.Web.Mvc.RedirectResult("/Home/error");
                return;
            }
        }



        //如果包含以下规则就放行
        if (!string.IsNullOrEmpty(User))
        {
            var menu = Invoke.PostRequestApi("http://localhost:8848/api/GetRuleMenu?Account=" + User);
            dynamic rootmenu = JsonConvert.DeserializeObject(menu);
            list = new List<string>();
            foreach (var item in rootmenu)
            {
                string menuurl = item.MenuUrl;
                list.Add(menuurl);
            }
            bool b = list.Contains(controller);
            if (b)
            {
                base.OnActionExecuting(filterContext);
                return;
            }else{
                filterContext.Result = new System.Web.Mvc.RedirectResult("/Home/error");
                return;
            }
        }
    }





    public static class IsSessionNull
    {
        public static string Get(string SessionName)
        {
            if (HttpContext.Current.Session[SessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[SessionName].ToString();
            }
        }
    }

}





