using System;

namespace Learning.AOP
{
	public interface IPerson
	{
		void Say(string words);
	}

	[MyAOP]
	public class Person : ContextBoundObject, IPerson
	{
		private string _name;
		private int _age;

		public Person(string name):this(name,24)
		{
		}

		public Person(string name,int age)
		{
			_name = name;
			_age = age;
		}

		[Log]
		[Hello]
		public void Say(string words)
		{
			if (string.IsNullOrWhiteSpace(words))
			{
				Console.WriteLine(string.Format("{0} said nothing"));
			}
			Console.WriteLine(string.Format("{0} said {1}",_name,words));
		}
	}
}
