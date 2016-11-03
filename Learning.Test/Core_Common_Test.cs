using Learn.Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Test
{
	public class User
	{
		public User(){}
	}
	[TestClass]
	public class Core_Common_Test
	{
		[TestMethod]
		public void Test_Singleton()
		{
			for (int i = 0; i < 10000; i++)
			{
				Task.Run(()=> 
				{
					for (int j = 0; j < 100; j++)
					{
						Singleton.GetInstance<User>();
					}
				});
			}
			Singleton.GetInstance<User>();
		}
	}
}
