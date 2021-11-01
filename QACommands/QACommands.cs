using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned;
using SDG.Unturned;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using MySql.Data.MySqlClient;
using HarmonyLib;


namespace MundoRP
{
	class QACommands : IRocketCommand
	{
		NotificationManager notificator = new NotificationManager();

		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "qa";

		public string Help => "Teste de QA";

		public string Syntax => "[<qa>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		void IRocketCommand.Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			DataManager databaseManager = new DataManager();
			MundoPlayer mPlayer = PlayerManager.getPlayerInList(Player.CSteamID.ToString());

			if (Player.IsInVehicle)
			{
				NotificationManager.alerta(Player, "Salvando o carro: "+mPlayer.actualCar.ToString());
				databaseManager.updateCar(Main.Instance.vehicleList[Player.CSteamID].iv, Main.Instance.vehicleList[Player.CSteamID].gv.tableId);
			}
			else
			{
				NotificationManager.erro(Player, "Você não está em um veículo!");
			}
		}
	}
}