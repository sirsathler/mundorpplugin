using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System.Linq;
using UnityEngine;
using Rocket.Unturned.Player;
using Steamworks;
using System;

namespace MundoRP
{
	public class MundoVehicleManager
	{
		public static void ClearVehicles()
		{
			Rocket.Core.Logging.Logger.Log("Clearing vehicles!");

			var cleared = 0;
			var vehicles = VehicleManager.vehicles;
			for (int i = vehicles.Count - 1; i >= 0; i--)
			{
				var vehicle = vehicles[i];
				VehicleManager.askVehicleDestroy(vehicle);
				//VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
				cleared++;

			}
			Rocket.Core.Logging.Logger.Log("Veiculos limpados: " + cleared);
		}

		public static void createTicket(Vector3 pos, Garage gr)
		{
			int grID = GetGarageID(gr);
			Main.Instance.Configuration.Save();
		}

		public static void criarGarage(Garage garage)
		{
			Main.Instance.VehicleManager_garagens.Add(garage);
			Main.Instance.Configuration.Instance.VehicleManager_Garagens.Add(garage);
			Main.Instance.Configuration.Save();
		}

		public static int GetGarageID(Garage gr)
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

		public static void giveVehicle(UnturnedPlayer player, int carIndexInPlayerGarage, int carTableId, Garage garage)
		{
			MundoPlayer mp = PlayerManager.getPlayerInList(player.CSteamID.ToString());
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
				VehicleManager.spawnLockedVehicleForPlayerV2(gv.vehicleId, new Vector3(garage.x, getVehicleBySteamID(player.CSteamID).transform.position.y, garage.z), Quaternion.Euler(garage.ang.x, garage.ang.y, garage.ang.z), player.Player);
				InteractableVehicle PlayerVehicle = VehicleManager.vehicles[VehicleManager.vehicles.Count() - 1];
				Main.Instance.vehicleList.Add(player.CSteamID, new Vehicle(PlayerVehicle, gv)); //ADICIONANDO O CARRO À LISTA DE VEÍCULOS DO SERVER
				//getVehicleBySteamID(player.CSteamID).transform.position = ;
				//getVehicleBySteamID(player.CSteamID).transform.eulerAngles = garage.ang;


				VehicleManager.sendVehicleBatteryCharge(PlayerVehicle, gv.battery);
				PlayerVehicle.tellBatteryCharge(gv.battery);
				VehicleManager.sendVehicleFuel(PlayerVehicle, gv.fuel);
				PlayerVehicle.tellFuel(gv.fuel);
				VehicleManager.sendVehicleHealth(PlayerVehicle, gv.health);
				PlayerVehicle.tellHealth(gv.health);

				//VehicleManager.instance.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				//{
				//	getVehicleBySteamID(player.CSteamID).instanceID,
				//	player.CSteamID,
				//	player.Player.quests.groupID,
				//	true
				//});
			mp.actualCar = carTableId;
			}
			catch (Exception ex)
			{
				NotificationManager.erro(player);
				Rocket.Core.Logging.Logger.Log(ex);
			}
		}

		public static InteractableVehicle getVehicleBySteamID(CSteamID steamID)
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

		public static void clearVehiclesByID(CSteamID steamID)
		{
			getVehicleBySteamID(steamID).forceRemoveAllPlayers();
			VehicleManager.askVehicleDestroy(getVehicleBySteamID(steamID));

			//VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, getVehicleBySteamID(steamID).instanceID);
		}
		
		public static void clearVehicleByVehicle(InteractableVehicle vehicle)
		{
			vehicle.forceRemoveAllPlayers();
			VehicleManager.askVehicleDestroy(vehicle);
			//VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, vehicle.instanceID);
		}

		public static bool CanClearVehicle(InteractableVehicle vehicle)
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

		public static string parse(float num)
		{
			return num.ToString().Replace(",", ".");
		}
	}
}