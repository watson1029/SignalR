using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.IM.Hubs
{
    [HubName("imHub")]
    public class IMHub : Hub
    {
        [HubMethodName("hello")]
        public void Hello()
        {
            Clients.All.welcome("Welcome to SignalR IM.");
        }
        [HubMethodName("send")]
        public void Send(string name, string content)
        {
            //回调前端JS函数
            Clients.All.sendMessage(name, content);
        }
    }
}