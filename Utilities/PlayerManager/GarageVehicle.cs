using System;
using Steamworks;
using SDG.Unturned;


namespace MundoRP
{
	public class GarageVehicle
	{
		public string owner;
		public DateTime spawnDate;
		public bool usable;
		public string vname;
		public ushort battery;
		public ushort health;
		public ushort fuel;
		public ushort vehicleId;

		public GarageVehicle(string Owner, ushort vId, DateTime SpawnDate, ushort bat, ushort hp, ushort gas, string vename)
		{
			owner = Owner;
			vehicleId = vId;
			spawnDate = SpawnDate;
			usable = true;
			battery = bat;
			health = hp;
			fuel = gas;
			vname = vename;
		}
	}
}
