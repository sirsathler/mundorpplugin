using Rocket.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class NPCManager
	{
		public static List<WorkNPC> injectNPCList()
		{
			foreach (WorkNPC npc in Main.Instance.Configuration.Instance.NPC_List)
			{
				Main.Instance.NPCList_WorkNPCs.Add(npc);
				Logger.Log("Adicionado o NPC de Trabalho " + npc.name);
			}
			return null;
		}

		public static void saveNPCList()
		{
			Main.Instance.Configuration.Instance.NPC_List.Clear();
			Main.Instance.Configuration.Save();

			foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
			{
				Main.Instance.Configuration.Instance.NPC_List.Add(npc);
				Logger.Log("Adicionado o NPC de: " + npc.jobname);
			}

			Main.Instance.Configuration.Save();
		}
	}
}
