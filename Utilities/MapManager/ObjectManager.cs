using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class ObjectManager
	{
		public List<Dump> Dumps;
		public List<Mailbox> MailBoxes;
		public List<Garbage> Garbages;
		public List<BusStop> BusStops;
		public List<FuseBox> FuseBoxes;

		public ObjectManager(List<Dump> dumps, List<Mailbox> mailboxes, List<Garbage> garbages, List<BusStop> busStops, List<FuseBox> fuseBoxes)
		{
			Dumps = dumps;
			MailBoxes = mailboxes;
			Garbages = garbages;
			BusStops = busStops;
			FuseBoxes = fuseBoxes;
		}
	}
}
