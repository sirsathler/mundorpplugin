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


		public static void injectObjectList()
		{
			ObjectManager objManager = DataManager.getObjectsFromDB();

			Main.Instance.ObjList_Garbages = objManager.Garbages;
			Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Garbages.Count.ToString() + " Latas de Lixos.");

            Main.Instance.ObjList_Fuses = objManager.FuseBoxes;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Fuses.Count.ToString() + " Fusíveis.");

            Main.Instance.ObjList_Mailbox = objManager.MailBoxes;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Mailbox.Count.ToString() + " Caixas de Correios.");

            Main.Instance.ObjList_Dumps = objManager.Dumps;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Dumps.Count.ToString() + " Aterros Sanitários.");

            Main.Instance.ObjList_BusStops = objManager.BusStops;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Dumps.Count.ToString() + " Pontos de Ônibus.");
		}

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
