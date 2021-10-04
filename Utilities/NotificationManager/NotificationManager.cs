using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using Rocket.Unturned.Player;
using Steamworks;

namespace MundoRP
{
	public class NotificationManager
	{
        //--------------------NOTIFICATIONMANAGER---------------------//
        public void GarageHUD(MundoPlayer player, List<GarageVehicle> vehicles)
		{
            UnturnedPlayer untplayer = UnturnedPlayer.FromCSteamID(player.steamid);
            EffectManager.sendUIEffect(17000, 17000, player.steamid, false);
            for (int i = 1; i <= 6; i++)
            {
                if (i <= vehicles.Count)
                {
                    EffectManager.sendUIEffectText(17000, player.steamid, true, "Car#" + i + "Name", vehicles[i-1].vname);
                }
                else
                {
                    EffectManager.sendUIEffectText(17000, player.steamid, true, "Car#" + i + "Name", "SEM VEÍCULO!");
                }
            }
            untplayer.Player.serversideSetPluginModal(true);
		}

        //CLEAR ALL----------------------------------//
        public void updateHUD(MundoPlayer player)
		{
            EffectManager.sendUIEffect(16000, 16000, player.steamid, false, player.level.ToString(), player.xp.ToString(), player.rp.ToString(), player.job);
		}

        //SUCESSO!
        public void sucesso(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14522, 14522, player.CSteamID, false, "sucesso!".ToUpper());
        }
        public void sucesso(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14522, 14522, player.CSteamID, false, param.ToUpper());
        }

        //ERRO!
        public void erro(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14520, 14520, player.CSteamID, false, "erro!".ToUpper());
        }
        public void erro(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14520, 14520, player.CSteamID, false, param.ToUpper());
        }

        //ALERTA!
        public void alerta(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14521, 14521, player.CSteamID, false, param.ToUpper());
        }

        //EXP!
        public void exp(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14523, 14523, player.CSteamID, false, "você recebeu 1 ponto de EXP!".ToUpper());
        }

        //LEVEL!
        public void lvl(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14524, 14524, player.CSteamID, false, "você passou de nível!".ToUpper());
        }

        //CHAMADO!
        public void chamado(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14525, 14525, player.CSteamID, false, param.ToUpper());
        }

        //JOB!
        public void job(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14526, 14526, player.CSteamID, false, param.ToUpper());
        }









        //================================ UI MANAGER =============================================== //

        public void uiClose(UnturnedPlayer uplayer)
		{
            EffectManager.askEffectClearByID(17000, uplayer.CSteamID); //Garagem
            uplayer.Player.serversideSetPluginModal(false);
        }

        public void uiButtonClick(Player player, string buttonName)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            MundoPlayer mplayer = Main.Instance.getPlayerInList(uplayer.CSteamID.ToString());
            NotificationManager notification = new NotificationManager();
            VehicleManager_Methods vehicleManager = new VehicleManager_Methods();

            //GARAGE================================
            if (buttonName == "CloseButton")
            {
                player.serversideSetPluginModal(false);
                EffectManager.askEffectClearByID(17000, uplayer.CSteamID);
                
                return;
            }

            //Car#1
            if (buttonName.Substring(0,4) == "Car#")
			{
                int carId = Convert.ToInt32(buttonName.Substring(4,1));
                vehicleManager.giveVehicle(uplayer, mplayer.vehicleList[carId-1], Main.Instance.VehicleManager_garagens[0]);
                uiClose(uplayer);
                sucesso(uplayer, "Veículo retirado da garagem!");
			}

            //======================================
        }


        //========================= UI METHODS ======================================================== //

    }
}
