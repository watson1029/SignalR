using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalR.MonitorViewTest.DataAccess
{
    public class SignalData
    {
        public IEnumerable<Models.SignalData> GetSignalData()
        {
            using (var sqlConnection = new SqlConnection(DB.Connect))
            {
                using (var sqlCommand = new SqlCommand(@"select [SignalID], [SignalName], [SignalContent], [SignalMessage], [SignalRemark] from [dbo].[SignalData]", sqlConnection))
                {
                    //sqlCommand.Notification = null;
                    //var dependency = new SqlDependency(sqlCommand);
                    //dependency.OnChange += new OnChangeEventHandler(SignalData_OnChange);
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                        sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        return reader.Cast<IDataRecord>().Select(a => new Models.SignalData()
                        {
                            SignalID = Convert.ToInt32(a["SignalID"]),
                            SignalName = a["SignalName"].ToString(),
                            SignalContent = a["SignalContent"].ToString(),
                            SignalMessage = a["SignalMessage"].ToString(),
                            SignalRemark = a["SignalRemark"].ToString()
                        }).ToList();
                    }
                }
            }
        }
    }
}