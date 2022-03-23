using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class MapManager_Commands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "mapmanager";

		public string Help => "Cria a cidade!";

		public string Syntax => "[<mapmanager>]";

		public List<string> Aliases => new List<string>() { "mm", "mapm" };

		public List<string> Permissions => new List<string>() { };
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer uplayer = (UnturnedPlayer)caller;

			if (command.Length == 0)
			{
				InterfaceManager.erro(uplayer, "Erro de sintaxe!");
				return;
			}
			
			if (command[0] == "criarlixeira" || command[0] == "clixeira")
			{
				if (command.Length == 1)
				{
					foreach (Garbage gb in Main.Instance.ObjList_Garbages)
					{
						Vector3 gbPosition = new Vector3(gb.x, gb.y, gb.z);
						if (Vector3.Distance(gbPosition, uplayer.Position) < Main.Instance.Configuration.Instance.Interaction_Range)
						{
							InterfaceManager.erro(uplayer, "Muito próximo à outra lixeira!");
							return;
						}
					}
					Garbage newGarbage = new Garbage(uplayer.Position.x, uplayer.Position.y, uplayer.Position.z);
					Main.Instance.ObjList_Garbages.Add(newGarbage);
					InterfaceManager.sucesso(uplayer, "Lixeira criada com sucesso!");
					return;
				}
				InterfaceManager.erroSintaxe(uplayer);
				return;
			}
			if (command[0] == "deletarlixeira" || command[0] == "dlixeira")
			{
				if (command.Length == 1)
				{
					foreach (Garbage gb in Main.Instance.ObjList_Garbages)
					{
						Vector3 gbPosition = new Vector3(gb.x, gb.y, gb.z);
						if (Vector3.Distance(gbPosition, uplayer.Position) < Main.Instance.Configuration.Instance.Interaction_Range)
						{
							Main.Instance.ObjList_Garbages.Remove(gb);
							InterfaceManager.sucesso(uplayer, "Lixeira deletada com sucesso!");
							return;
						}
					}
					InterfaceManager.erro(uplayer, "Você não está próximo de uma lixeira!");
					return;
				}
				InterfaceManager.erroSintaxe(uplayer);
				return;
			}

			if (command[0] == "criargaragem" || command[0] == "cgaragem")
			{
				if (command.Length == 2)
				{
					Garage newGarage = new Garage(command[1], uplayer.Position.x, uplayer.Position.y, uplayer.Position.z, uplayer.Player.transform.rotation.eulerAngles);
					try
					{
						MundoVehicleManager.createNewGarage(uplayer, newGarage);
						InterfaceManager.sucesso(uplayer, "Garagem " + newGarage.name + " criada com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						InterfaceManager.erro(uplayer);
						Rocket.Core.Logging.Logger.Log(ex.ToString());
					}
					return;
				}
				InterfaceManager.erroSintaxe(uplayer);
				return;
			}

			if (command[0] == "deletarnpc" || command[0] == "dnpc")
			{
				if (command.Length == 2)
				{
					foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
					{
						if (npc.name == command[1])
						{
							Main.Instance.NPCList_WorkNPCs.Remove(npc);

							return;
						}
					}
					InterfaceManager.erro(uplayer, "NPC não encontrado!");
					return;
				}
				InterfaceManager.erroSintaxe(uplayer);
				return;
			}

			if (command[0] == "criarnpc" || command[0] == "cnpc")
			{
				if (command.Length == 3)
				{
					Job newJob = JobManager.getJobByName(command[2]);

					if (newJob != null)
					{
						foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
						{
							if (Vector3.Distance(npc.position, uplayer.Position) < Main.Instance.Configuration.Instance.Interaction_Range)
							{
								InterfaceManager.erro(uplayer, "Muito próximo de outro NPC!");
								return;
							}
						}
						WorkNPC newNPC = new WorkNPC(command[1], uplayer.Position, command[2]);
						Main.Instance.NPCList_WorkNPCs.Add(newNPC);
						InterfaceManager.sucesso(uplayer, "NPC criado!");
						return;
					}
					else
					{
						InterfaceManager.erro(uplayer, "Trabalho não encontrado!");
						return;
					}
				}
				InterfaceManager.erroSintaxe(uplayer);
				return;
			}

			InterfaceManager.erroSintaxe(uplayer);
			return;
			
		}
	}
}