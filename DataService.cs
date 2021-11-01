using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace MundoRP
{
    public partial class Main : RocketPlugin<Configuration>
    {
        public bool setData()
        {
            ObjectManager objManager = new ObjectManager(Instance.ObjList_Dumps, Instance.ObjList_Mailbox, Instance.ObjList_Garbages, Instance.ObjList_BusStops, Instance.ObjList_Fuses);
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.Dumps.Count + " Aterros sanitários!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.MailBoxes.Count + " Caixas de Correios!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.Garbages.Count + " Latas de Lixo!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.BusStops.Count + " Pontos de Ônibus!");
            Rocket.Core.Logging.Logger.Log("Adicionando ao banco: " + objManager.FuseBoxes.Count + " Fusíveis!");
            return dataManager.updateObjects(objManager);
        }

        public void readData()
        {
            Instance.ObjList_Fuses.Clear();
            Instance.ObjList_Dumps.Clear();
            Instance.ObjList_Garbages.Clear();
            Instance.ObjList_BusStops.Clear();
            Instance.ObjList_Mailbox.Clear();
            Instance.VehicleManager_garagens.Clear();

            foreach (Garage gr in Configuration.Instance.VehicleManager_Garagens)
            {
                Main.Instance.VehicleManager_garagens.Add(gr);
                Rocket.Core.Logging.Logger.Log("Adicionada a garagem: " + gr.nome);
            }

            ObjectManager objManager = dataManager.getObjectsFromDB();

            Main.Instance.ObjList_Garbages = objManager.Garbages;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Garbages.Count.ToString() + " Latas de Lixos.");

            Main.Instance.ObjList_Fuses = objManager.FuseBoxes;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Fuses.Count.ToString() + " Fusíveis.");

            Main.Instance.ObjList_Mailbox = objManager.MailBoxes;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Mailbox.Count.ToString() + " Caixas de Correios.");

            Main.Instance.ObjList_Dumps = objManager.Dumps;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Dumps.Count.ToString() + " Aterros Sanitários.");

            Main.Instance.ObjList_BusStops = objManager.BusStops;
            Rocket.Core.Logging.Logger.Log("Adicionados: " + Main.Instance.ObjList_Dumps.Count.ToString() + " Pontos de Ônibus.");

        }
    }
}
