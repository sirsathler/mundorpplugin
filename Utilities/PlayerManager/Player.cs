using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamworks;

namespace MundoRP
{
	public class Player
	{
		public int level, xp;
		public string username, steamid, job;
		public bool premium;
		public float mp, rp;

		public Player(string Username, string SteamId, int Level, int Xp, string Job, bool Premium, float Mp, float Rp)
		{
			username = Username;
			steamid = SteamId;
			level = Level;
			xp = Xp;
			job = Job;
			premium = Premium;
			mp = Mp;
			rp = Rp;
		}
		public Player()
		{

		}
	}
}