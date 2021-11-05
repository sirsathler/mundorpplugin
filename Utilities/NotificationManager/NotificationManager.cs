using System;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class NotificationManager
	{
        public static Configuration config = Main.Instance.Configuration.Instance;
        public static void WorkHUD(MundoPlayer mplayer, Job job)
		{
            Rocket.Core.Logging.Logger.Log("Abrindo modal");
            UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(mplayer.steamid);
            ushort effectId = Convert.ToUInt16(Main.Instance.Configuration.Instance.EffectID_NewWorkModal);

            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_NewWorkModal), config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Difficulty", job.difficulty);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Salary", job.salary.ToString());
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_MinLvl", job.minLvl.ToString());
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Arg0", job.arg0);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Arg1", job.arg1);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Arg2", job.arg2);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Work_Arg3", job.arg3);
            uplayer.Player.serversideSetPluginModal(true);
        }

        public static void GarageHUD(MundoPlayer mplayer, UnturnedPlayer uplayer)
		{
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Garage), config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true);
            float totalDebts = 0;
            for (int i = 1; i <= 6; i++)
            {
                if (i <= mplayer.vehicleList.Count)
                {
                    int carTableId = mplayer.vehicleList[i-1].tableId;
                    EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Name", mplayer.vehicleList[i-1].vname);
                    EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Color", mplayer.vehicleList[i-1].vehicleColor);

                    if (mplayer.vehicleList[i - 1].vehicleDebts.Count == 0)
					{
                        if(carTableId == mplayer.actualCar)
					    {
                            EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "EM RUA!");
                        }
						else
						{
                            EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "");
						}
					}
					else
					{
                        float carDebts = 0;
                        foreach (VehicleDebt vD in mplayer.vehicleList[i - 1].vehicleDebts)
						{
                            carDebts += vD.value;
						}
                        EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "COM DÉBITOS");
                        EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Debts", carDebts.ToString()+"RP");
                        totalDebts += carDebts;
					}
                }
				else
				{
                    EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Name", "vazio");
                    EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Color", "");
                    EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "Car#" + i + "Status", "");
				}
            }
            EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "GarageCars#Input", mplayer.vehicleList.Count.ToString());
            EffectManager.sendUIEffectText(config.EffectID_Garage, uplayer.SteamPlayer().transportConnection, true, "GarageDebt#Input", totalDebts.ToString());
            uplayer.Player.serversideSetPluginModal(true);
		}

		public static void parkHUD(UnturnedPlayer uplayer)
		{
            uiClose(uplayer, Convert.ToUInt16(config.EffectID_Park));
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Park), config.EffectID_Park, uplayer.SteamPlayer().transportConnection, false);
            uplayer.Player.serversideSetPluginModal(true);
        }

        public static void createModal(UnturnedPlayer uplayer, Vector3 modalPosition, short id)
		{
            Rocket.Core.Logging.Logger.Log("Creating modal!");
            ModalBeacon modalb = new ModalBeacon(modalPosition, Main.Instance.Configuration.Instance.Interaction_Range, Convert.ToUInt16(id));
            Main.Instance.ModalOpenedPlayers.Add(uplayer, modalb);
        }




        //CLEAR ALL----------------------------------//
        public static void updateHUD(MundoPlayer player)
		{
            UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(player.steamid);
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_HUD), Convert.ToInt16(config.EffectID_HUD), uplayer.SteamPlayer().transportConnection, false, player.level.ToString(), player.xp.ToString(), player.rp.ToString(), player.job.name);
		}

        //SUCESSO!
        public static void sucesso(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Sucess), config.EffectID_Sucess, player.SteamPlayer().transportConnection, false, "sucesso!".ToUpper());
        }
        public static void sucesso(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Sucess), config.EffectID_Sucess, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //ERRO!
        public static void erro(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Sucess), config.EffectID_Sucess, player.SteamPlayer().transportConnection, false, "erro!".ToUpper());
        }
        public static void erro(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Error), config.EffectID_Error, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //ALERTA!
        public static void alerta(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Alert), config.EffectID_Alert, player.SteamPlayer().transportConnection, false, param.ToUpper());
        }

        //EXP!
        public static void exp(UnturnedPlayer player, int amount)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Exp), config.EffectID_Exp, player.SteamPlayer().transportConnection, false, "você recebeu "+amount+" ponto de EXP!".ToUpper());
        }

        //LEVEL!
        public static void lvl(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Level), config.EffectID_Level, player.SteamPlayer().transportConnection, false, "você passou de nível!".ToUpper());
        }




        //================================ UI MANAGER =============================================== //

        public static void uiClose(UnturnedPlayer uplayer, ushort id)
		{
            EffectManager.askEffectClearByID(id, uplayer.SteamPlayer().transportConnection);
            uplayer.Player.serversideSetPluginModal(false);
        }

        public static void uiButtonClick(Player player, string buttonName)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            MundoPlayer mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());

            //GARAGE================================
            if (buttonName == "CloseButton")
            {
                player.serversideSetPluginModal(false);
                EffectManager.askEffectClearByID(Convert.ToUInt16(config.EffectID_Garage), uplayer.SteamPlayer().transportConnection);
                EffectManager.askEffectClearByID(Convert.ToUInt16(config.EffectID_Park), uplayer.SteamPlayer().transportConnection);
                EffectManager.askEffectClearByID(Convert.ToUInt16(config.EffectID_NewWorkModal), uplayer.SteamPlayer().transportConnection);
                
                return;
            }

            //Car#1
            if (buttonName.Substring(0,4) == "Car#")
			{
                int cardId = Convert.ToInt32(buttonName.Substring(4, 1));
                mplayer = PlayerManager.getPlayerInList(uplayer.CSteamID.ToString());
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

                if (carTableId == PlayerManager.getPlayerInList(uplayer.CSteamID.ToString()).actualCar)
				{
                    alerta(uplayer, "Entre no veículo para guardá-lo na garagem!");
                    return;
                }
                if (mplayer.actualCar != 0)
				{
                    erro(uplayer, "Guarde o seu veículo na garagem primeiro!");
                    return;
                }
                MundoVehicleManager.giveVehicle(uplayer, cardId, carTableId, GarageManager.getNearbyGarage(uplayer));
                uiClose(uplayer, Convert.ToUInt16(config.EffectID_Garage));
                sucesso(uplayer, "Veículo retirado da garagem!");
			}
            //======================================//
            //PARKBUTTON
            if (buttonName == "ParkButton")
            {
                MundoVehicleManager.clearVehiclesByID(uplayer.CSteamID);
                DataManager.updateCar(Main.Instance.MundoVehicle_Vehicles[uplayer.CSteamID].iv, Main.Instance.MundoVehicle_Vehicles[uplayer.CSteamID].gv.tableId);
                mplayer.actualCar = 0;
                uiClose(uplayer, Convert.ToUInt16(config.EffectID_Park));
                sucesso(uplayer, "Você guardou seu veículo na garagem!");
                return;
            }

        }
        //========================= UI METHODS ======================================================== //
    }
}
