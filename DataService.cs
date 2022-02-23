using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    {
        public bool setData()
		{
			try
			{
				DataManager.updateObjects();
				GarageManager.saveGarageList();
                NPCManager.saveNPCList();

                Rocket.Core.Logging.Logger.Log("Configurações salvas com sucesso!");
                return true;
			}
			catch (Exception ex)
			{
                Rocket.Core.Logging.Logger.Log(ex.ToString());
                return false;
			}
		}

		public void readData()
        {
            Instance.ObjList_Fuses.Clear();
            Instance.ObjList_Dumps.Clear();
            Instance.ObjList_Garbages.Clear();
            Instance.ObjList_BusStops.Clear();
            Instance.ObjList_Mailbox.Clear();
            Instance.MundoVehicle_Garages.Clear();
            Instance.NPCList_WorkNPCs.Clear();
            Instance.JobList_Jobs.Clear();

            JobManager.injectJobList();
            GarageManager.injectGarage();
            NPCManager.injectNPCList();
            ObjectManager.injectObjectList();
        }
    }
}
