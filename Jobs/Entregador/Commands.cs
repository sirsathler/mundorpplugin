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
	class EntregarCommand : IRocketCommand
	{
		DataManager DataManager = new DataManager();
		NotificationManager Notificator = new NotificationManager();
		Entregador_Methods methods = new Entregador_Methods();
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "entregar";

		public string Help => "Entrega a correspondencia";

		public string Syntax => "[<entregar>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			string playerJob = Main.Instance.getPlayerInList(Player.CSteamID.ToString()).job;

			if (playerJob == "entregador")
			{
				if (Main.Instance.Entregador_servicos.ContainsKey(Player.CSteamID))
				{
					CaixaCorreio cx = Main.Instance.Entregador_servicos[Player.CSteamID][0];
					if (Vector3.Distance(new Vector3(cx.x, cx.y, cx.z), Player.Position) < Main.Instance.Configuration.Instance.Entregador_Range)
					{
						if (Main.Instance.vehicleList[Player.CSteamID].iv.sirensOn)
						{
							//Main.Instance.Eletricista_servicos.Remove(Player.CSteamID);
							Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player.CSteamID), "Correspondência entregada! Vá para a próxima!");
							methods.nextCaixa(Player.CSteamID);
							return;
						}
						else
						{
							Notificator.erro(Player, "Abra as portas da van usando Ctrl!");
						}
					}
					else
					{
						Notificator.erro(Player, "Muito longe da caixa de correio indicada!");
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
				Notificator.erro(Player, "Você não é um entregador!");
				return;
			}
		}
	}
}
