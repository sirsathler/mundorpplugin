using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using Rocket.API;

namespace MundoRP
{
	public static class JobManager
	{
		public static Job getJobByName(string name)
		{
			foreach(Job job in Main.Instance.JobList_Jobs)
			{
				if(name == job.name)
				{
					return job;
				}
			}
			return null;
		}

		public static void injectJobList()
		{
			Main.Instance.JobList_Jobs.Clear();
			Main.Instance.JobList_Jobs.Add(new Job("Desempregado", 0, 0, "", "", "", "", ""));

			List<Job> newJobs = DataManager.getJobsFromDB();

			Main.Instance.JobList_Jobs = newJobs == null ? new List<Job>() : newJobs;
		}

		public static void startPlayerJob(MundoPlayer mplayer) { 

			UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(mplayer.steamid);
            MundoPlayer mundoplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());
			WorkNPC npc = NPCManager.getNearbyNPC(uplayer);
            Job job = JobManager.getJobByName(npc.jobname);

            if(mplayer.jobName == job.name)
            {
				if (Main.Instance.PlayerList_InJob.Contains(mplayer.steamid.ToString()))
				{
					InterfaceManager.erro(uplayer, "Você já está em serviço!");
					ModalManager.uiClose(uplayer, Convert.ToUInt16(Main.Instance.Configuration.Instance.EffectID_NewWorkModal));
					return;
				}

                try
                {
					//CREATING NEW JOB CONTRACT ==================================== IMPORTANT!
					ActiveContract newContract = null;

					if(mplayer.jobName == "reciclador") { newContract = new ActiveContract(mplayer, new Contract_Reciclador(mplayer.steamid.ToString())); }


					//============================================================== IMPORTANT!
					
					if(newContract == null)
                    {
						InterfaceManager.erro(uplayer, "Cargo não foi encontrado! Contate um administrador!");
						return;
                    }
					Main.Instance.JobList_ActiveContracts.Add(newContract);
                }
				catch(Exception ex)
                {
					Rocket.Core.Logging.Logger.Log(ex.ToString());
					return;
                }

				ModalManager.uiClose(uplayer, Convert.ToUInt16(Main.Instance.Configuration.Instance.EffectID_NewWorkModal));
                InterfaceManager.sucesso(uplayer, "Você iniciou o trabalho de " + mplayer.jobName + "!");
                return;
            }

            if(job.minLvl <= mundoplayer.level)
			{
                mundoplayer.jobName = job.name;
                DataManager.updatePlayer(mundoplayer);
                ModalManager.uiClose(uplayer, Convert.ToUInt16(Main.Instance.Configuration.Instance.EffectID_NewWorkModal));
                InterfaceManager.sucesso(uplayer, "Você foi contratado como: " + npc.jobname);
                return;
			}
            InterfaceManager.erro(uplayer, "Você não possui level suficiente para esse trabalho!");
            return;
			
		}

		public static bool removePlayerJob(MundoPlayer mplayer)
        {
			int hasContract = ActiveContract.getActiveContract(mplayer);

			if (hasContract == -1)
            {
				Rocket.Core.Logging.Logger.Log("[MUNDO - Job Manager] Este usuario nao esta em um trabalho!");
				return false;
            }
            try{
				Main.Instance.JobList_ActiveContracts.RemoveAt(ActiveContract.getActiveContract(mplayer));
				Rocket.Core.Logging.Logger.Log("[MUNDO - Job Manager] Contrato de trabalho encerrado!");
				return true;
            }
			catch(Exception ex)
            {
				Rocket.Core.Logging.Logger.Log(ex.ToString());
				return false;
            }
        }
	}
}