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
		public string jobName;
		public CSteamID steamid;
		public bool premium;
		public float mp, rp;
		public int actualCar;
		public List<GarageVehicle> vehicleList;

		public MundoPlayer(string Username, CSteamID SteamId, int Level, int Xp, string Job, bool Premium, float Mp, float Rp, List<GarageVehicle> vlist)
		{
			username = Username;
			steamid = SteamId;
			level = Level;
			xp = Xp;
			jobName = Job;
			premium = Premium;
			mp = Mp;
			rp = Rp;
			vehicleList = vlist;
			actualCar = 0;
		}

		public static MundoPlayer getPlayerInList(string csteamid)
		{
			int i = 0;
			for (i = 0; i <= Main.Instance.PlayerList.Count; i++)
			{
				if (Main.Instance.PlayerList[i].steamid.ToString() == csteamid)
				{
					return Main.Instance.PlayerList[i];
				}
			}
			return null;
		}
		public static int getPlayerIdInList(string csteamid)
		{
			int i = 0;
			for (i = 0; i <= Main.Instance.PlayerList.Count; i++)
			{
				if (Main.Instance.PlayerList[i].steamid.ToString() == csteamid)
				{
					return i;
				}
			}
			return -1;
		}
	}
}