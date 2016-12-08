using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.luceneNet
{
	class Program
	{
		static void Main(string[] args)
		{
			//var message = new MessageEntity()
			//{
			//	Title="这是标题党",
			//	Content= "没有什么好看的没有什么好看的没有什么好看的没有什么好看的没有什么好看的没有什么好看的没有什么好看的没有什么好看的没有什么好看的这里有干货这里有干货这里有干货这里有干货这里有干货这里有干货这里有干货这里有干货人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人人比人气死人"
			//};
			//IndexContext.GetInstance().Write(message,true);

			var totalCount = 0;
			var list=IndexContext.GetInstance().Retrieve<MessageSearch>("干货",out totalCount);

			if (list != null)
			{
				list.ToList().ForEach(l=> 
				{
					Console.WriteLine(l.Title);
					Console.WriteLine(l.Content);
				});
			}

			Console.Read();
		}
	}
}
