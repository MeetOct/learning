using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
	public interface IIntercept
	{
		void Do();
	}
	public class LogIntercept : IIntercept
	{
		public void Do()
		{
			Console.WriteLine("LogIntercept：Log");
		}
	}

	public class TimeIntercept : IIntercept
	{
		public void Do()
		{
			Console.WriteLine(string.Format("TimeIntercept：当前时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
		}
	}
}
