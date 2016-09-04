using Castle.Windsor;
using Castle.Windsor.Installer;
using Learning.Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Castle
{
	class Program
	{

		static void Main(string[] args)
		{
			// application starts...
			var container = new WindsorContainer();

			// adds and configures all components using WindsorInstallers from executing assembly
			container.Install(FromAssembly.This());

			// instantiate and configure root component and all its dependencies and their dependencies and...

			//var dic = new Dictionary<string, string>();
			//dic.Add("name", "hance");
			//var service = container.Resolve<IPersonService>(dic);
			//service.SaySomething("hello");

			//var typeservice = container.Resolve<IType>(dic);
			//typeservice.SaySomething("bye");

			var service = container.Resolve<IService>();
			service.SaySomething();

			Console.Read();
		}
	}
}
