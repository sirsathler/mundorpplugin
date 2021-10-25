using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Logging;
using Rocket.API;
using Rocket.Unturned.Events;
using Steamworks;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    { 
		public void removePlayer()
		{
			foreach (UnturnedPlayer uplayer in Main.Instance.ModalOpenedPlayers.Keys)
			{
				ModalBeacon mb = Main.Instance.ModalOpenedPlayers[uplayer];
				if (Vector3.Distance(uplayer.Position, mb.position) > mb.range*2)
				{
					try
					{
						Main.Instance.ModalOpenedPlayers.Remove(uplayer);
						NotificationManager notificator = new NotificationManager();
						notificator.uiClose(uplayer, mb.id);
						notificator.alerta(uplayer, "Você saiu da zona do modal!");
						return;
					}
					catch { }
				}
			}
		}

		public void GarageModal()
		{
			foreach (var steamplayer in Provider.clients)
			{
				UnturnedPlayer uplayer = UnturnedPlayer.FromSteamPlayer(steamplayer);
				MundoPlayer mplayer = Main.Instance.getPlayerInList(uplayer.CSteamID.ToString());
				if (!Main.Instance.ModalOpenedPlayers.ContainsKey(uplayer))
				{
					foreach (Garage gr in Main.Instance.VehicleManager_garagens)
					{
						Vector3 grPosition = new Vector3(gr.x, gr.y, gr.z);

						if (Vector3.Distance(uplayer.Position, grPosition) <= Main.Instance.Configuration.Instance.GarageBeacon_Range)
						{
							NotificationManager notificator = new NotificationManager();
							if (uplayer.IsInVehicle && uplayer.CurrentVehicle.instanceID == Main.Instance.vehicleList[uplayer.CSteamID].iv.instanceID)
							{
								try
								{
									ModalBeacon mb = new ModalBeacon(grPosition, Main.Instance.Configuration.Instance.GarageBeacon_Range, 17001);
									Main.Instance.ModalOpenedPlayers.Add(uplayer, mb);
									notificator.parkHUD(uplayer);
									return;
								}
								catch (Exception ex)
								{
									Rocket.Core.Logging.Logger.Log(ex.ToString());
								}
							}
							try
							{
								Main.Instance.getPlayerInList(uplayer.CSteamID.ToString()).vehicleList = Data.getVehiclesBySteamId(uplayer.CSteamID);
								ModalBeacon mb = new ModalBeacon(grPosition, Main.Instance.Configuration.Instance.GarageBeacon_Range, 17000);
								Main.Instance.ModalOpenedPlayers.Add(uplayer, mb);
								notificator.GarageHUD(getPlayerInList(uplayer.CSteamID.ToString()), uplayer);
							}
							catch (Exception ex)
							{
								Rocket.Core.Logging.Logger.Log(ex.ToString());
							}
						}
					}
				}
			}
		}
        public void FixedUpdate()
		{
			removePlayer();
			GarageModal();
        }
    }
}
