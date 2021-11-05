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

		public List<string> Permissions => new List<string>() {};
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer uplayer = (UnturnedPlayer)caller;
			NotificationManager.alerta(uplayer, "Executando comando!");


			if (command.Length == 0)
			{
				NotificationManager.erro(uplayer, "Erro de sintaxe!");
			}
			else
			{
				if(command.Length == 2)
				{
					if (command[0] == "criargaragem" || command[0] == "cgaragem")
					{
						Garage newGarage = new Garage(command[1], uplayer.Position.x, uplayer.Position.y, uplayer.Position.z, uplayer.Player.transform.rotation.eulerAngles);
						try
						{
							MundoVehicleManager.createNewGarage(uplayer, newGarage);
							NotificationManager.sucesso(uplayer, "Garagem " + newGarage.name + " criada com sucesso!");
						}
						catch(Exception ex)
						{
							NotificationManager.erro(uplayer);
							Rocket.Core.Logging.Logger.Log(ex.ToString());
						}
						return;
					}
					
					if (command[0] == "deletarnpc" || command[0] == "dnpc")
					{
						foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
						{
							if (npc.name == command[1])
							{
								Main.Instance.NPCList_WorkNPCs.Remove(npc);
								return;
							}
						}
						NotificationManager.erro(uplayer, "NPC não encontrado!");
						return;
					}
					NotificationManager.erro(uplayer, "Erro de sintaxe!");
					return;
				}
				if(command.Length == 3)
				{
					if (command[0] == "criarnpc" || command[0] == "cnpc")
					{
						Job newJob = JobManager.getJobByName(command[2]);

						if (newJob != null)
						{
							WorkNPC newNPC = new WorkNPC(command[1], uplayer.Position, command[2]);
							Main.Instance.NPCList_WorkNPCs.Add(newNPC);
							NotificationManager.sucesso (uplayer, "NPC criado!");
							return;
						}
						else
						{
							NotificationManager.erro(uplayer, "Trabalho não encontrado!");
							return;
						}
					}
				}
				NotificationManager.erro(uplayer, "Erro de sintaxe!");
				return;
			}
		}
	}
}
