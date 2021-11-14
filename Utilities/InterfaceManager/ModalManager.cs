using System;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class ModalManager
	{
		public static void createModal(UnturnedPlayer uplayer, Vector3 modalPosition, short id)
		{
			ModalBeacon modalb = new ModalBeacon(modalPosition, Main.Instance.Configuration.Instance.Interaction_Range, Convert.ToUInt16(id));
			Main.Instance.ModalOpenedPlayers.Add(uplayer, modalb);
		}

		public static void removePlayerFromModal(UnturnedPlayer uplayer)
		{
			ModalBeacon mb = Main.Instance.ModalOpenedPlayers[uplayer];
			if (Vector3.Distance(uplayer.Position, mb.position) > mb.range * 2)
			{
				try
				{
					Main.Instance.ModalOpenedPlayers.Remove(uplayer);
					uiClose(uplayer, mb.id);
					InterfaceManager.alerta(uplayer, "Você saiu da zona do modal!");
					return;
				}
				catch (Exception ex)
				{
					Rocket.Core.Logging.Logger.Log(ex.ToString());
					return;
				}
			}
		}
		public static void uiClose(UnturnedPlayer uplayer, ushort id)
		{
			EffectManager.askEffectClearByID(id, uplayer.SteamPlayer().transportConnection);
			uplayer.Player.serversideSetPluginModal(false);
		}
		public static void newWorkModal(UnturnedPlayer uplayer)
		{
			foreach (WorkNPC npc in Main.Instance.NPCList_WorkNPCs)
			{
				Vector3 grPosition = npc.position;
				if (!uplayer.IsInVehicle && Vector3.Distance(grPosition, uplayer.Position) < Main.Instance.Configuration.Instance.Interaction_Range)
				{
					ModalManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_NewWorkModal);
					InterfaceManager.WorkHUD(MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString()), JobManager.getJobByName(npc.jobname));
					return;
				}
			}
			return;
		}

		public static void GarageModal(UnturnedPlayer uplayer)
		{
			MundoPlayer mplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());

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
								ModalManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_Garage);
								InterfaceManager.parkHUD(uplayer);
								return;
							}
							catch (Exception ex)
							{
								Rocket.Core.Logging.Logger.Log(ex.ToString());
								return;
							}
						}
						InterfaceManager.erro(uplayer, "Esse veículo não te pertence!");
						return;
					}
					try
					{
						MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString()).vehicleList = DataManager.getVehiclesBySteamId(uplayer.CSteamID);
						ModalManager.createModal(uplayer, grPosition, Main.Instance.Configuration.Instance.EffectID_Park);
						InterfaceManager.GarageHUD(MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString()), uplayer);
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
