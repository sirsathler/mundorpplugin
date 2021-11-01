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

		public Job(string name, float salary, int minLvl)
		{
			this.name = name;
			this.salary = salary;
			this.minLvl = minLvl;
		}
	}
}
