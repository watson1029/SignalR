using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 监控方法：表名
/// 数据变化事件：表明_OnChange
/// </summary>
namespace SignalR.Monitor
{
    public class MonitorInit : DbBase
    {
        public static void SignalData()
        {
            using (var sqlConnection = new SqlConnection(Connect))
            {
                using (var sqlCommand = new SqlCommand(@"select [SignalID], [SignalName], [SignalContent], [SignalMessage], [SignalRemark] from [dbo].[SignalData]", sqlConnection))
                {
                    sqlCommand.Notification = null;
                    var dependency = new SqlDependency(sqlCommand);
                    dependency.OnChange += new OnChangeEventHandler(SignalData_OnChange);
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                        sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        // 监控成功
                    }
                }
            }
        }

        public static void SignalData_OnChange(object sender, SqlNotificationEventArgs e)
        {
            // 监控到数据发生改变
            // 调用SignalR进行广播
            Hubs.DataMonitor.MonitorSignalData();
        }
    }
}
