using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using Rocket.Unturned.Player;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using Rocket.Unturned;

namespace MundoRP
{
	public class NotificationManager
	{
        DataManager data = new DataManager();
        //--------------------NOTIFICATIONMANAGER---------------------//
        public void GarageHUD(MundoPlayer mplayer, UnturnedPlayer uplayer)
		{
            EffectManager.sendUIEffect(17000, 17000, uplayer.CSteamID, false);
            for (int i = 1; i <= 8; i++)
            {

                if (i <= mplayer.vehicleList.Count)
                {
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Name", mplayer.vehicleList[i-1].vname);
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Color", mplayer.vehicleList[i-1].vehicleColor);
                
                    if(i == mplayer.actualCar)
					{
                        EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "Guardar");
                        //carButton.color = new Color(194, 38, 30);
                    }
                    else
					{
                        EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "Retirar");
                        //carButton.color = new Color(0, 114, 207);
                    }
                }
                else
                {
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Name", "SEM VEÍCULO!");
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Color", "");
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "");
                }
            }
            uplayer.Player.serversideSetPluginModal(true);
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
                mplayer = Main.Instance.getPlayerInList(uplayer.CSteamID.ToString());
                if (carId > mplayer.vehicleList.Count)
				{
                    erro(uplayer, "Você não possui veículos nessa vaga!");
                    return;
				}
                if (carId == Main.Instance.getPlayerInList(uplayer.CSteamID.ToString()).actualCar)
				{
                    vehicleManager.clearVehiclesByID(uplayer.CSteamID);
                    data.updateCar(Main.Instance.vehicleList[uplayer.CSteamID].iv, Main.Instance.vehicleList[uplayer.CSteamID].gv.tableId);
                    sucesso(uplayer, "Você guardou seu veículo na garagem!");
                    mplayer.actualCar = 0;
                    uiClose(uplayer);
                    return;
				}
                vehicleManager.giveVehicle(uplayer, carId, Main.Instance.getNearbyGarage(uplayer));
                uiClose(uplayer);
                sucesso(uplayer, "Veículo retirado da garagem!");
			}

            //======================================
        }


        //========================= UI METHODS ======================================================== //

    }
}
