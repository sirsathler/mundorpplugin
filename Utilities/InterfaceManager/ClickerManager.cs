using System;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;


namespace MundoRP
{
	class ClickerManager
	{
        public static void uiButtonClick(Player player, string buttonName)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            MundoPlayer mplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());
            Configuration config = Main.Instance.Configuration.Instance;



            if (buttonName == "CloseButton")
            {
                ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_Garage));
                ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_Park));
                ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_NewWorkModal));
                return;
            }





            if (buttonName == "ActionModal")
            {
                JobManager.startPlayerJob(mplayer);
            }




            if (buttonName.Substring(0, 4) == "Car#")
            {
                int cardId = Convert.ToInt32(buttonName.Substring(4, 1));
                mplayer = MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString());
                if (cardId > mplayer.vehicleList.Count)
                {
                    InterfaceManager.erro(uplayer, "Você não possui veículos nessa vaga!");
                    return;
                }
                if (mplayer.vehicleList[cardId - 1].vehicleDebts.Count != 0)
                {
                    InterfaceManager.erro(uplayer, "Veículo com débitos! Procure uma prefeitura!");
                    return;
                }

                int carTableId = mplayer.vehicleList[Convert.ToInt32(buttonName.Substring(4, 1)) - 1].tableId;

                if (carTableId == MundoPlayer.getPlayerInList(uplayer.CSteamID.ToString()).actualCar)
                {
                    InterfaceManager.alerta(uplayer, "Entre no veículo para guardá-lo na garagem!");
                    return;
                }
                if (mplayer.actualCar != 0)
                {
                    InterfaceManager.erro(uplayer, "Você deve guardar o seu veículo primeiro antes retirar outro da garagem!");
                    return;
                }
                MundoVehicleManager.giveVehicle(uplayer, cardId, carTableId, GarageManager.getNearbyGarage(uplayer));
                ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_Garage));
                InterfaceManager.sucesso(uplayer, "Veículo retirado da garagem!");
            }







            if (buttonName == "ParkButton")
            {
                MundoVehicleManager.clearVehiclesByID(uplayer.CSteamID);
                DataManager.updateCar(Main.Instance.MundoVehicle_Vehicles[uplayer.CSteamID].iv, Main.Instance.MundoVehicle_Vehicles[uplayer.CSteamID].gv.tableId);
                mplayer.actualCar = 0;
                ModalManager.uiClose(uplayer, Convert.ToUInt16(config.EffectID_Park));
                InterfaceManager.sucesso(uplayer, "Você guardou seu veículo na garagem!");
                return;
            }

        }
    }
}
