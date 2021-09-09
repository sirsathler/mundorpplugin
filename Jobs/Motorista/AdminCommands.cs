using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Core.Logging;
using UnityEngine;
using Rocket.Unturned.Chat;

namespace MundoRP
{
	class Motorista_AdminCommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "motorista";

		public string Help => "Comandos de ADM para Motoristas.";

		public string Syntax => "[<motorista>]";

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
