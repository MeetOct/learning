using Learn.Core;
using Learning.Redis.Attributes;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Redis.Extensions
{
	public static class RedisHashExtension
	{

		public static void StoreHashEntity<T>(this IRedisClient client, string key, T entity)
		{
			var keyValuePairs = new List<KeyValuePair<string, string>>();
			PropertyInfo[] properties = typeof(T).GetProperties();

			foreach (var property in properties)
			{
				if (property.IsDefined(typeof(HashAttribute), false))
				{
					var getter=ReappearMember.CreatePropertyGetter(property);
					keyValuePairs.Add(new KeyValuePair<string, string>(property.Name, getter(entity)?.ToString()));
				}
			}
			client.SetRangeInHash(key, keyValuePairs);
		}

		public static T GetHashEntity<T>(this IRedisClient client, string key)where T : class, new()
		{
			var entity = new T();
			PropertyInfo[] properties = typeof(T).GetProperties();
			var values = client.GetAllEntriesFromHash(key);
			foreach (var property in properties)
			{
				if (property.IsDefined(typeof(HashAttribute), false)&& values.ContainsKey(property.Name))
				{
					var setter = ReappearMember.CreatePropertySetter(property);
					//这里其实可能有很多类型需要判断
					if (property.PropertyType.IsValueType)
					{
						setter(entity, Convert.ToInt32(values[property.Name]));
					}
					else
					{
						setter(entity, values[property.Name]);
					}
				}
			}
			return entity;
		}
	}
}
