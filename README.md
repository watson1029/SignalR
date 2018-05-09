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
        Install-Package Microsoft.AspNet.SignalR
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
## 4. 添加Hubs文件
```CSharp
[HubMethodName("send")]
public void Send(string name, string content)
{
    //回调前端JS函数
    Clients.All.sendMessage(name, content);
}
```
## 5. 引用js文件
```JavaScript
<script src="~/Scripts/jquery.signalR-2.2.3.min.js"></script>
<script src="~/signalr/hubs"></script>
```
## 6. 前端js获取消息
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

---

# SignalR.Notify 消息通知
## 1. 添加NuGet引用
        Install-Package Microsoft.AspNet.SignalR
## 2. 添加Startup.cs
```CSharp
[assembly: OwinStartup(typeof(SignalR.Notify.App_Start.Startup))]
namespace SignalR.Notify.App_Start
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
## 4. 添加Hubs文件
```CSharp
[HubMethodName("flyPlanNotify")]
public static void FlyPlanNotify()
{
    context.Clients.All.flyNotify("Here is fly plan notify.");
}
```
## 5. 引用js文件
```JavaScript
<script src="~/Scripts/jquery.signalR-2.2.3.min.js"></script>
<script src="~/signalr/hubs"></script>
```
## 6. 前端js获取消息
```JavaScript
// 定义NotifyHub对象
var hub = $.connection.notifyHub;

// 连接NotifyHub对象
$.connection.hub.start()；

// 定义IMHub.sendMessage的回调函数
im.client.flyNotify = function (name, message) {
    // 处理脚本
}

$('#send').click(function () {
    // 调用后台Hub方法
    im.server.flyPlanNotify($('#username').val(), $('#message').val());
});
```
## 7. 前端和后端调用Hub的区别
后端待用要使用静态方法，但是前端js调用不能获取静态方法的脚本，所以两种调用方法要区分开。

---

# SignalR.Monitor 剥离业务场景，监控数据变化
## 1. 添加NuGet引用
        Install-Package Microsoft.AspNet.SignalR
## 2. 数据库开启代理
```Sql
// 设置某个数据库代理的回滚
ALTER DATABASE [DBName] SET NEW_BROKER WITH ROLLBACK IMMEDIATE;
// 设置某个数据库的代理
ALTER DATABASE [DBName] SET ENABLE_BROKER;
// 查询某个数据库是否已经启动了代理
// is_broker_enabled 为0表示未启动代理
// is_broker_enabled 为1表示已启动代理
SELECT is_broker_enabled FROM sys.databases WHERE name = '[DBName]'
```
### 3. MonitorInit
添加对应表的监控代码
```CSharp
// 添加SqlDependency进行数据监控
var dependency = new SqlDependency(sqlCommand);
dependency.OnChange += new OnChangeEventHandler(SignalData_OnChange);
```
添加数据变化事件
```CSharp
/// <summary>
/// 监控到数据发生改变
/// </summary>
public static void SignalData_OnChange(object sender, SqlNotificationEventArgs e)
{
    // 调用SignalR进行广播
    Hubs.DataMonitor.MonitorSignalData();
}
```
### 4. 添加Hubs文件
```CSharp
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
```
### 5. 应用调用监控的方法
Global.asax添加对SqlDependency的支持
```CSharp
protected void Application_Start()
{
    // 启动SqlDependency数据监控
    SqlDependency.Start(DataAccess.DB.Connect);
}

protected void Application_End()
{
    // 关闭SqlDependency数据监控
    SqlDependency.Stop(DataAccess.DB.Connect);
}
```
其余Nuget引用、Startup添加、前端js回调函数等应用和前面IM，Notify的一致，这里就不重复了。
