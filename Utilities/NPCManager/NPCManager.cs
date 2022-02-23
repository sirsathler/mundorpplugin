using Rocket.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class NPCManager
	{
		public static List<WorkNPC> injectNPCList()
		{
			foreach (WorkNPC npc in Main.Instance.Configuration.Instance.NPC_List)
			{
				Main.Instance.NPCList_WorkNPCs.Add(npc);
				Rocket.Core.Logging.Logger.Log("Adicionado o NPC de Trabalho " + npc.name);
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
				Rocket.Core.Logging.Logger.Log("Adicionado o NPC de: " + npc.jobname);
			}

			Main.Instance.Configuration.Save();
		}
		public static WorkNPC getNearbyNPC(UnturnedPlayer uplayer)
		{
			if (Main.Instance.NPCList_WorkNPCs.Count == 0)
			{
				Rocket.Core.Logging.Logger.Log("Não há NPC cadastrado! Use /mm cnpc <nome> <job>");
				return null;
			}
			WorkNPC nearNPC = Main.Instance.NPCList_WorkNPCs[0];
			Vector3 playerPos = uplayer.Position;
			foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
			{
				if (Vector3.Distance(playerPos, npc.position) < Vector3.Distance(playerPos, nearNPC.position))
				{
					nearNPC = npc;
				}
			}
			return nearNPC;
		}
	}
}
