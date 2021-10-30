using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using Rocket.Unturned.Player;
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
            EffectManager.sendUIEffect(17000, 17000, uplayer.SteamPlayer().transportConnection, true);
            float totalDebts = 0;
            for (int i = 1; i <= 6; i++)
            {
                if (i <= mplayer.vehicleList.Count)
                {
                    int carTableId = mplayer.vehicleList[i-1].tableId;
                    EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Name", mplayer.vehicleList[i-1].vname);
                    EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Color", mplayer.vehicleList[i-1].vehicleColor);

                    if (mplayer.vehicleList[i - 1].vehicleDebts.Count == 0)
					{
                        if(carTableId == mplayer.actualCar)
					    {
                            EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "EM RUA!");
                        }
						else
						{
                            EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "");
						}
					}
					else
					{
                        float carDebts = 0;
                        foreach (VehicleDebt vD in mplayer.vehicleList[i - 1].vehicleDebts)
						{
                            carDebts += vD.value;
						}
                        EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "COM DÉBITOS");
                        EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Debts", carDebts.ToString()+"RP");
                        totalDebts += carDebts;
					}
                }
				else
				{
                    EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Name", "vazio");
                    EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Color", "");
                    EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "");
				}
            }
            EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "GarageCars#Input", mplayer.vehicleList.Count.ToString());
            EffectManager.sendUIEffectText(17000, uplayer.SteamPlayer().transportConnection, true, "GarageDebt#Input", totalDebts.ToString());
            uplayer.Player.serversideSetPluginModal(true);
		}

		[Obsolete]
		public void parkHUD(UnturnedPlayer uplayer)
		{
            uiClose(uplayer, 17001);
            EffectManager.sendUIEffect(17001, 17001, uplayer.CSteamID, false);
            uplayer.Player.serversideSetPluginModal(true);
        }



        public static void sendUiEffectImageURL()
		{
        
		}








        //CLEAR ALL----------------------------------//
        public void updateHUD(MundoPlayer player)
		{
            UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(player.steamid);
            EffectManager.sendUIEffect(16000, 16000, uplayer.SteamPlayer().transportConnection, false, player.level.ToString(), player.xp.ToString(), player.rp.ToString(), player.job);
		}

        //SUCESSO!
        public void sucesso(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14522, 14522, player.SteamPlayer().transportConnection, false, "sucesso!".ToUpper());
        }
        public void sucesso(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14522, 14522, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //ERRO!
        public void erro(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14520, 14520, player.SteamPlayer().transportConnection, false, "erro!".ToUpper());
        }
        public void erro(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14520, 14520, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //ALERTA!
        public void alerta(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14521, 14521, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //EXP!
        public void exp(UnturnedPlayer player, int amount)
        {
            EffectManager.sendUIEffect(14523, 14523, player.SteamPlayer().transportConnection, false, "você recebeu "+amount+" ponto de EXP!".ToUpper());
        }

        //LEVEL!
        public void lvl(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(14524, 14524, player.SteamPlayer().transportConnection, false, "você passou de nível!".ToUpper());
        }

        //CHAMADO!
        public void chamado(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14525, 14525, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //JOB!
        public void job(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(14526, 14526, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }




        //================================ UI MANAGER =============================================== //

        public void uiClose(UnturnedPlayer uplayer, ushort id)
		{
            EffectManager.askEffectClearByID(id, uplayer.SteamPlayer().transportConnection);
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
                EffectManager.askEffectClearByID(17000, uplayer.SteamPlayer().transportConnection);
                EffectManager.askEffectClearByID(17001, uplayer.SteamPlayer().transportConnection);
                
                return;
            }

            //Car#1
            if (buttonName.Substring(0,4) == "Car#")
			{
                int cardId = Convert.ToInt32(buttonName.Substring(4, 1));
                mplayer = Main.Instance.getPlayerInList(uplayer.CSteamID.ToString());
                if (cardId > mplayer.vehicleList.Count)
				{
                    erro(uplayer, "Você não possui veículos nessa vaga!");
                    return;
				}
                if (mplayer.vehicleList[cardId - 1].vehicleDebts.Count != 0)
				{
                    erro(uplayer, "Veículo com débitos! Procure uma prefeitura!");
                    return;
				}

                int carTableId = mplayer.vehicleList[Convert.ToInt32(buttonName.Substring(4, 1)) - 1].tableId;

                if (carTableId == Main.Instance.getPlayerInList(uplayer.CSteamID.ToString()).actualCar)
				{
                    alerta(uplayer, "Entre no veículo para guardá-lo na garagem!");
                    return;
                }
                if (mplayer.actualCar != 0)
				{
                    erro(uplayer, "Guarde o seu veículo na garagem primeiro!");
                    return;
                }
                vehicleManager.giveVehicle(uplayer, cardId, carTableId, Main.Instance.getNearbyGarage(uplayer));
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
