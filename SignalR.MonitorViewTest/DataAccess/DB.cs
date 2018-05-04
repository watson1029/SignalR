using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.MonitorViewTest.DataAccess
{
    public class DB
    {
        public static string Connect
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DbConnect"];
            }
        }
    }
}