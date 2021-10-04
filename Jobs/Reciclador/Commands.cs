using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unturned;

namespace MundoRP
{
	//------------------------------------------ COLETAR
	public class ColetarCommand : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "coletar";

		public string Help => "Coleta o lixo de uma lixeira";

		public string Syntax => "[<coletar>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		DataManager DBManager = new DataManager();

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();
			string playerJob = Main.Instance.getPlayerInList(Player.CSteamID.ToString()).job;

			if (playerJob == "reciclador")
			{
				foreach (LataDeLixo lx in Main.Instance.Reciclador_latasdelixo)
				{
					if (Vector3.Distance(new Vector3(lx.x, lx.y, lx.z), Player.Position) < Main.Instance.Configuration.Instance.Reciclador_MinRange)
					{
						if (!Main.Instance.Reciclador_servicos.ContainsKey(Player.CSteamID) || Main.Instance.Reciclador_servicos[Player.CSteamID] < Main.Instance.Configuration.Instance.Reciclador_CargasPorTrabalho)
						{
							if (lx.Cooldown < DateTime.Now)
							{
								if (!Main.Instance.Reciclador_servicos.ContainsKey(Player.CSteamID))
								{
									Notificator.erro(Player, "Você não está em serviço! Use /trabalhar !");
									return;
								}
								
								Main.Instance.Reciclador_servicos[Player.CSteamID] = Main.Instance.Reciclador_servicos[Player.CSteamID] + 1;
								lx.Cooldown = DateTime.Now.AddMinutes(Main.Instance.Configuration.Instance.Reciclador_Cooldown);

								Notificator.sucesso(Player, "Lata de lixo coletada com sucesso!");

								if(Main.Instance.Reciclador_servicos[Player.CSteamID] == Main.Instance.Configuration.Instance.Reciclador_CargasPorTrabalho)
								{
									Notificator.sucesso(Player, "Caminhão carregado! Volte para o aterro!");
									EffectManager.sendUIEffect(21000, 21000, Player.CSteamID, false, "DESCARREGUE NO ATERRO SANITÁRIO", "CAMINHÃO CARREGADO", "use /descarregar");
									return;
								}
								EffectManager.sendUIEffect(21000, 21000, Player.CSteamID, false, "COLETE AS LIXEIRAS!", Main.Instance.Reciclador_servicos[Player.CSteamID].ToString() + "/" + Main.Instance.Configuration.Instance.Reciclador_CargasPorTrabalho.ToString(), "use /coletar");
;								return;
							}
							else
							{
								Notificator.erro(Player, "Essa lixeira já foi coletada!");
								return;
							}
						}
						else
						{
							Notificator.erro(Player, "Você já está com a carga máxima! Descarregue no Aterro!");
							return;
						}
					}
				}
				Notificator.erro(Player, "Muito longe de uma lixeira!");
				return;
			}
			else
			{
				Notificator.erro(Player, "Você não é um reciclador! Procure a sede do emprego para se tornar um.");
			}
		}
	}
}
