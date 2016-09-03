using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MyAOPAttribute : ContextAttribute, IContributeObjectSink
	{
		public MyAOPAttribute() : base("MyAOP")
		{
			Console.WriteLine("MyAOP begin");
		}

		public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
		{
			//先添加后执行
			return nextSink.AddLogSink()
									.AddHelloSink();
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class LogAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class HelloAttribute : Attribute
	{
	}

	public class LogSink : IMessageSink
	{
		private IMessageSink _sink;
		public IMessageSink NextSink
		{
			get
			{
				return _sink;
			}
		}

		public LogSink(IMessageSink sink)
		{
			_sink = sink;
		}

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return null;
		}

		public IMessage SyncProcessMessage(IMessage msg)
		{
			try
			{
				var call = msg as IMethodCallMessage;

				if (Attribute.GetCustomAttribute(call.MethodBase, typeof(LogAttribute)) == null)
				{
					return _sink.SyncProcessMessage(msg);
				}
				Console.WriteLine("log begin");
				var returnMsg = _sink.SyncProcessMessage(msg);
				Console.WriteLine("log end");
				return returnMsg;
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("error：{0}",ex.ToString()));
				return _sink.SyncProcessMessage(msg);
			}
		}
	}

	public class HelloSink : IMessageSink
	{
		private IMessageSink _sink;
		public IMessageSink NextSink
		{
			get
			{
				return _sink;
			}
		}

		public HelloSink(IMessageSink sink)
		{
			_sink = sink;
		}

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return null;
		}

		public IMessage SyncProcessMessage(IMessage msg)
		{
			try
			{
				var call = msg as IMethodCallMessage;

				if (Attribute.GetCustomAttribute(call.MethodBase, typeof(HelloAttribute)) == null)
				{
					return _sink.SyncProcessMessage(msg);
				}
				Console.WriteLine("hello");
				var returnMsg = _sink.SyncProcessMessage(msg);
				Console.WriteLine("goodbye");
				return returnMsg;
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("error：{0}", ex.ToString()));
				return _sink.SyncProcessMessage(msg);
			}
		}
	}

	public static class SinkChain
	{

		public static LogSink AddLogSink(this IMessageSink sink)
		{
			return new LogSink(sink);
		}

		public static HelloSink AddHelloSink(this IMessageSink sink)
		{
			return new HelloSink(sink);
		}
	}

}
