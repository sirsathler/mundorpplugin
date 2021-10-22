using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace MundoRP
{
	public class JobManager_TrabalharCommand : IRocketCommand
	{
		DataManager dataManager = new DataManager();

		AllowedCaller IRocketCommand.AllowedCaller => AllowedCaller.Player;

		string IRocketCommand.Name => "trabalhar";

		string IRocketCommand.Help => "Entra em horario de trabalho!";

		string IRocketCommand.Syntax => "[<trabalhar>]";

		List<string> IRocketCommand.Aliases => new List<string>();

		List<string> IRocketCommand.Permissions => new List<string>();



		void IRocketCommand.Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();
			MapManager mapManager = new MapManager();
			string playerJob = Main.Instance.getPlayerInList(Player.CSteamID.ToString()).job;

			//ELETRICISTA======================================================================//
			if (playerJob == "reciclador")
			{
				if (Main.Instance.Reciclador_servicos.ContainsKey(Player.CSteamID))
				{
					Notificator.erro(Player, "Você já está em um serviço!");
					return;
				}
				if (Player.IsInVehicle && Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Reciclador_Carro)
				{
					Main.Instance.Reciclador_servicos.Add(Player.CSteamID, 0);

					Notificator.job(Player, "Você entrou em serviço! Use /ajuda reciclador");
					EffectManager.sendUIEffect(21000, 21000, Player.CSteamID, false, "COLETE AS LIXEIRAS!", Main.Instance.Reciclador_servicos[Player.CSteamID].ToString() + "/" + Main.Instance.Configuration.Instance.Reciclador_CargasPorTrabalho.ToString(), "use /coletar");
					return;
				}
				else
				{
					Notificator.erro(Player, "Primeiro, retire um caminhão de lixo no ticket da empresa!");
				}
			}
			//==================================================================================//
			else if (playerJob == "eletricista")
			{
				Eletricista_Methods eletricistaMethods = new Eletricista_Methods();

				if (command.Length == 0)
				{
					if (Main.Instance.Eletricista_servicos.ContainsKey(Player.CSteamID))
					{
						Notificator.erro(Player, "você já está em um serviço!");
						return;
					}
					if (Player.IsInVehicle && Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Eletricista_Carro)
					{
						try
						{
							Main.Instance.Eletricista_servicos.Add(Player.CSteamID, new List<Poste>());
							Main.Instance.Eletricista_servicos[Player.CSteamID].Clear();
							for (int i = 0; i < Main.Instance.Configuration.Instance.Eletricista_CargasPorTrabalho; i++)
							{
								Main.Instance.Eletricista_servicos[Player.CSteamID].Add(mapManager.GetRandomPoste());
							}
							eletricistaMethods.infoPoste(Player.CSteamID);
							Notificator.job(Player, "Você entrou em serviço! Use /ajuda eletricista");
							return;
						}
						catch (Exception ex)
						{
							Notificator.erro(Player);
							Rocket.Core.Logging.Logger.Log(ex);
							return;
						}
					}
					else
					{
						Notificator.erro(Player, "Primeiro, retire uma caminhonete no ticket da empresa!");
					}
				}
			}
			//CARTEIRO ================================================================================//
			else if (playerJob == "entregador")
			{
				Entregador_Methods entregador_Methods = new Entregador_Methods();

				if (command.Length == 0)
				{
					if (Main.Instance.Entregador_servicos.ContainsKey(Player.CSteamID))
					{
						Notificator.erro(Player, "você já está em um serviço!");
						return;
					}
					if (Player.IsInVehicle && Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Entregador_Carro && Player.CurrentVehicle.instanceID == Main.Instance.vehicleList[Player.CSteamID].iv.instanceID)
					{
						if (Main.Instance.vehicleList[Player.CSteamID].gv.usable)
						{
							try
							{
								Main.Instance.Entregador_servicos.Add(Player.CSteamID, new List<CaixaCorreio>());
								Main.Instance.Entregador_servicos[Player.CSteamID].Clear();
								for (int i = 0; i < Main.Instance.Configuration.Instance.Entregador_CargasPorTrabalho; i++)
								{
									Main.Instance.Entregador_servicos[Player.CSteamID].Add(mapManager.GetRandomCaixa());
								}
								entregador_Methods.infoCaixa(Player.CSteamID);
								Notificator.job(Player, "Você entrou em serviço! Use /ajuda entregador");
								return;
							}
							catch (Exception ex)
							{
								Notificator.erro(Player);
								Rocket.Core.Logging.Logger.Log(ex);
								return;
							}
						}
						else
						{
							Notificator.erro(Player, "Essa van está vazia! Busque outra no ticket da empresa!");
						}
					}
					else
					{
						Notificator.erro(Player, "Primeiro, retire uma van no ticket da empresa!");
					}
				}
			}
			//====================================================================//
			else if (playerJob == "motorista")
			{
				Motorista_Methods motorista_Methods = new Motorista_Methods();

				if (command.Length == 0)
				{
					if (Main.Instance.motorista_servicos.ContainsKey(Player.CSteamID))
					{
						Notificator.erro(Player, "você já está em um serviço!");
						return;
					}
					try
					{
						if (Player.IsInVehicle && Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Motorista_Carro && Player.CurrentVehicle.instanceID == Main.Instance.vehicleList[Player.CSteamID].iv.instanceID)
						{
							if (Main.Instance.vehicleList[Player.CSteamID].gv.usable)
							{
								List<PontoOnibus> terminais = new List<PontoOnibus>();
								Main.Instance.motorista_servicos.Add(Player.CSteamID, new List<PontoOnibus>());
								Main.Instance.motorista_servicos[Player.CSteamID].Add(mapManager.GetRandomTerminal());
								foreach (PontoOnibus pt in Main.Instance.motorista_PontosOnibus)
								{
									Main.Instance.motorista_servicos[Player.CSteamID].Add(pt);
								}
								Main.Instance.motorista_servicos[Player.CSteamID].Add(mapManager.GetRandomTerminal());
								motorista_Methods.infoPonto(Player.CSteamID, "PEGUE PASSAGEIROS NO PONTO");
								Notificator.job(Player, "Você entrou em serviço! Use /ajuda motorista");
								return;
							}
							else
							{
								Notificator.erro(Player, "Esse ônibus já foi usado! Busque outro no ticket da empresa!");
							}
						}
						else
						{
							Notificator.erro(Player, "Primeiro, retire um ônibus no ticket do terminal!");
						}
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
						return;
					}
				}
			} 
			else
			{
				Notificator.erro(Player, "Emprego inexistente! Contate o administrador");
				return;
			}
		}
	}


	//DESCARREGAR---------------------------------------------------------//
	public class JobManager_DescarregarCommand : IRocketCommand
	{
		DataManager dataManager = new DataManager();

		AllowedCaller IRocketCommand.AllowedCaller => AllowedCaller.Player;

		string IRocketCommand.Name => "descarregar";

		string IRocketCommand.Help => "Descarrega a carga para finalizar o trabalho!";

		string IRocketCommand.Syntax => "[<descarregar>]";

		List<string> IRocketCommand.Aliases => new List<string>();

		List<string> IRocketCommand.Permissions => new List<string>();

		public void Execute(IRocketPlayer caller, string[] command)
		{
		DataManager DBManager = new DataManager();
		UnturnedPlayer Player = (UnturnedPlayer)caller;
		NotificationManager Notificator = new NotificationManager();

			if (command.Length == 0)
			{
				if (DBManager.getPlayerBySteamId(Player.CSteamID).job == "reciclador")
				{
					foreach (Aterro lx in Main.Instance.Reciclador_aterros)
					{
						if (Vector3.Distance(Player.Position, new Vector3(lx.x, lx.y, lx.z)) < Main.Instance.Configuration.Instance.Reciclador_MinRange)
						{
							if (Main.Instance.Reciclador_servicos[Player.CSteamID] == Main.Instance.Configuration.Instance.Reciclador_CargasPorTrabalho)
							{

								if (Player.IsInVehicle == true && Player.CurrentVehicle.id == Main.Instance.Configuration.Instance.Reciclador_Carro)
								{
									VehicleManager_Methods vmethods = new VehicleManager_Methods();
									Notificator.sucesso(Player, "Você descarregou o caminhão!");
									EffectManager.sendUIEffect(16520, 16520, Player.CSteamID, false);
									EffectManager.askEffectClearByID(21000, Player.CSteamID);
									Main.Instance.Reciclador_servicos.Remove(Player.CSteamID);
									vmethods.clearVehiclesByID(Player.CSteamID);
									return;
								}
									
								Notificator.erro(Player, "Você precisa estar em um caminhão para descarregar!");
								return;

							}
							else
							{
								Notificator.erro(Player, "Você não tem carga suficiente para descarregar!");
								return;
							}
						}
					}

					Notificator.erro(Player, "Você não está em um lixão!");
					return;
				}
				else
				{
					Notificator.erro(Player, "Você não é um reciclador!");
				}
			}
			else
			{
				Notificator.erro(Player, "Erro de sintaxe!");
			}
		}
	}
}
