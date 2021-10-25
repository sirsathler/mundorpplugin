using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Logging;
using Rocket.API;
using Rocket.Unturned.Events;
using Steamworks;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    {
        NotificationManager Notificator = new NotificationManager();

        //------------------ COMMON OBJECTS
        public List<MundoPlayer> PlayerList = new List<MundoPlayer>(); //PLAYERLIST
        public Dictionary<UnturnedPlayer, ModalBeacon> ModalOpenedPlayers = new Dictionary<UnturnedPlayer, ModalBeacon>();
        public DataManager Data = new DataManager(); //DATAMANAGER
        public static Main Instance;
        protected override void Load()
        {
            ModalOpenedPlayers.Clear();
            Instance = this;
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked += Notificator.uiButtonClick;

            PlayerList.Clear();

            Main.Instance.Configuration.Save();

            readData();

            Rocket.Core.Logging.Logger.Log("Mundo Roleplay Plugin | Carregado com sucesso!", ConsoleColor.Green);
        }
		protected override void Unload()
		{
            setData();
        }
        //BUTTONCLICK-------------------------------------------------//

        //ONLOG-------------------------------------------------------//

        private void OnPlayerConnected(UnturnedPlayer Player)
		{
            MundoPlayer newplayer = dataManager.getPlayerBySteamId(Player.CSteamID);
            Instance.PlayerList.Add(newplayer);
            Notificator.updateHUD(newplayer);
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
            int playerId = getPlayerIdInList(Player.CSteamID.ToString());
            if(playerId != -1)
			{
                Instance.PlayerList.RemoveAt(playerId);
                Rocket.Core.Logging.Logger.Log("Players online atualmente: " + Instance.PlayerList.Count.ToString());
			}

            Rocket.Core.Logging.Logger.Log(Player.CSteamID.ToString());
            string playerJob = Data.getPlayerBySteamId(Player.CSteamID).job;

            if (playerJob == "eletricista")
			{
				if (Instance.Eletricista_servicos.ContainsKey(Player.CSteamID)) 
                {
					try
					{
                        Instance.Eletricista_servicos.Remove(Player.CSteamID);
					}
                    catch(Exception ex)
					{
                        Rocket.Core.Logging.Logger.Log(ex);
					}
                }
			}
            
            else if(playerJob == "reciclador")
			{
				if (Instance.Reciclador_servicos.ContainsKey(Player.CSteamID)) 
                {
					try
					{
                        Instance.Reciclador_servicos.Remove(Player.CSteamID);
					}
                    catch(Exception ex)
					{
                        Rocket.Core.Logging.Logger.Log(ex);
					}
                }
			}

            else if (playerJob == "entregador")
            {
                if (Instance.Entregador_servicos.ContainsKey(Player.CSteamID))
                {
                    try
                    {
                        Instance.Entregador_servicos.Remove(Player.CSteamID);
                    }
                    catch (Exception ex)
                    {
                        Rocket.Core.Logging.Logger.Log(ex);
                    }
                }
            }

            else if (playerJob == "motorista")
            {
                if (Instance.motorista_servicos.ContainsKey(Player.CSteamID))
                {
                    try
                    {
                        Instance.motorista_servicos.Remove(Player.CSteamID);
                    }
                    catch (Exception ex)
                    {
                        Rocket.Core.Logging.Logger.Log(ex);
                    }
                }
            }
        }

        //--------------------UTILITIES-------------------------------//
        DataManager dataManager = new DataManager();


        //--------------------VEHICLE MANAGER-------------------------//

        public Dictionary<CSteamID, Vehicle> vehicleList = new Dictionary<CSteamID, Vehicle>();
        public List<Garage> VehicleManager_garagens = new List<Garage>();

        //-------------------JOBS--------------------------//
        
        //JOBS


        //RECICLADOR
        public List<LataDeLixo> Reciclador_latasdelixo = new List<LataDeLixo>();
        public List<Aterro> Reciclador_aterros = new List<Aterro>();
        public Dictionary<CSteamID, int> Reciclador_servicos = new Dictionary<CSteamID, int>();

        //ELETRICISTA
        public Dictionary<CSteamID, List<Poste>> Eletricista_servicos = new Dictionary<CSteamID, List<Poste>>();
        public List<Poste> Eletricista_postes = new List<Poste>();
        
        //CARTEIRO
        public List<CaixaCorreio> Entregador_caixascorreios = new List<CaixaCorreio>();
        public Dictionary<CSteamID, List<CaixaCorreio>> Entregador_servicos = new Dictionary<CSteamID, List<CaixaCorreio>>();        
       
        //MOTORISTA
        public List<PontoOnibus> motorista_PontosOnibus = new List<PontoOnibus>();
        public List<PontoOnibus> motorista_Terminais = new List<PontoOnibus>();
        public Dictionary<CSteamID, List<PontoOnibus>> motorista_servicos = new Dictionary<CSteamID, List<PontoOnibus>>();

        public bool setData()
		{
            ObjectManager objManager = new ObjectManager(Instance.Reciclador_aterros, Instance.Entregador_caixascorreios, Instance.Reciclador_latasdelixo, Instance.motorista_PontosOnibus, Instance.Eletricista_postes);
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.Aterros.Count + " Aterros!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.CaixasCorreios.Count + " Caixas de Correios!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.LatasDeLixo.Count + " Latas de Lixo!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.PontosDeOnibus.Count + " Pontos de Ônibus!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.Postes.Count + " Postes!");
            return dataManager.updateObjects(objManager);
        }

        public void readData() // LOAD DATA
        {
            Instance.Eletricista_postes.Clear();
            Instance.Reciclador_aterros.Clear();
            Instance.Reciclador_latasdelixo.Clear();
            Instance.motorista_PontosOnibus.Clear();
            Instance.Entregador_caixascorreios.Clear();
            Instance.VehicleManager_garagens.Clear();

            foreach(Garage gr in Configuration.Instance.VehicleManager_Garagens)
			{
                Main.Instance.VehicleManager_garagens.Add(gr);
                Rocket.Core.Logging.Logger.Log("Adicionada a garagem: " + gr.nome);
			}

            ObjectManager objManager = dataManager.getObjectsFromDB();

            Main.Instance.Reciclador_latasdelixo = objManager.LatasDeLixo;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.Reciclador_latasdelixo.Count.ToString() + " Latas de Lixos.");

            Main.Instance.Eletricista_postes = objManager.Postes;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.Eletricista_postes.Count.ToString() + " Postes.");

            Main.Instance.Entregador_caixascorreios = objManager.CaixasCorreios;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.Entregador_caixascorreios.Count.ToString() + " Caixas de Correios.");

            Main.Instance.Reciclador_aterros = objManager.Aterros;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.Reciclador_aterros.Count.ToString() + " Aterros Sanitários.");

            Main.Instance.motorista_PontosOnibus = objManager.PontosDeOnibus;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.Reciclador_aterros.Count.ToString() + " Pontos de Ônibus.");

        }

        // FUNÇÕES
        public Garage getNearbyGarage(UnturnedPlayer player)
		{
            Garage NearbyGarage = null;
            float distance=500;
            foreach(Garage gr in Main.Instance.VehicleManager_garagens)
			{
                Vector3 grPosition = new Vector3(gr.x, gr.y, gr.z);
                if (Vector3.Distance(grPosition, player.Position) < distance)
				{
                    NearbyGarage = gr;
                    distance = Vector3.Distance(grPosition, player.Position);
				}
			}
            return NearbyGarage;
		}
        public MundoPlayer getPlayerInList(string csteamid)
		{
            int i = 0;
            for(i=0; i <= Main.Instance.PlayerList.Count; i++)
			{
                if (PlayerList[i].steamid.ToString() == csteamid)
				{
                    return PlayerList[i];
				}
			}
            return null;
		}
        public int getPlayerIdInList(string csteamid)
		{
            int i = 0;
            for(i=0; i <= Main.Instance.PlayerList.Count; i++)
			{
                if (PlayerList[i].steamid.ToString() == csteamid)
				{
                    return i;
				}
			}
            return -1;
		}
    }
}
