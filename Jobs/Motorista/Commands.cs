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
	class PontoCommand : IRocketCommand
	{
		DataManager DataManager = new DataManager();
		NotificationManager Notificator = new NotificationManager();
		Motorista_Methods methods = new Motorista_Methods();
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "ponto";

		public string Help => "Pega passageiros no ponto";

		public string Syntax => "[<ponto>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			string playerJob = Main.Instance.PlayerList[Main.Instance.getPlayerInList(Player.CSteamID.ToString())].job;

			if (playerJob == "motorista")
			{
				if (Main.Instance.motorista_servicos.ContainsKey(Player.CSteamID))
				{
					PontoOnibus po = Main.Instance.motorista_servicos[Player.CSteamID][0];
					if (Vector3.Distance(new Vector3(po.x, po.y, po.z), Player.Position) < Main.Instance.Configuration.Instance.Motorista_MinRange)
					{
						//Main.Instance.Eletricista_servicos.Remove(Player.CSteamID);
						if(Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Motorista_Carro)
						{
							if (!Player.CurrentVehicle.isLocked && !Player.CurrentVehicle.sirensOn)
							{
								if (Player.CurrentVehicle.speed < 1)
								{
									Notificator.sucesso(Player, "Você pegou passageiros, vá para o próximo ponto!");
									methods.nextPonto(Player);
									return;
								}
								else
								{
									Notificator.erro(Player, "Você precisa estar parado!");
									return;
								}
							}
							else
							{
								Notificator.erro(Player, "Abra as portas com CTRL e destranque o onibus!");
								return;
							}
						}
						else
						{
							Notificator.erro(Player, "Você precisa estar em um ônibus!");
							return;
						}
					}
					else
					{
						Notificator.erro(Player, "Muito longe do ponto de ônibus!");
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
				Notificator.erro(Player, "Você não é um motorista!");
				return;
			}
		}
	}
}
