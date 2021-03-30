using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace WebAPI.Unity.Filter
{/// <summary>
/// 消息通知aop实现
/// </summary>
    public class Global_aop : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Console.WriteLine("前置");
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Console.WriteLine("后置");
        }
    }
}