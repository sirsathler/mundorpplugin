using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class Job
	{
		public string name;
		public float salary;
		public int minLvl;
		public string difficulty;
		public string description;

		public string arg0;
		public string arg1;
		public string arg2;
		public string arg3;

		public Job(string name, float salary, string description, int minLvl, string difficulty, string arg0, string arg1, string arg2, string arg3)
		{
			this.name = name;
			this.salary = salary;
			this.minLvl = minLvl;
			this.difficulty = difficulty;
			this.description = description;
			this.arg0 = arg0;
			this.arg1 = arg1;
			this.arg2 = arg2;
			this.arg3 = arg3;
		}
		private Job() { }
	}
}
