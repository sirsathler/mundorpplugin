using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System.Linq;
using Rocket.API.Collections;
using UnityEngine;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Events;

namespace MundoRP
{
	public class VehicleManager_Methods
	{
		NotificationManager Notificator = new NotificationManager();
		public void createTicket(Vector3 pos, Garagem gr)
		{
			Main.Instance.Configuration.Instance.VehicleManager_Garagens[GetGaragemID(gr)].ticketPosX = pos.x;
			Main.Instance.Configuration.Instance.VehicleManager_Garagens[GetGaragemID(gr)].ticketPosY = pos.y;
			Main.Instance.Configuration.Instance.VehicleManager_Garagens[GetGaragemID(gr)].ticketPosZ = pos.z;

			Main.Instance.VehicleManager_garagens[GetGaragemID(gr)].ticketPosX = pos.x;
			Main.Instance.VehicleManager_garagens[GetGaragemID(gr)].ticketPosY = pos.y;
			Main.Instance.VehicleManager_garagens[GetGaragemID(gr)].ticketPosZ = pos.z;

			Main.Instance.Configuration.Save();
		}

		public void criarGaragem(Garagem garagem)
		{
			Main.Instance.VehicleManager_garagens.Add(garagem);
			Main.Instance.Configuration.Instance.VehicleManager_Garagens.Add(garagem);
			Main.Instance.Configuration.Save();
		}

		public void ClearVehicles()
		{
			Rocket.Core.Logging.Logger.Log("Clearing vehicles!");

			var cleared = 0;
			for (int i = VehicleManager.vehicles.Count - 1; i >= 0; i--)
			{
				var vehicle = VehicleManager.vehicles[i];
				vehicle.forceRemoveAllPlayers();
				VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
				cleared++;
			}

			Rocket.Core.Logging.Logger.Log($"Cleared {cleared} vehicles!");
		}

		public int GetGaragemID(Garagem gr)
		{
			for(int i = 0; i < Main.Instance.Configuration.Instance.VehicleManager_Garagens.Count; i++)
			{
				if(Main.Instance.Configuration.Instance.VehicleManager_Garagens[i].nome == gr.nome)
				{
					return i;
				}
			}

			return 0;
		}

		public void giveVehicle(UnturnedPlayer player, ushort vehicle, Garagem garagem)
		{
			if (Main.Instance.vehicleList.ContainsKey(player.CSteamID))
			{
				clearVehiclesByID(player.CSteamID);
				Main.Instance.vehicleList.Remove(player.CSteamID);
			}
			try
			{
				player.GiveVehicle(vehicle);
				Main.Instance.vehicleList.Add(player.CSteamID, new Vehicle(player.CSteamID, VehicleManager.vehicles[VehicleManager.vehicles.Count() - 1], DateTime.Now));
				getVehicleBySteamID(player.CSteamID).transform.position = new Vector3(garagem.x, getVehicleBySteamID(player.CSteamID).transform.position.y, garagem.z);
				getVehicleBySteamID(player.CSteamID).transform.eulerAngles = garagem.ang;
				VehicleManager.instance.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{		
					getVehicleBySteamID(player.CSteamID).instanceID,
					player.CSteamID,
					player.Player.quests.groupID,
					true
				});
			}
			catch (Exception ex)
			{
				Notificator.erro(player);
				Rocket.Core.Logging.Logger.Log(ex);
			}
		}

		public InteractableVehicle getVehicleBySteamID(CSteamID steamID)
		{
			foreach (CSteamID id in Main.Instance.vehicleList.Keys)
			{
				if (id == steamID)
				{
					return Main.Instance.vehicleList[id].vehicle;
				}
			}
			return null;
		}

		public void clearVehiclesByID(CSteamID steamID)
		{
			getVehicleBySteamID(steamID).forceRemoveAllPlayers();
			VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, getVehicleBySteamID(steamID).instanceID);
		}
		
		public void clearVehicleByVehicle(InteractableVehicle vehicle)
		{
			vehicle.forceRemoveAllPlayers();
			VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
		}

		public bool CanClearVehicle(InteractableVehicle vehicle)
		{
			if (vehicle.passengers.Any(p => p.player != null) || vehicle.asset.engine == EEngine.TRAIN)
			{
				return false;
			}
			if (vehicle.isExploded)
			{
				return true;
			}

			if (vehicle.isDrowned && vehicle.transform.FindChild("Buoyancy") == null)
			{
				return true;
			}

			var tires = vehicle.transform.Find("Tires");
			if (tires != null)
			{
				var totalTires = vehicle.transform.Find("Tires").childCount;
				if (totalTires == 0)
				{
					return false;
				}
				var aliveTires = 0;
				for (var i = 0; i < totalTires; i++)
				{
					if (tires.GetChild(i).gameObject.activeSelf)
					{
						aliveTires++;
					}
				}
				if (aliveTires == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}