using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned.Chat;


namespace MundoRP
{
	public class VehicleManager_AdminCommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "vehiclemanager";

		public string Help => "Comandos de Admins do VehicleManager!";

		public string Syntax => "[<vehiclemanager>]";

		public List<string> Aliases => new List<string>() { "vm" };

		public List<string> Permissions => new List<string>() { };

		public void Execute(IRocketPlayer caller, string[] command)
		{


			InterfaceManager Notificator = new InterfaceManager();
			DataManager DBManager = new DataManager();
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			MundoVehicleManager methods = new MundoVehicleManager();

			if (command[0] == "reset")
			{
				MundoVehicleManager.clearVehicles();
				return;
			}
		}
	}
}