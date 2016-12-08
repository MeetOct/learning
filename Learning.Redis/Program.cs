using ServiceStack.Redis;
using System;
using System.Configuration;

namespace Learning.Redis
{
	class Program
	{
		static string host = ConfigurationManager.AppSettings["Host"];
		//static string 
		static void Main(string[] args)
		{
			//using (var redis = RedisManager.GetClient())
			//{
			//	var result = redis.Get<string>("hi");
			//	Console.WriteLine(result);

			//	redis.Set<string>("hance","ce");

			//	result = redis.Get<string>("hance");
			//	Console.WriteLine(result);
			//}


			//using (RedisUnitOfWork uof = new RedisUnitOfWork(RedisManager.GetClient()))
			//{
			//	try
			//	{
			//		uof.TranCommand(e => e.Set<string>("hi", "hello"));
			//		uof.TranCommand(e => e.Set<string>("hance", "cece"));
			//		uof.Commit();
			//	}
			//	catch (Exception)
			//	{
			//		uof.Roolback();
			//		throw;
			//	}
			//}

			using (var redis = RedisManager.GetClient())
			{
				var result = redis.Get<string>("hi");
				Console.WriteLine(result);

				result = redis.Get<string>("hance");
				Console.WriteLine(result);
			}

			Console.ReadKey();
		}
	}
}
