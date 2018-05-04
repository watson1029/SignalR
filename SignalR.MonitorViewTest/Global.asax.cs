using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalR.MonitorViewTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 启动SqlDependency数据监控
            SqlDependency.Start(DataAccess.DB.Connect);
        }

        protected void Application_End()
        {
            // 关闭SqlDependency数据监控
            SqlDependency.Stop(DataAccess.DB.Connect);
        }
    }
}
