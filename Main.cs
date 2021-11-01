using System;
using System.Collections.Generic;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using HarmonyLib;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    {
        public Dictionary<UnturnedPlayer, ModalBeacon> ModalOpenedPlayers = new Dictionary<UnturnedPlayer, ModalBeacon>();
        public NotificationManager Notificator = new NotificationManager();
        public List<MundoPlayer> PlayerList = new List<MundoPlayer>();
        public DataManager Data = new DataManager();



        //--------------------UTILITIES-------------------------------//
        DataManager dataManager = new DataManager();


        //--------------------VEHICLE MANAGER-------------------------//

        public Dictionary<CSteamID, Vehicle> vehicleList = new Dictionary<CSteamID, Vehicle>();
        public List<Garage> VehicleManager_garagens = new List<Garage>();

        //-------------------JOBS--------------------------//


        public List<Mailbox> ObjList_Mailbox = new List<Mailbox>();
        public List<BusStop> ObjList_BusStops = new List<BusStop>();
        public List<Garbage> ObjList_Garbages = new List<Garbage>();
        public List<Dump> ObjList_Dumps = new List<Dump>();
        public List<FuseBox> ObjList_Fuses = new List<FuseBox>();
        public JobManager jobManager = new JobManager();
        public List<Job> jobList = new List<Job>();


        public static Main Instance;

        //
        //
        //
        // 
        //  ON PLUGIN LOAD                =========================================
        //
        //
        //
        //

        protected override void Load()
        {
            Instance = this;
            
            //EVENTS
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked += Notificator.uiButtonClick;

            //JOBS
            jobManager.InjectJobList();
            
            //HARMONY SETUP
            var Harmony = new Harmony("com.mundorp.patches");
            Harmony.PatchAll();

            readData();
            Rocket.Core.Logging.Logger.Log("Mundo Roleplay Plugin | Carregado com sucesso!", ConsoleColor.Green);
        }
		protected override void Unload()
		{
            setData();
        }

        private void OnPlayerConnected(UnturnedPlayer Player)
		{
            MundoPlayer newplayer = dataManager.getPlayerBySteamId(Player.CSteamID);
            Instance.PlayerList.Add(newplayer);
            NotificationManager.updateHUD(newplayer);
            Rocket.Core.Logging.Logger.Log("Players online atualmente: " + Instance.PlayerList.Count.ToString());
            foreach(MundoPlayer player in Main.Instance.PlayerList)
			{
                Rocket.Core.Logging.Logger.Log("Usuário: "+player.username);
			}   
		}

		//ONDESLOG----------------------------------------------------//
		private void OnPlayerDisconnected(UnturnedPlayer Player)
		{
            Instance.ModalOpenedPlayers.Remove(Player);
            int playerId = PlayerManager.getPlayerIdInList(Player.CSteamID.ToString());
            if(playerId != -1)
			{
                Instance.PlayerList.RemoveAt(playerId);
                Rocket.Core.Logging.Logger.Log("Players online atualmente: " + Instance.PlayerList.Count.ToString());
			}

            Rocket.Core.Logging.Logger.Log(Player.CSteamID.ToString());
            string playerJob = Data.getPlayerBySteamId(Player.CSteamID).job;

        }
    }
}
