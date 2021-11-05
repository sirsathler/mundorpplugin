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
				MundoPlayer mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());
				
				if (Instance.ModalOpenedPlayers.ContainsKey(uplayer))
				{
					removePlayerFromModal(uplayer);
				}
				else
				{
					GarageModal(uplayer);
					newWorkModal(uplayer);
				}
			}
		}

		public void removePlayerFromModal(UnturnedPlayer uplayer)
		{
			ModalBeacon mb = Instance.ModalOpenedPlayers[uplayer];
			if (Vector3.Distance(uplayer.Position, mb.position) > mb.range*2)
			{
				try
				{
					Instance.ModalOpenedPlayers.Remove(uplayer);
					NotificationManager.uiClose(uplayer, mb.id);
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

		public void newWorkModal(UnturnedPlayer uplayer)
		{
			MundoPlayer mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());

			foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
			{
				Vector3 grPosition = npc.position;
				if (!uplayer.IsInVehicle && Vector3.Distance(grPosition, uplayer.Position) < Main.Instance.Configuration.Instance.Interaction_Range) 
				{
					NotificationManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_NewWorkModal);
					NotificationManager.WorkHUD(mplayer, JobManager.getJobByName(npc.jobname));
					return;
				}
				return;
			}
		}

		public void GarageModal(UnturnedPlayer uplayer)
		{
			MundoPlayer mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());

			foreach (Garage gr in Main.Instance.MundoVehicle_Garages)
			{
				Vector3 grPosition = new Vector3(gr.x, gr.y, gr.z);

				if (Vector3.Distance(uplayer.Position, grPosition) <= Main.Instance.Configuration.Instance.Interaction_Range)
				{
					if (uplayer.IsInVehicle)
					{
						if (uplayer.CurrentVehicle.instanceID == Main.Instance.MundoVehicle_Vehicles[uplayer.CSteamID].iv.instanceID)
						{
							try
							{
								NotificationManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_Garage);
								NotificationManager.parkHUD(uplayer);
								return;
							}
							catch (Exception ex)
							{
								Rocket.Core.Logging.Logger.Log(ex.ToString());
								return;
							}
						}
						NotificationManager.erro(uplayer, "Esse veículo não te pertence!");
						return;
					}
					try
					{
						PlayerManager.getPlayerInList(uplayer.CSteamID.ToString()).vehicleList = DataManager.getVehiclesBySteamId(uplayer.CSteamID);
						NotificationManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_Park);
						NotificationManager.GarageHUD(PlayerManager.getPlayerInList(uplayer.CSteamID.ToString()), uplayer);
						return;
					}
					catch (Exception ex)
					{
						Rocket.Core.Logging.Logger.Log(ex.ToString());
						return;
					}
				}
			}
		}
	}
}

