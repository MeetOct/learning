using Castle.Core;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Castle.Windsor
{
	public interface IPersonService
	{
		void SaySomething(string words);
	}
	public class PersonService : IPersonService
	{
		private string _name = "stranger";

		public PersonService()
		{ }

		public PersonService(string name)
		{
			_name = name;
		}

		public void SaySomething(string words)
		{
			if(string.IsNullOrWhiteSpace(words))
			{
				Console.WriteLine(string.Format("{0} said nothing"),_name);
			}

			Console.WriteLine(string.Format("{0} said {1}",_name,words));
		}
	}

	public interface IType
	{
		void SaySomething(string words);
	}

	[Interceptor(typeof(PersonInterceptor))]
	public class PersonType: IType
	{
		private string _name = "type_stranger";

		public PersonType()
		{ }
		public PersonType(string name)
		{
			_name = name;
		}
		public void SaySomething(string words)
		{
			if (string.IsNullOrWhiteSpace(words))
			{
				Console.WriteLine(string.Format("{0} said nothing"), _name);
			}

			Console.WriteLine(string.Format("{0} said {1}", _name, words));
		}
	}

	public class PersonInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			Console.WriteLine("interceptor begin");
			//Console.WriteLine("doing something");
			invocation.Proceed();
			Console.WriteLine("interceptor end");
		}
	}

	public interface IService
	{
		void SaySomething();
	}

	public class Service: IService
	{
		private IPersonService _service;
		private IType _type;
		public Service(IPersonService service, IType type)
		{
			_service = service;
			_type = type;
		}

		public void SaySomething()
		{
			_service.SaySomething("hello");
			_type.SaySomething("bye");
		}
	}
}
