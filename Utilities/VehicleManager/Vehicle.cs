using System;
using Steamworks;
using SDG.Unturned;


namespace MundoRP
{
	public class Vehicle
	{
		public CSteamID owner;	
		public DateTime spawnDate;
		public InteractableVehicle vehicle;
		public bool usable;

		public Vehicle(CSteamID Owner, InteractableVehicle Vehicle, DateTime SpawnDate)
		{
			owner = Owner;
			spawnDate = SpawnDate;
			vehicle = Vehicle;
			usable = true;
		}
	}
}
