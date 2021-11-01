using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class PlayerManager
	{
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
