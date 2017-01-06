using Learning.Redis.Extensions;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
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
			//var redis = RedisManager.GetClient();

			//var user=redis.GetHashEntity<User>("users:1");

			//var user = redis.GetFromHash<User>("users:1");  这个是什么？

			//long id = redis.IncrementValue("ids:user");
			//if (id > 0)
			//{
			//	var uid = $"users:{id}";
			//	redis.StoreHashEntity(uid, new User() { Age = 42, Name = "大叔" });
			//}

			Console.ReadKey();
		}
	}
}
