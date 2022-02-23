using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace MundoRP
{
	public class GarageManager
	{
		public static void injectGarage()
		{
			foreach (Garage gr in Main.Instance.Configuration.Instance.MundoVehicle_Garages)
			{
				Main.Instance.MundoVehicle_Garages.Add(gr);
				Rocket.Core.Logging.Logger.Log("Adicionada a garagem: " + gr.name);
			}
		}

		public static void saveGarageList()
		{
			foreach (Garage gar in Main.Instance.MundoVehicle_Garages)
			{
				Main.Instance.Configuration.Instance.MundoVehicle_Garages.Add(gar);
				Rocket.Core.Logging.Logger.Log("Adicionado o trabalho de: " + gar.name);
			}

			Main.Instance.Configuration.Save();
		}

		public static Garage getNearbyGarage(UnturnedPlayer player)
		{
			Garage NearbyGarage = null;
			float distance = 500;
			foreach (Garage gr in Main.Instance.MundoVehicle_Garages)
			{
				Vector3 grPosition = new Vector3(gr.x, gr.y, gr.z);
				if (Vector3.Distance(grPosition, player.Position) < distance)
				{
					NearbyGarage = gr;
					distance = Vector3.Distance(grPosition, player.Position);
				}
			}
			return NearbyGarage;
		}
	}
}
