using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace MundoRP
{
	public class VehicleManager_RetirarCommand : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "retirar";

		public string Help => "Retira um carro de servico ou da garagem!";

		public string Syntax => "[<retirar>]";

		public List<string> Aliases => new List<string>() {"rt"};

		public List<string> Permissions => new List<string>() { };

		public void Execute(IRocketPlayer caller, string[] command)
		{
			NotificationManager Notificator = new NotificationManager();
			DataManager DBManager = new DataManager();
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			VehicleManager_Methods methods = new VehicleManager_Methods();
			string playerJob = Main.Instance.PlayerList[Main.Instance.getPlayerInList(Player.CSteamID.ToString())].job;
			foreach (Garagem gr in Main.Instance.VehicleManager_garagens)
			{
				if(Vector3.Distance(Player.Position, new Vector3(gr.ticketPosX, gr.ticketPosY, gr.ticketPosZ)) < Main.Instance.Configuration.Instance.VehicleManager_MinRange)
				{
					if(playerJob == gr.job)
					{
						if(DateTime.Now > gr.Cooldown)
						{
							if(gr.job != "")
							{
								methods.giveVehicle(Player, gr.vehicleId, gr);
								gr.Cooldown = DateTime.Now.AddSeconds(Main.Instance.Configuration.Instance.VehicleManager_Cooldown);
							}
							else { 
							}
							Notificator.sucesso(Player, "Você retirou um carro da garagem!");
							return;
						}
						else
						{
							Notificator.erro(Player, "Aguarde para usar essa garagem novamente!");
							return;
						}
					}
					else
					{
						Notificator.erro(Player, "Você precisa ser " + gr.job + " para usar essa garagem!");
						Rocket.Core.Logging.Logger.Log("Seu trabalho é: " + DBManager.getPlayerBySteamId(Player.CSteamID).job);
						return;
					}
				}
			}
			Notificator.erro(Player, "Você não está em uma máquina de ticket");
			return;
		}
	}
}
