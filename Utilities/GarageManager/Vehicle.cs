using System;
using Steamworks;
using SDG.Unturned;


namespace MundoRP
{
	public class Vehicle
	{
		public GarageVehicle gv;
		public InteractableVehicle iv;
		public Vehicle(InteractableVehicle Vehicle, GarageVehicle vehiclemodel)
		{
			gv = vehiclemodel;
			iv = Vehicle;
		}
	}
}
