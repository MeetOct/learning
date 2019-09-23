using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Common
{
	public static class Singleton
	{
		private static readonly ConcurrentDictionary<Type, object> currentdic = new ConcurrentDictionary<Type, object>(); 
		//private static readonly Dictionary<Type, object> dic = new Dictionary<Type, object>();
		public static T GetInstance<T>()
		{
			//若value已加入，其余创建了的对象会被丢弃
			return (T)currentdic.GetOrAdd(typeof(T), (t) => Activator.CreateInstance(t));

			#region 想得太多
			//object result = null;

			//if (dic.TryGetValue(typeof(T), out result))
			//{
			//	return (T)result;
			//}
			//lock (syncObject)
			//{
			//	if (dic.TryGetValue(typeof(T), out result))
			//	{
			//		return (T)result;
			//	}
			//	result = Activator.CreateInstance<T>();
			//	dic.Add(typeof(T), result);
			//	return (T)result;
			//}
			#endregion
		}
	}
}
