using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Learning.SignalR.Hubs
{
	[HubName("chatHub")]
	public class ChatHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}

		public void Say(string message)
		{
			Clients.All.say(message);
		}
	}
}