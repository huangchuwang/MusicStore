﻿using System.Web;
using System.Web.Mvc;

namespace Shoppingcart.MicroService.ServiceInstance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
