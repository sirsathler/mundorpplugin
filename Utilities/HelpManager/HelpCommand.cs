using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace MundoRP
{
	public class HelpManager_HelpCommand : IRocketCommand
	{
		AllowedCaller IRocketCommand.AllowedCaller => AllowedCaller.Player;

		string IRocketCommand.Name => "ajuda";

		string IRocketCommand.Help => "Busca ajuda!";

		string IRocketCommand.Syntax => "[<ajuda>]";

		List<string> IRocketCommand.Aliases => new List<string>() { "help" };

		List<string> IRocketCommand.Permissions => new List<string>();



		void IRocketCommand.Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();
			MapManager mapManager = new MapManager();

			Int16 range = 20;

			for(Int16 i = 0; i==range; i++)
			{
				EffectManager.askEffectClearByID(Convert.ToUInt16(20000 + i), Player.CSteamID);
			}

			if (command[0] == "reciclador")
			{
				EffectManager.sendUIEffect(20000, 20000, Player.CSteamID, false);
			}
			else if (command[0] == "eletricista")
			{
				EffectManager.sendUIEffect(20001, 20001, Player.CSteamID, false);
			}
			else if (command[0] == "entregador")
			{
				EffectManager.sendUIEffect(20002, 20002, Player.CSteamID, false);				
			}
			else if (command[0] == "motorista")
			{
				EffectManager.sendUIEffect(20003, 20003, Player.CSteamID, false);
			}
			else
			{
				Notificator.erro(Player, "Erro de SINTAXE!");
				return;
			}
		}
	}
}
