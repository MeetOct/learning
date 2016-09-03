using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
	public class MyProxy<T> : RealProxy
	{
		private T _target;
		public MyProxy(T target):base(typeof(T))
		{
			_target = target;
		}
		public override IMessage Invoke(IMessage msg)
		{
			var call = msg as IMethodCallMessage;
			if (call == null)
			{
				Console.WriteLine("error");
			}
			Console.WriteLine("before proxy");
			var result = call.MethodBase.Invoke(this._target,call.Args);
			Console.WriteLine("end proxy");

			return new ReturnMessage(result,new object[0],0,null,call);
		}
	}

	public class Container
	{
		//public Type type { get; set; }
		//public BindingFlags flags { get; set; }
		//public Binder binder { get; set; }
		public List<object> args { get; set; }=new List<object>();
		//public CultureInfo cutureInfo { get; set; }
	}

	public static class Singleton
	{
		public static Container GetContainer()
		{

			return new Container() { };
		}

		/// <summary>
		/// 构造函数参数(warning：参数添加顺序必须一致)
		/// </summary>
		/// <param name="sp"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Container AddParameter(this Container sp,object value)
		{
			sp.args.Add(value);
			return sp;
		}

		public static T Create<T>(this Container sp)
		{
			var result= (T)Activator.CreateInstance(typeof(T), sp.args.ToArray());
			return (T)new MyProxy<T> (result).GetTransparentProxy();
		}
	}
}
