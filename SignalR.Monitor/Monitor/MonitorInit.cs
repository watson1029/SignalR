using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            }
        }

        public static void SignalData_OnChange(object sender, SqlNotificationEventArgs e)
        {
            Hubs.DataMonitor.MonitorSignalData();
        }
    }
}
