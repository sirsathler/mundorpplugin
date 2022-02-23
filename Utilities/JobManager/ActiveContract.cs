using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
    public class ActiveContract
    {
        string WorkerSteamid;
        object JobContract;   

        public ActiveContract(MundoPlayer mplayer, object jobContract)
        {
            this.WorkerSteamid = mplayer.steamid.ToString();
            this.JobContract = jobContract;
        }

        public static int getActiveContract(MundoPlayer mplayer)
        {
            if(Main.Instance.JobList_ActiveContracts.Count == 0)
            {
                return -1;
            }
            for(int i = 0; i <= Main.Instance.JobList_ActiveContracts.Count; i++)
            {
                if (Main.Instance.JobList_ActiveContracts[i].WorkerSteamid == mplayer.steamid.ToString())
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
