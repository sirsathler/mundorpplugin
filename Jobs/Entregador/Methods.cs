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
	public class Entregador_Methods
	{
		Random rdn = new Random();
		NotificationManager Notificator = new NotificationManager();
		public CaixaCorreio GetCaixa(int id)
		{
			return Main.Instance.Entregador_caixascorreios[id];
		}

		public CaixaCorreio GetRandomCaixa()
		{
			return Main.Instance.Entregador_caixascorreios[rdn.Next(0, Main.Instance.Entregador_caixascorreios.Count())];
		}

		public void nextCaixa(CSteamID Player)
		{
			if (Main.Instance.Entregador_servicos[Player].Count > 1)
			{
				try
				{
					Main.Instance.Entregador_servicos[Player].RemoveAt(0);
					infoCaixa(Player);
				}
				catch (Exception ex)
				{
					Notificator.erro(UnturnedPlayer.FromCSteamID(Player));
					Rocket.Core.Logging.Logger.Log(ex);
				}
			}
			else
			{
				EffectManager.askEffectClearByID(21002, Player);
				Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player), "Ultima correspondência entregada!");
				VehicleManager_Methods vehicleManager_Methods = new VehicleManager_Methods();
				EffectManager.sendUIEffect(16520, 16520, Player, false);
				Main.Instance.Entregador_servicos.Remove(Player);
				Main.Instance.vehicleList[Player].usable = false;
			}
		}

		public void infoCaixa(CSteamID Player)
		{
			EffectManager.askEffectClearByID(21002, Player);
			EffectManager.sendUIEffect(21002, 21002, Player, false, "ENTREGUE A ENCOMENDA", Main.Instance.Entregador_servicos[Player][0].nome.ToLower(), "use /entregar");
		}
	}
}
