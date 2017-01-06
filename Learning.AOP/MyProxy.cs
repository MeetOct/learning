using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
	public class MyProxy: RealProxy
	{
		private object _target; //目标代理类
		private List<IIntercept> _intercepts; //拦截器
		public MyProxy(object target, Type type,params IIntercept[] intercepts) : base(type)
		{
			_target = target;
			_intercepts = intercepts!=null? intercepts.ToList():null;
		}
		public override IMessage Invoke(IMessage msg)
		{
			var ctr = msg as IConstructionCallMessage;
			if (ctr != null)
			{
				Console.WriteLine("ctr");
				RealProxy _proxy = RemotingServices.GetRealProxy(this._target);
				_proxy.InitializeServerObject(ctr);
				MarshalByRefObject tp = (MarshalByRefObject)this.GetTransparentProxy();
				return EnterpriseServicesHelper.CreateConstructionReturnMessage(ctr, tp);
			}

			if(_intercepts!=null)
			{
				foreach (var _intercept in _intercepts)
				{
					_intercept.Do();
				}
			}
			var call = msg as IMethodCallMessage;
			Console.WriteLine(string.Format("proxy method:{0}", call.MethodName));
			var result = call.MethodBase.Invoke(this._target,call.Args);
			return new ReturnMessage(result,new object[0],0,null,call);
		}
	}


	public static class ActivatorContainer
	{
		public static T Create<T>(params IIntercept[] intercepts)
		{
			var result= Activator.CreateInstance(typeof(T));

			var myProxy = new MyProxy(result, typeof(T), intercepts);
			return (T)myProxy.GetTransparentProxy();
		}
	}
}
