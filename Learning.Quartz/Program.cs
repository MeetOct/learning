using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Quartz
{
	class Program
	{
		static void Main(string[] args)
		{

			var list=Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterface("IJob") != null);
			Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
			scheduler.Start();

			IJobDetail job = JobBuilder.Create<TimeJob>()
														.WithIdentity("time")
														.StoreDurably()
														.Build();

			IJobDetail job2 = JobBuilder.Create<HelloJob>()
											.WithIdentity("hello")
											.StoreDurably()  //Whether or not the job should remain stored after it is orphaned（是否可以不依赖于Trigger而独立存在）
											.Build();


			//StartNow将当前时间赋值给DateTimeOffset，而不是产生标志，orz
			ITrigger trigger = TriggerBuilder.Create().StartNow().WithIdentity("time").ForJob(new JobKey("time")).WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()).Build();
		


			scheduler.AddJob(job, true);
			scheduler.ScheduleJob(trigger);

			//scheduler.AddJob(job2, true);

			//scheduler.ScheduleJob(trigger2);

			Task.Run(()=> 
			{
				Thread.Sleep(TimeSpan.FromSeconds(2));
				scheduler.UnscheduleJob(new TriggerKey("time"));
			});
			Task.Run(() =>
			{
				Thread.Sleep(TimeSpan.FromSeconds(4));
				ITrigger trigger2 = TriggerBuilder.Create().StartNow().WithIdentity("time2",null).ForJob(new JobKey("time")).WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()).Build();
				scheduler.ScheduleJob(trigger2);
			});
			Thread.Sleep(TimeSpan.FromSeconds(8));

			StopScheduler();

			Console.WriteLine("END.");
			Console.Read();
		}

		public static void StopScheduler()
		{
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
			scheduler.Shutdown();
		}
	}

	public class TimeJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			Console.WriteLine(DateTime.Now.ToString());
		}
	}

	public class HelloJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			Console.WriteLine("hello");
		}
	}
}
