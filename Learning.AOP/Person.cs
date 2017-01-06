using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Learning.AOP
{
	public interface IPerson
	{
		void Say(string words);
	}

	[MyAOP]
	//[MyProxy(typeof(LogIntercept), typeof(TimeIntercept))]
	public class Person : Component
	{
		private string _name;
		private int _age;

		public Person() : this("hance", 24)
		{
			Console.WriteLine("构造函数");
		}

		public Person(string name):this(name,24)
		{
		}

		public Person(string name,int age)
		{
			_name = name;
			_age = age;
		}

		[Log]
		public void Say(string words)
		{
			if (string.IsNullOrWhiteSpace(words))
			{
				Console.WriteLine(string.Format("{0} said nothing"));
			}
			Console.WriteLine(string.Format("{0} said {1}",_name,words));
		}

		public void Log()
		{
			Console.WriteLine("log");
		}
	}
}
