using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Logging;

namespace MundoRP
{
	public class JobManager
	{

		public List<Job> InjectJobList()
		{
			Main.Instance.jobList.Clear();
			foreach (Job job in Main.Instance.Configuration.Instance.JobsList)
			{
				Main.Instance.jobList.Add(job);
				Logger.Log(job.name);
			}
			return null;
		}
	}
}