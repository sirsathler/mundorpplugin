using System;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public static class HintUI
	{
		public static void AskHintModal(UnturnedPlayer uplayer)
		{
            MundoPlayer mplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());

            if (MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString()) == null) { Rocket.Core.Logging.Logger.Log("NULL!"); }

            Configuration config = Main.Instance.Configuration.Instance;


            if (mplayer.jobName == "reciclador" && JobManager.isPlayerWorking(mplayer.steamid.ToString()))
            {
                foreach (Garbage gb in Main.Instance.ObjList_Garbages)
                {
                    if (gb.Cooldown >= DateTime.Now)
                    {
                        return;
                    }
                    Vector3 gbPosition = new Vector3(gb.x, gb.y, gb.z);
                    if (Vector3.Distance(gbPosition, uplayer.Position) <= config.Interaction_Range)
                    {
                        InterfaceManager.hintJob(uplayer, "Você encontrou uma lixeira não coletada! Use o comando /coletar para coletá-la!", "/coletar");
                        ModalManager.createModal(uplayer, config.EffectID_Hint);
                        return;
                    }
                }
            }
        }
    }
}
