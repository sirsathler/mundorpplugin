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
	class Eletricista_AdminCommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "eletricista";

		public string Help => "Comandos de ADM para Eletricistas.";

		public string Syntax => "[<eletricista>]";

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
