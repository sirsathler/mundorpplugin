using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class MapManager_Commands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "mapmanager";

		public string Help => "Cria a cidade!";

		public string Syntax => "[<mapmanager>]";

		public List<string> Aliases => new List<string>() { "mm", "mapm" };

		public List<string> Permissions => new List<string>() {};
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();
			MundoVehicleManager vehicle_Methods = new MundoVehicleManager();

			if (command.Length == 0)
			{
			}
			else
			{

			}
		}
	}
}
