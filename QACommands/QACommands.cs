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
			DataManager data = new DataManager();
			UnturnedPlayer player = (UnturnedPlayer)caller;
			data.getPlayerBySteamId(player.CSteamID);
			int playerId = Main.Instance.getPlayerInList(player.CSteamID.ToString());
			try
			{
				notificator.alerta(player, Main.Instance.PlayerList[playerId].username);
			}
			catch(Exception ex)
			{
				Rocket.Core.Logging.Logger.Log(ex.ToString());
			}
		}
	}
}
