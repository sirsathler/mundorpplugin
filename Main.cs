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
        public List<MundoPlayer> PlayerList = new List<MundoPlayer>();
        
        //JOBS
        public Dictionary<CSteamID, Vehicle> MundoVehicle_Vehicles = new Dictionary<CSteamID, Vehicle>();
        public List<Garage> MundoVehicle_Garages = new List<Garage>();

        //GAMBIARRISSIMA
        public List<ActiveContract> JobList_ActiveContracts = new List<ActiveContract>();

        public List<Mailbox> ObjList_Mailbox = new List<Mailbox>();
        public List<BusStop> ObjList_BusStops = new List<BusStop>();
        public List<Garbage> ObjList_Garbages = new List<Garbage>();
        public List<FuseBox> ObjList_Fuses = new List<FuseBox>();
        public List<Dump> ObjList_Dumps = new List<Dump>();
        public List<Job> JobList_Jobs = new List<Job>();

        public List<WorkNPC> NPCList_WorkNPCs = new List<WorkNPC>();


        public static Main Instance;

        protected override void Load()
        {
            Instance = this;
            //EVENTS
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked += ClickerManager.uiButtonClick;
            PlayerInput.onPluginKeyTick += QAClass.qacommand;

            //HARMONY SETUP
            var Harmony = new Harmony("com.mundorp.patches");
            Harmony.PatchAll();

            //CLEARS
            Instance.ModalOpenedPlayers.Clear();
            Instance.PlayerList.Clear();

            readData();

            Rocket.Core.Logging.Logger.Log("Mundo Roleplay Plugin | Carregado com sucesso!", ConsoleColor.Green);
        }
		protected override void Unload()
		{
            setData();
        }

        private void OnPlayerConnected(UnturnedPlayer Player)
		{
            MundoPlayer newplayer = DataManager.getPlayerBySteamId(Player.CSteamID);
            Instance.PlayerList.Add(newplayer);

            InterfaceManager.updateHUD(newplayer);
            (MundoPlayer player in Main.Instance.PlayerList)
			{
                Rocket.Core.Logging.Logger.Log("Usuário: "+player.Player.name+" entrou no jogo. Existem " + Instance.PlayerList.Count.ToString() + "online no momento.");
			}   
		}

		//ONDESLOG----------------------------------------------------//
		private void OnPlayerDisconnected(UnturnedPlayer Player)
		{
            MundoPlayer mplayer = MundoPlayer.getPlayerInList(Player.CSteamID.ToString());

            Instance.ModalOpenedPlayers.Remove(Player);
            JobManager.removePlayerJob(mplayer);
            int playerId = MundoPlayer.getPlayerIdInList(Player.CSteamID.ToString());
            if(playerId != -1)
			{
                Instance.PlayerList.RemoveAt(playerId);
                Rocket.Core.Logging.Logger.Log("Usuário "+Player.Player.name+" saiu do jogo. Restam " + Instance.PlayerList.Count.ToString() + "players online.");
			}

            Rocket.Core.Logging.Logger.Log(Player.CSteamID.ToString());
        }
    }
}


