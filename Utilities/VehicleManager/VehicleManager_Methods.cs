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
		public void createTicket(Vector3 pos, Garage gr)
		{
			int grID = GetGarageID(gr);
			Main.Instance.Configuration.Save();
		}

		public void criarGarage(Garage garage)
		{
			Main.Instance.VehicleManager_garagens.Add(garage);
			Main.Instance.Configuration.Instance.VehicleManager_Garagens.Add(garage);
			Main.Instance.Configuration.Save();
		}

		public void ClearVehicles()
		{
			Rocket.Core.Logging.Logger.Log("Clearing vehicles!");

			var cleared = 0;
			for (int i = VehicleManager.vehicles.Count; i == 0; i--)
			{
				var vehicle = VehicleManager.vehicles[i];
				vehicle.forceRemoveAllPlayers();
				VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
				cleared++;
			}

			Rocket.Core.Logging.Logger.Log($"Cleared {cleared} vehicles!");
		}

		public int GetGarageID(Garage gr)
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

		public void giveVehicle(UnturnedPlayer player, int carIndexInPlayerGarage, Garage garage)
		{
			MundoPlayer mp = Main.Instance.getPlayerInList(player.CSteamID.ToString());
			GarageVehicle gv = mp.vehicleList[carIndexInPlayerGarage-1];

			if (Main.Instance.vehicleList.ContainsKey(player.CSteamID))
			{
				clearVehiclesByID(player.CSteamID);
				Main.Instance.vehicleList.Remove(player.CSteamID);
			}
			try
			{
				//SETTANDO AS CONFIGS DO VEHICLE
				player.GiveVehicle(gv.vehicleId);
				InteractableVehicle PlayerVehicle = VehicleManager.vehicles[VehicleManager.vehicles.Count() - 1];
				Main.Instance.vehicleList.Add(player.CSteamID, new Vehicle(PlayerVehicle, gv)); //ADICIONANDO O CARRO À LISTA DE VEÍCULOS DO SERVER
				getVehicleBySteamID(player.CSteamID).transform.position = new Vector3(garage.x, getVehicleBySteamID(player.CSteamID).transform.position.y, garage.z);
				getVehicleBySteamID(player.CSteamID).transform.eulerAngles = garage.ang;


				VehicleManager.sendVehicleBatteryCharge(PlayerVehicle, gv.battery);
				PlayerVehicle.tellBatteryCharge(gv.battery);
				VehicleManager.sendVehicleFuel(PlayerVehicle, gv.fuel);
				PlayerVehicle.tellFuel(gv.fuel);
				VehicleManager.sendVehicleHealth(PlayerVehicle, gv.health);
				PlayerVehicle.tellHealth(gv.health);

				VehicleManager.instance.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					getVehicleBySteamID(player.CSteamID).instanceID,
					player.CSteamID,
					player.Player.quests.groupID,
					true
				});
			mp.actualCar = carIndexInPlayerGarage;
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
					return Main.Instance.vehicleList[id].iv;
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

			if (vehicle.isDrowned && vehicle.transform.Find("Buoyancy") == null)
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

		public string parse(float num)
		{
			return num.ToString().Replace(",", ".");
		}
	}
}