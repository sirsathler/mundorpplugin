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
	public class Motorista_Methods
	{
		NotificationManager Notificator = new NotificationManager();
		VehicleManager_Methods vm = new VehicleManager_Methods();

		public void nextPonto(UnturnedPlayer Player)
		{
			Main.Instance.motorista_servicos[Player.CSteamID].RemoveAt(0);
			if (Main.Instance.motorista_servicos[Player.CSteamID].Count > 1)
			{
				try
				{
					infoPonto(Player.CSteamID, "BUSQUE PASSAGEIROS NO PONTO");
					if(Main.Instance.motorista_servicos[Player.CSteamID][0].tipo == "terminal")
					{
						Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player.CSteamID), "Busque passageiros na plataforma indicada!");
					}
					else
					{
						Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player.CSteamID), "Os passageiros subiram! Vá para o próximo ponto!");
					}
				}
				catch (Exception ex)
				{
					Notificator.erro(UnturnedPlayer.FromCSteamID(Player.CSteamID));
					Rocket.Core.Logging.Logger.Log(ex);
				}
			}
			else if (Main.Instance.motorista_servicos[Player.CSteamID].Count == 1)	
			{
				infoPonto(Player.CSteamID, "RETORNE PARA A PLATAFORMA NO TERMINAL");
				Notificator.alerta(UnturnedPlayer.FromCSteamID(Player.CSteamID), "Retorne para o terminal na plataforma indicada!");
			}
			else
			{
				try
				{
					VehicleManager_Methods vehicleManager_Methods = new VehicleManager_Methods();
					vehicleManager_Methods.clearVehiclesByID(Player.CSteamID);
					EffectManager.askEffectClearByID(21003, Player.CSteamID);
					Notificator.sucesso(UnturnedPlayer.FromCSteamID(Player.CSteamID), "Fim da viagem!");
					EffectManager.sendUIEffect(16520, 16520, Player.CSteamID, false);
					Main.Instance.motorista_servicos.Remove(Player.CSteamID);
				}
				catch
				{
					Notificator.erro(Player);

				}
			}
		}
		public void infoPonto(CSteamID Player, string message)
		{
			EffectManager.askEffectClearByID(21003, Player);
			EffectManager.sendUIEffect(21003, 21003, Player, false, message.ToUpper(), Main.Instance.motorista_servicos[Player][0].nome.ToUpper(), "use /ponto");
			return;
		}

	}
}
