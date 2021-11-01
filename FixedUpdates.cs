using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    {
		NotificationManager notificator = new NotificationManager();
		public void removePlayerFromModal()
		{
			foreach (UnturnedPlayer uplayer in Main.Instance.ModalOpenedPlayers.Keys)
			{
				ModalBeacon mb = Main.Instance.ModalOpenedPlayers[uplayer];
				if (Vector3.Distance(uplayer.Position, mb.position) > mb.range*2)
				{
					try
					{
						Main.Instance.ModalOpenedPlayers.Remove(uplayer);
						notificator.uiClose(uplayer, mb.id);
						NotificationManager.alerta(uplayer, "Você saiu da zona do modal!");
						return;
					}
					catch(Exception ex) 
					{
						Rocket.Core.Logging.Logger.Log(ex.ToString());
						return;
					}
				}
			}
		}

		public void garbageHintModal()
		{

		}

		public void GarageModal()
		{
			foreach (var steamplayer in Provider.clients)
			{
				UnturnedPlayer uplayer = UnturnedPlayer.FromSteamPlayer(steamplayer);
				MundoPlayer mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());
				if (!Main.Instance.ModalOpenedPlayers.ContainsKey(uplayer))
				{
					
				}
			}
		}
        public void FixedUpdate()
		{
			removePlayerFromModal();
			GarageModal();
        }
    }
}
