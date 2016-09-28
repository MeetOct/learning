using Learning.SignalR.Filters;
using Learning.SignalR.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.SignalR.Hubs
{
	[HubName("chatHub")]
	public class ChatHub : Hub
	{b
		private static ConcurrentDictionary<string, ChatUser> currentUser = new ConcurrentDictionary<string, ChatUser>();

		//这个需要修改成单例（也要保证线程安全）
		private static BadWordsFilter filter = new BadWordsFilter(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "badworlds.txt"));

		public void Register(string name,string id)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				Clients.Client(Context.ConnectionId).error("昵称不能为空~");
				return;
			}
			name = name.Trim();
			if (filter.HasBadWord(name))
			{
				Clients.Client(Context.ConnectionId).error("昵称包含敏感词~");
				return;
			}
			if (currentUser.Any(c => c.Value.Name.Equals(name)))
			{
				Clients.Client(Context.ConnectionId).error("昵称已存在，重新取一个DA☆ZE~");
				return;
			}
			currentUser.TryAdd(id, new ChatUser() { Name=name});
			Clients.All.broadcast(string.Format("{0}加入了聊天室，请大家尽情调戏他~~", name));
			Clients.Client(Context.ConnectionId).closeRegister();
		}

		public void Broadcast(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}
			if (filter.HasBadWord(message))
			{
				Clients.Client(Context.ConnectionId).error("信息包含敏感词~");
				return;
			}
			var user = new ChatUser();
			if (this.GetUser(out user))
			{
				Clients.Client(Context.ConnectionId).ok();
				Clients.All.broadcast(string.Format("{0}：{1}",user.Name, message));
			}
		}

		/// <summary>
		/// 断开连接？
		/// </summary>
		/// <param name="stopCalled">当stopCalled为true时，真正断开,为false时，timeout</param>
		/// <returns></returns>
		public override Task OnDisconnected(bool stopCalled)
		{
			var user = new ChatUser();
			if (currentUser.TryRemove(Context.ConnectionId, out user))
			{
				Clients.All.broadcast(string.Format("{0}离开了聊天室~~", user.Name));
				
			}
			return base.OnDisconnected(stopCalled);
		}

		public  override Task OnReconnected()
		{
			return base.OnReconnected();
		}

		private bool GetUser(out ChatUser user)
		{
			if (!currentUser.TryGetValue(Context.ConnectionId, out user))
			{
				Clients.Client(Context.ConnectionId).error("出了点小问题，orz~");
				return false;
			}
			return true;
		}
	}
}