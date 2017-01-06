using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
  [AttributeUsage(AttributeTargets.Class)]
   public class MyProxyAttribute : ProxyAttribute
	{
		private List<Type> _types; //拦截器
		public MyProxyAttribute(params Type[] types)
		{
			_types = types?.ToList();
		}
		public MyProxyAttribute()
		{
		}
		public override MarshalByRefObject CreateInstance(Type serverType)
		{
			System.Console.WriteLine("Start！");
			var target= base.CreateInstance(serverType);
			List<IIntercept> intercepts = null;
			if (_types!=null)
			{
				intercepts = new List<IIntercept>();
				intercepts.AddRange(_types.Select(s =>
				{
					return (IIntercept)Activator.CreateInstance(s);
				}));

			} 
			var myProxy = new MyProxy(target, serverType, intercepts?.ToArray());
			return (MarshalByRefObject)myProxy.GetTransparentProxy();
		}
	}
}
