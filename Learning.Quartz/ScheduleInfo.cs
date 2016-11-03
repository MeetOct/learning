using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Quartz
{
	public class ScheduleInfo
	{
		public string Name { get; set; }
		
		public string Group { get; set; }

		public DateTime StarRunAt { get; set; }

		public DateTime EndRunAt { get; set; }


		public string CronStr { get; set; }
	}
}
