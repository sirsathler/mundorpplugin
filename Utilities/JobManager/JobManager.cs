using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Logging;

namespace MundoRP
{
	public static class JobManager
	{
		public static Job getJobByName(string name)
		{
			foreach(Job job in Main.Instance.JobList_Jobs)
			{
				if(name == job.name)
				{
					return job;
				}
			}
			return null;
		}

		public static void injectJobList()
		{
			Main.Instance.JobList_Jobs.Clear();
			Main.Instance.JobList_Jobs.Add(new Job("Desempregado", 0, 0, "", "", "", "", ""));

			List<Job> newJobs = DataManager.getJobsFromDB();

			Main.Instance.JobList_Jobs = newJobs == null ? new List<Job>() : newJobs;
		}
	}
}