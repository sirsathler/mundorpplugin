using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
	{
		public void FixedUpdate()
		{
			foreach (var steamplayer in Provider.clients)
			{
				UnturnedPlayer uplayer = UnturnedPlayer.FromSteamPlayer(steamplayer);
				MundoPlayer mplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());
				
				if (Instance.ModalOpenedPlayers.ContainsKey(uplayer))
				{
					if(Vector3.Distance(Instance.ModalOpenedPlayers[uplayer].position, uplayer.Position) > Instance.Configuration.Instance.Interaction_Range){
						ModalManager.removePlayerFromModal(uplayer);
                    }
				}
				else
				{
					ModalManager.GarageModal(uplayer);
					ModalManager.newWorkModal(uplayer);
					HintUI.AskHintModal(uplayer);
				}
			}
		}
	}
}

