using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
	class Program
	{

		static void Main(string[] args)
		{
			var hance = new Person("hance", 24);
			hance.Say("hello");

			var hance2 = ActivatorContainer.GetContainer()
															.AddParameter("hance2")
															.AddParameter(24)
															.Create<Person>();
			hance2.Say("hello Proxy");

			Console.Read();
		}
	}
}
