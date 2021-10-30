using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Steamworks;
using SDG.Unturned;

namespace MundoRP
{
	public class Eletricista_Methods
	{
		NotificationManager Notificator = new NotificationManager();

		public void nextPoste(CSteamID Player)
		{
			if (Main.Instance.Eletricista_servicos[Player].Count > 1)
			{
				try
				{
					Main.Instance.Eletricista_servicos[Player].RemoveAt(0);
					infoPoste(Player);
					Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player), "Poste consertado! Vá para o próximo!");
				}
				catch (Exception ex)
				{
					Notificator.erro(UnturnedPlayer.FromCSteamID(Player));
					Rocket.Core.Logging.Logger.Log(ex);
				}
			}
			else
			{
				EffectManager.askEffectClearByID(21001, UnturnedPlayer.FromCSteamID(Player).SteamPlayer().transportConnection);
				Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player), "Ultimo poste consertado!");
				EffectManager.sendUIEffect(16520, 16520, UnturnedPlayer.FromCSteamID(Player).SteamPlayer().transportConnection, false);
				Main.Instance.Eletricista_servicos.Remove(Player);
			}
		}

		public void infoPoste(CSteamID Player)
		{
			EffectManager.askEffectClearByID(21001, UnturnedPlayer.FromCSteamID(Player).SteamPlayer().transportConnection);
			EffectManager.sendUIEffect(21001, 21001, UnturnedPlayer.FromCSteamID(Player).SteamPlayer().transportConnection, false, "CONSERTE O POSTE DA CASA", Main.Instance.Eletricista_servicos[Player][0].name.ToLower(), "use /consertar");
		}
	}
}
