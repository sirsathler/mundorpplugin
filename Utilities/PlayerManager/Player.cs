using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;

namespace MundoRP
{
	public class MundoPlayer
	{
		public int level, xp;
		public string username;
		public Job job;
		public CSteamID steamid;
		public bool premium;
		public float mp, rp;
		public int actualCar;
		public List<GarageVehicle> vehicleList;

		public MundoPlayer(string Username, CSteamID SteamId, int Level, int Xp, Job Job, bool Premium, float Mp, float Rp, List<GarageVehicle> vlist)
		{
			username = Username;
			steamid = SteamId;
			level = Level;
			xp = Xp;
			job = Job;
			premium = Premium;
			mp = Mp;
			rp = Rp;
			vehicleList = vlist;
			actualCar = 0;
		}
	}
}