﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

/// <summary>
/// 方法定义规则
/// 方法名：Monitor+需要监控的数据库表名
/// CallBack函数：方法名+CallBack
/// </summary>
namespace SignalR.Monitor.Hubs
{
    [HubName("dataMonitor")]
    public class DataMonitor : Hub
    {
        private static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DataMonitor>();

        [HubMethodName("monitorConnection")]
        public void MonitorConnection()
        {
            // 添加对数据表SignalData的监控
            MonitorInit.SignalData();
            context.Clients.All.monitorConnectionCallBack("Monitor Connection Success.");
        }
        
        [HubMethodName("monitorSignalData")]
        public static void MonitorSignalData()
        {
            // 对定义了回调函数的应用进行广播
            context.Clients.All.monitorSignalDataCallBack();
            // 继续监控
            MonitorInit.SignalData();
        }
    }
}
