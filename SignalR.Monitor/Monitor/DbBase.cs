using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 可配置分布式分布式数据库
/// </summary>
namespace SignalR.Monitor
{
    public class DbBase
    {
        protected internal static string Connect
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DbConnect"];
            }
        }
    }
}
