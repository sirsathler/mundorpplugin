using System;
using System.Collections.Generic;
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
		public string vehicleColor;
		public int tableId;
		public List<VehicleDebt> vehicleDebts;

		public GarageVehicle(int TableId, string Owner, ushort vId, string vColor, DateTime SpawnDate, ushort bat, ushort hp, ushort gas, string vename, List<VehicleDebt> vDebts)
		{
			tableId = TableId;
			owner = Owner;
			vehicleId = vId;
			spawnDate = SpawnDate;
			usable = true;
			battery = bat;
			health = hp;
			fuel = gas;
			vname = vename;
			vehicleColor = vColor;
			vehicleDebts = vDebts;
		}
	}
}
