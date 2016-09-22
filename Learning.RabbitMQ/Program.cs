using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Text;

namespace Learning.RabbitMQ
{
	class Program
	{
		
		private static string GetMessage(string args)
		{
			return ((args.Length > 0) ? string.Join(" ", args) : "Hance said: ");
		}

		static void Main(string[] args)
		{
			//http://www.rabbitmq.com/tutorials/tutorial-one-python.html

			#region RPC

			//http://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html

			#endregion

			#region MQ
			//ConnectionFactory factory = new ConnectionFactory() { HostName="localhost"};
			//using (var conn=factory.CreateConnection())
			//{
			//	using (var channel=conn.CreateModel())
			//	{

			//		channel.ExchangeDeclare("topic_logs", "topic");
			//		string msg = "start!";
			//		var body = Encoding.UTF8.GetBytes(msg);
			//		var severity = "start.info";
			//		channel.BasicPublish(exchange: "topic_logs",
			//										routingKey: severity,
			//										basicProperties: null,
			//										body: body);
			//		Console.WriteLine(" [X] Send {0} : {1}", severity, msg);
			//		while (true)
			//		{
			//			Console.WriteLine(" [X] Input Exit to exit.");
			//			msg = Console.ReadLine();
			//			body = Encoding.UTF8.GetBytes(GetMessage(msg));
			//			var propertites = channel.CreateBasicProperties();
			//			propertites.Persistent = true;
			//			severity = severity.Equals("han.info") ? "ce.info" : "han.info";
			//			Console.WriteLine(" [X] Send {0} : {1}", severity, msg);
			//			channel.BasicPublish(exchange: "topic_logs",
			//									routingKey: severity,
			//									basicProperties: null,
			//									body: body);
			//			if (msg.Equals("Exit"))
			//			{
			//				break;
			//			}
			//		}
			//	}
			//} 
			#endregion
		}
	}
}
