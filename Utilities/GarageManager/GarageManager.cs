using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class GarageManager
	{
		public static Garage getNearbyGarage(UnturnedPlayer player)
		{
			Garage NearbyGarage = null;
			float distance = 500;
			foreach (Garage gr in Main.Instance.VehicleManager_garagens)
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
