使用SignalR实现IM和消息推送Demo
=====
## 解决方案
* SignalR.IM 即时通讯
* SignalR.Notify 消息通知
* SignalR.Monitor 剥离业务场景，监控数据变化
* SignalR.MonitorViewTest 监控数据变化的测试页面
---
# SignalR.IM 即时通讯
## 1. 添加NuGet引用
        Install-Package Watson.Base.DotNetCore
## 2. 添加Startup.cs
```CSharp
[assembly: OwinStartup(typeof(SignalR.Notify.App_Start.Startup))]
namespace SignalR.IM.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 注册SignalR
            app.MapSignalR();
        }
    }
}
```
## 3. Hubs文件的命名
注意使用驼峰命名法，否则会报错。在未指定类名和方法名的情况下，生成hubs脚本的时候会自动将帕斯卡命名法改成驼峰命名法。
## 4 添加Hubs文件
```CSharp
[HubMethodName("send")]
public void Send(string name, string content)
{
    //回调前端JS函数
    Clients.All.sendMessage(name, content);
}
```
## 5.引用js文件
```JavaScript
<script src="~/Scripts/jquery.signalR-2.2.3.min.js"></script>
<script src="~/signalr/hubs"></script>
```
## 6.前端js获取消息
```JavaScript
// 定义IM.Hub对象
var im = $.connection.imHub;

// 连接IM.Hub对象
$.connection.hub.start()；

// 定义IMHub.sendMessage的回调函数
im.client.sendMessage = function (name, message) {
    // 处理脚本
}

$('#send').click(function () {
    // 调用后台Hub方法
    im.server.send($('#username').val(), $('#message').val());
});
```
