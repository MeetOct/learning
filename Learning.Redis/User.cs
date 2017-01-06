using Learning.Redis.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Redis
{
	public class User
	{
		[Hash]
		public string Name { get; set; }

		[Hash]
		public int Age { get; set; }
	}
}
