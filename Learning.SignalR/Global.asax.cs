using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Learning.SignalR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

			//这个设置是离开一个传输连接开放的时间和等待响应之前关闭它,打开一个新连接。默认值为110秒。
			//这个设置只适用于当keepalive功能被禁用,通常只适用于长轮询传输。
			//GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);

			//GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(30);

			// For transports other than long polling, send a keepalive packet every
			// 10 seconds. 
			// This value must be no more than 1/3 of the DisconnectTimeout value.
			//GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);


			AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
