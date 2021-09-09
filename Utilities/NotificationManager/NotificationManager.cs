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

        //CLEAR ALL----------------------------------//


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
    }
}
