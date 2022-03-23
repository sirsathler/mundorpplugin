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

using UnityEngine;
using UnityEditor;

namespace MundoRP
{
	public static class QAClass
	{
		internal static void qacommand(Player player, uint sim, byte key, bool state)
		{
		}
	}

	class QACommands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "qa";

		public string Help => "Teste de QA";

		public string Syntax => "[<qa>]";

		public List<string> Aliases => new List<string>();

		public List<string> Permissions => new List<string>();

		void IRocketCommand.Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer uplayer = (UnturnedPlayer)caller;
			Rocket.Core.Logging.Logger.Log("QA!");
			EffectManager.sendEffect(14021, uplayer.SteamPlayer().transportConnection, uplayer.Position);
		}
	}
}