using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace MundoRP
{
	class Carteiro_AdminCommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "entregador";

		public string Help => "Comandos de ADM para Carteiros.";

		public string Syntax => "[<entregador>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();

			if (command.Length == 2)
			{
				
			}
			else
			{
				Notificator.erro(Player, "Syntax ERROR!");
			}
		}
	}
}
