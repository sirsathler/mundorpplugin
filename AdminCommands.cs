using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using SDG.Unturned;
using Rocket.Core.Logging;

namespace MundoRP
{
	//------------------------------------------ COLETAR
	public class AdminCommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "mundorp";

		public string Help => "Administrar!";

		public string Syntax => "[<mundorp>]";

		public List<string> Aliases => new List<string>(){"mrp"};

		public List<string> Permissions => new List<string>();

		DataManager DBManager = new DataManager();
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;

			if (command[0] == "setdata")
			{
				NotificationManager.alerta(Player, "Adicionando dados no banco!");
				try
				{
					if (Main.Instance.setData())
					{
						NotificationManager.sucesso(Player, "Banco de Dados atualizado com sucesso!");
					}
				}
				catch (Exception ex)
				{
					NotificationManager.erro(Player);
					Logger.Log(ex);
					return;
				}
				NotificationManager.sucesso(Player);
			}
			else if (command[0] == "readdata")
			{
				NotificationManager.alerta(Player, "Lendo dados no banco!");
				try
				{
					Main.Instance.readData();
				}
				catch (Exception ex)
				{
					NotificationManager.erro(Player);
					Logger.Log(ex);
					return;
				}
				NotificationManager.sucesso(Player);
			}
		}
	}
}
