using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class JobContract
	{
		int garbageCollected;
		string csteamId;

		public JobContract(string csteamId)
		{
			this.csteamId = csteamId;
			garbageCollected = 0;
		}
	}
}
