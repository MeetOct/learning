using ServiceStack.Redis;
using System.Configuration;

namespace Learning.Redis
{
	public class RedisManager
	{
		private static PooledRedisClientManager clientManager;
		private static object obj=new object();
		private static string host = ConfigurationManager.AppSettings["Host"];

		private static void CreateManager()
		{
			clientManager = new PooledRedisClientManager(host);
		}

		public static IRedisClient GetClient()
		{
			if (clientManager == null)
			{
				lock (obj)
				{
					if (clientManager == null)
					{
						CreateManager();
					}
				}
			}
			return clientManager.GetClient();
		}
	}
}
