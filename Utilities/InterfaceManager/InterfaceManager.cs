﻿using System;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class InterfaceManager
	{
        public static Configuration config = Main.Instance.Configuration.Instance;
        public static void NewWorkHUD(MundoPlayer mplayer, Job job)
		{
            UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(mplayer.steamid);
            ushort effectId = Convert.ToUInt16(Main.Instance.Configuration.Instance.EffectID_NewWorkModal);

            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_NewWorkModal), config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Message_JobName", job.name);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Message_JobDifficulty", job.difficulty);
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Message_JobSalary", job.salary.ToString());
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Message_LvlMin", job.minLvl.ToString());
            EffectManager.sendUIEffectText(config.EffectID_NewWorkModal, uplayer.SteamPlayer().transportConnection, true, "Message_JobDescription", job.description.ToString());
            uplayer.Player.serversideSetPluginModal(true);
            ModalManager.createModal(uplayer, config.EffectID_Hint);

            return;
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
            ModalManager.createModal(uplayer, config.EffectID_Hint);
            uplayer.Player.serversideSetPluginModal(true);
		}

		public static void parkHUD(UnturnedPlayer uplayer)
		{
            ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_Park));
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Park), config.EffectID_Park, uplayer.SteamPlayer().transportConnection, false);
            ModalManager.createModal(uplayer, config.EffectID_Hint);
            uplayer.Player.serversideSetPluginModal(true);
        }




        //CLEAR ALL----------------------------------//
        public static void updateHUD(MundoPlayer player)
		{
            UnturnedPlayer uplayer = UnturnedPlayer.FromCSteamID(player.steamid);
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_HUD), Convert.ToInt16(config.EffectID_HUD), uplayer.SteamPlayer().transportConnection, false, player.level.ToString(), player.xp.ToString(), player.rp.ToString(), player.jobName);
		}

        //SUCESSO!
        public static void sucesso(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, "sucesso!".ToUpper());
        }
        public static void sucesso(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, param.ToUpper());
            Rocket.Core.Logging.Logger.Log(param);
        }

        //ERRO!
        public static void erro(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, "erro!".ToUpper());
            Rocket.Core.Logging.Logger.Log("Erro!");

        }
        public static void erro(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, param.ToUpper());
            Rocket.Core.Logging.Logger.Log(param);

        }

        public static void erroSintaxe(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, "Erro de Sintaxe!");
            Rocket.Core.Logging.Logger.Log("Erro!");
        }

        //ALERTA!
        public static void alerta(UnturnedPlayer player, string param)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, param.ToUpper());
            Rocket.Core.Logging.Logger.Log(param);
        }

        //EXP!
        public static void exp(UnturnedPlayer player, int amount)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, "você recebeu "+amount+" ponto de EXP!".ToUpper());
        }

        //LEVEL!
        public static void lvl(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Notification), config.EffectID_Notification, player.SteamPlayer().transportConnection, false, "você passou de nível!".ToUpper());
        }        
        
        public static void hintJob(UnturnedPlayer player, string text, string command)
        {
            EffectManager.sendUIEffect(Convert.ToUInt16(config.EffectID_Hint), config.EffectID_Hint, player.SteamPlayer().transportConnection, false);
            EffectManager.sendUIEffectText(config.EffectID_Hint, player.SteamPlayer().transportConnection, true, "Hint_Title", "Dica de Trabalho");
            EffectManager.sendUIEffectText(config.EffectID_Hint, player.SteamPlayer().transportConnection, true, "Hint_Text", text);
            EffectManager.sendUIEffectText(config.EffectID_Hint, player.SteamPlayer().transportConnection, true, "Hint_Command", command);
        }
    }
}
