using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Rocket.Unturned.Player;

namespace MundoRP
{
	public class WorkNPC
	{
		public string name;
		public Vector3 position;
		public string jobname;

		public WorkNPC(string name, Vector3 position, string job)
		{
			this.name = name;
			this.position = position;
			this.jobname = job;
		}
		private WorkNPC() { }
	}
}