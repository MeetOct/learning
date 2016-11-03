using Quartz;
using Quartz.Impl;

namespace Learning.Quartz
{
	public class JobManager
	{
		public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
		public static bool AddJob<T>(ScheduleInfo model)where T:IJob
		{
			IJobDetail job = JobBuilder.Create<T>()
													.WithIdentity(model.Name, model.Group)
													.Build();

			ITrigger trigger = TriggerBuilder.Create()
																.StartAt(model.StarRunAt)
																.ForJob(job)
																.EndAt(model.EndRunAt)
																.WithIdentity(model.Name, model.Group)
																.WithCronSchedule(model.CronStr)
																.Build();
			//scheduler.AddJob(job,true);
			scheduler.ScheduleJob(job,trigger);

			return true;
		}
	}
}
