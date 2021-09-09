using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace MundoRP
{
	class ConsertarCommand : IRocketCommand
	{
		DataManager DataManager = new DataManager();
		NotificationManager Notificator = new NotificationManager();
		Eletricista_Methods methods = new Eletricista_Methods();
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "consertar";

		public string Help => "Conserta o poste que o eletricista foi designado";

		public string Syntax => "[<consertar>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			string playerJob = Main.Instance.PlayerList[Main.Instance.getPlayerInList(Player.CSteamID.ToString())].job;

			if (playerJob == "eletricista")
			{
				if (Main.Instance.Eletricista_servicos.ContainsKey(Player.CSteamID))
				{
					Poste pst = Main.Instance.Eletricista_servicos[Player.CSteamID][0];
					if (Vector3.Distance(new Vector3(pst.x, pst.y, pst.z), Player.Position) < Main.Instance.Configuration.Instance.Eletricista_MinRange)
					{
						//Main.Instance.Eletricista_servicos.Remove(Player.CSteamID);
						Notificator.sucesso(Player, "Poste consertado, vá para o próximo!");
						methods.nextPoste(Player.CSteamID);
						return;
					}
					else
					{
						Notificator.erro(Player, "Muito longe do poste quebrado. Tente subir no poste!");
						return;
					}
				}
				else
				{
					Notificator.erro(Player, "Você não está em serviço, use /trabalhar!");
					return;
				}
			}
			else
			{
				Notificator.erro(Player, "Você não é um eletricista!");
				return;
			}
		}
	}
}
