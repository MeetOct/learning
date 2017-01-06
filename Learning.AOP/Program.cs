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
			var hance = new Person();
			hance.Say("hello");
			Console.Read();
		}
	}
}
