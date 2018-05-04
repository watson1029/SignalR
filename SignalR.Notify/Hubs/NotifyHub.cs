using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalR.Notify.Hubs
{
    public class NotifyHub : Hub
    {
        private static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
        /// <summary>
        /// 成功连接Hubs
        /// </summary>
        public static void Hello()
        {
            context.Clients.All.welcome("Welcome to SignalR Notify.");
        }

        /// <summary>
        /// 飞行计划审批提示
        /// </summary>
        public static void FlyPlanNotify()
        {
            context.Clients.All.flyNotify("Here is fly plan notify.");
        }

        /// <summary>
        /// 飞行计划审批提示
        /// </summary>
        public void FlyPlanNotifyJs()
        {
            context.Clients.All.flyNotify("Here is fly plan notify.");
        }

        /// <summary>
        /// 长期计划审批提示
        /// </summary>
        public static void RepetPlanNotify()
        {
            context.Clients.All.repetNotify("Here is repet plan notify.");
        }

        /// <summary>
        /// 长期计划审批提示
        /// </summary>
        public void RepetPlanNotifyJs()
        {
            context.Clients.All.repetNotify("Here is repet plan notify.");
        }
    }
}