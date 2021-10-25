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
            float totalDebts = 0;
            for (int i = 1; i <= 6; i++)
            {
                if (i <= mplayer.vehicleList.Count)
                {
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Name", mplayer.vehicleList[i-1].vname);
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Color", mplayer.vehicleList[i-1].vehicleColor);
                    if (mplayer.vehicleList[i - 1].vehicleDebts.Count == 0)
					{
                        if(i == mplayer.actualCar)
					    {
                            EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "EM RUA!");
                        }
						else
						{
                            EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "");
						}
					}
					else
					{
                        float carDebts = 0;
                        foreach (VehicleDebt vD in mplayer.vehicleList[i - 1].vehicleDebts)
						{
                            carDebts += vD.value;
						}
                        EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "COM DÉBITOS");
                        EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Debts", carDebts.ToString()+"RP");
                        totalDebts += carDebts;
					}
                }
				else
				{
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Name", "vazio");
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Color", "");
                    EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "Car#" + i + "Status", "");
				}
            }
            EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "GarageCars#Input", mplayer.vehicleList.Count.ToString());
            EffectManager.sendUIEffectText(17000, uplayer.CSteamID, true, "GarageDebt#Input", totalDebts.ToString());
            uplayer.Player.serversideSetPluginModal(true);
		}

        public void parkHUD(UnturnedPlayer uplayer)
		{
            uiClose(uplayer, 17001);
            EffectManager.sendUIEffect(17001, 17001, uplayer.CSteamID, false);
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
        public void exp(UnturnedPlayer player, int amount)
        {
            EffectManager.sendUIEffect(14523, 14523, player.CSteamID, false, "você recebeu "+amount+" ponto de EXP!".ToUpper());
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

        public void uiClose(UnturnedPlayer uplayer, ushort id)
		{
            EffectManager.askEffectClearByID(id, uplayer.CSteamID);
            uplayer.Player.serversideSetPluginModal(false);
        }

        public void uiButtonClick(Player player, string buttonName)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            MundoPlayer mplayer = Main.Instance.getPlayerInList(uplayer.CSteamID.ToString());
            NotificationManager notification = new NotificationManager();
            MundoVehicleManager vehicleManager = new MundoVehicleManager();

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
                if (mplayer.vehicleList[carId-1].vehicleDebts.Count != 0)
				{
                    erro(uplayer, "Veículo com débitos! Procure uma prefeitura!");
                    return;
				}
                if (carId == Main.Instance.getPlayerInList(uplayer.CSteamID.ToString()).actualCar)
				{
                    alerta(uplayer, "Entre no carro para guardá-lo na garagem!");
                    return;
                }
                if (mplayer.actualCar != 0)
				{
                    erro(uplayer, "Você já possui um veículo em rua! Guarde-o na garagem!");
                    return;
                }
                vehicleManager.giveVehicle(uplayer, carId, Main.Instance.getNearbyGarage(uplayer));
                uiClose(uplayer, 17000);
                sucesso(uplayer, "Veículo retirado da garagem!");
			}
            //======================================//
            //PARKBUTTON
            if (buttonName == "ParkButton")
            {
                vehicleManager.clearVehiclesByID(uplayer.CSteamID);
                data.updateCar(Main.Instance.vehicleList[uplayer.CSteamID].iv, Main.Instance.vehicleList[uplayer.CSteamID].gv.tableId);
                sucesso(uplayer, "Você guardou seu veículo na garagem!");
                mplayer.actualCar = 0;
                uiClose(uplayer, 17001);
                return;
            }

        }
        //========================= UI METHODS ======================================================== //
    }
}
