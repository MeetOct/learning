using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Reflection;

namespace Learning.Castle.Windsor
{
	public class MyWindsorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{

			//  where non-abstract classes only are to be considered use Castle.MicroKernel.Registration.Classesclass instead
			container.Register(Classes.FromThisAssembly()
													.Where(t=>t.Name.EndsWith("Service"))
													.WithService
													.AllInterfaces()
													.LifestyleTransient());
		}
	}

	public class TypesWindsorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				 Component.For<PersonInterceptor>().LifestyleTransient(),
				Component.For<IType>()
							.ImplementedBy<PersonType>()
							);

			//仍然不知道Types注册什么
			container.Register(Types.FromThisAssembly()
												.Where(t => t.Name.EndsWith("Type"))
												//.Configure(conf => conf.)
												.WithService
												.AllInterfaces()
												.LifestyleTransient());

		}
	}
}
