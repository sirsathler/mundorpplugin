using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;

namespace MundoRP
{
	public class MapManager_Commands : IRocketCommand
	{
		public AllowedCaller AllowedCaller => AllowedCaller.Player;

		public string Name => "mapmanager";

		public string Help => "Cria a cidade!";

		public string Syntax => "[<mapmanager>]";

		public List<string> Aliases => new List<string>() {"mm", "mapm"};

		public List<string> Permissions => new List<string>() { "reciclador" };
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer Player = (UnturnedPlayer)caller;
			NotificationManager Notificator = new NotificationManager();
			VehicleManager_Methods vehicle_Methods = new VehicleManager_Methods();

			if (command.Length == 0) // /LIXEIROS
			{
				//HELP
			}
			else
			{
				if (command[0] == "d-garagem" || command[0] == "-ga")
				{
					if (command.Length == 2)
					{
						foreach (Garage gr in Main.Instance.VehicleManager_garagens)
						{
							if (gr.nome == command[1])
							{
								try
								{
									Main.Instance.VehicleManager_garagens.Remove(gr);
									Main.Instance.Configuration.Instance.VehicleManager_Garagens.Remove(gr);
									Main.Instance.Configuration.Save();
									Notificator.sucesso(Player, "Garage " + gr.nome + " removida com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}
						Notificator.erro(Player, "A garagem " + command[1] + " não existe!");
					}
				}

				else if (command[0] == "c-garagem" || command[0] == "+ga")
				{
					if (command.Length == 2)
					{
						foreach (Garage gr in Main.Instance.VehicleManager_garagens)
						{
							if (gr.nome == command[1])
							{
								Notificator.erro(Player, "Já existe uma garagem com esse nome!");
								return;
							}
							if (Vector3.Distance(new Vector3(gr.x, gr.y, gr.z), new Vector3(Player.Position.x, Player.Position.y, Player.Position.z)) < 5)
							{
								Notificator.erro(Player, "Muito próximo de outra garagem!");
								return;
							}
						}
						vehicle_Methods.criarGarage(new Garage(command[1], Player.Position.x, Player.Position.y, Player.Position.z, Player.Player.transform.rotation.eulerAngles));
						Notificator.sucesso(Player, "Garagem " + command[1] + " Criada!");
						return;
					}
					else
					{
						Notificator.erro(Player, "Erro de sintaxe!");
						return;
					}
				}

				else if (command[0] == "c-lixeira" || command[0] == "+li")
				{
					foreach (LataDeLixo lx in Main.Instance.Reciclador_latasdelixo)
					{
						if (Vector3.Distance(Player.Position, new Vector3(lx.x, lx.y, lx.z)) < Main.Instance.Configuration.Instance.Reciclador_MinRange)
						{
							Notificator.erro(Player, "Posição inválida para uma nova lixeira!");
							return;
						}
					}
					try
					{
						Main.Instance.Reciclador_latasdelixo.Add(new LataDeLixo(Player.Position.x, Player.Position.y, Player.Position.z));
						Notificator.sucesso(Player, "Lata de lixo criada com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
					}
				}

				else if (command[0] == "c-aterro" || command[0] == "+at")
				{
					Main.Instance.Reciclador_aterros.Add(new Aterro(Player.Position.x, Player.Position.y, Player.Position.z));
					Main.Instance.Configuration.Save();
					Notificator.sucesso(Player, "Aterro criado com sucesso!");
					return;
				}

				else if (command[0] == "d-lixeira" || command[0] == "-li")
				{
					if (command.Length == 1)
					{
						foreach (LataDeLixo lx in Main.Instance.Reciclador_latasdelixo)
						{
							if (Vector3.Distance(new Vector3(lx.x, lx.y, lx.z), Player.Position) < 5)
							{
								try
								{
									Main.Instance.Reciclador_latasdelixo.Remove(lx);
									Notificator.sucesso(Player, "Lata de Lixo apagado com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}

						Notificator.erro(Player, "Você não está perto de uma lata de lixo!");
						return;
					}
					else
					{
						try
						{
							Notificator.sucesso(Player, "Lata de lixo apagada com sucesso!");
							return;
						}
						catch (Exception ex)
						{
							Rocket.Core.Logging.Logger.Log(ex);
							return;
						}
					}
				}

				else if ((command[0] == "d-aterro" || command[0] == "-at") && Convert.ToInt32(command[1]) > 0)  // /ALIX [1]DELETEBIN [2]ID
				{
					try
					{
						Main.Instance.Reciclador_aterros.RemoveAt(Convert.ToInt32(command[2]));
						Notificator.sucesso(Player, "Aterro apagado com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
						return;
					}
				}


				//PONTOS
				else if (command[0] == "c-ponto" || command[0] == "+po")
				{
					if (Main.Instance.motorista_PontosOnibus.Count > 0)
					{
						foreach (PontoOnibus po in Main.Instance.motorista_PontosOnibus)
						{
							if (Vector3.Distance(Player.Position, new Vector3(po.x, po.y, po.z)) <= Main.Instance.Configuration.Instance.Motorista_MinRange)
							{
								Notificator.erro(Player, "muito próximo a outro ponto! escolha outro lugar.");
								return;
							}
							if (po.nome == command[1])
							{
								Notificator.erro(Player, "Já existe um ponto com esse nome!");
								return;
							}
						}
					}
					PontoOnibus pon;
					if (command.Length > 1 && (command[2] == "terminal" || command[2] == "+te"))
					{
						pon = new PontoOnibus(command[1].ToString(), Player.Position.x, Player.Position.y, Player.Position.z);
						Main.Instance.motorista_Terminais.Add(pon);
					}
					else
					{
						pon = new PontoOnibus(command[1].ToString(), Player.Position.x, Player.Position.y, Player.Position.z);
					}
					try
					{
						Main.Instance.motorista_PontosOnibus.Add(pon);
						Notificator.sucesso(Player, "ponto criado com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
						return;
					}
				}
				else if (command[0] == "d-ponto" || command[0] == "-po")
				{
					if (command.Length == 1)
					{
						foreach (PontoOnibus po in Main.Instance.motorista_PontosOnibus)
						{
							if (Vector3.Distance(new Vector3(po.x, po.y, po.z), Player.Position) < 5)
							{
								try
								{
									Main.Instance.motorista_PontosOnibus.Remove(po);
									Notificator.sucesso(Player, "Ponto apagado com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}
						Notificator.erro(Player, "Você não está perto de um ponto de ônibus!");
						return;
					}
					else
					{
						foreach (PontoOnibus po in Main.Instance.motorista_PontosOnibus)
						{
							if (po.nome == command[1])
							{
								try
								{
									Main.Instance.motorista_PontosOnibus.Remove(po);
									Notificator.sucesso(Player, "Ponto apagado com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}
					}

					Notificator.erro(Player, "Ponto nao encontrado!");
					return;
				}



				//POSTES
				else if (command[0] == "c-poste" || command[0] == "+po")
				{
					if (Main.Instance.Eletricista_postes.Count > 0)
					{
						foreach (Poste pst in Main.Instance.Eletricista_postes)
						{
							if (Vector3.Distance(Player.Position, new Vector3(pst.x, pst.y, pst.z)) <= Main.Instance.Configuration.Instance.Eletricista_MinRange)
							{
								Notificator.erro(Player, "muito próximo a outro poste! escolha outro lugar.");
								return;
							}
							if (pst.nome == command[1])
							{
								Notificator.erro(Player, "Já existe um poste com esse nome!");
								return;
							}
						}
					}

					try
					{
						Main.Instance.Eletricista_postes.Add(new Poste(Player.Position.x, Player.Position.y, Player.Position.z, command[1]));
						Notificator.sucesso(Player, "poste criado com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
						return;
					}
				}
				else if (command[0] == "d-poste" || command[0] == "-po")
				{
					if (command.Length == 1)
					{
						foreach (Poste pst in Main.Instance.Eletricista_postes)
						{
							if(Vector3.Distance(new Vector3(pst.x, pst.y, pst.z), Player.Position) < 5)
							{
								try
								{
									Main.Instance.Eletricista_postes.Remove(pst);
									Notificator.sucesso(Player, "Poste apagado com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}
						Notificator.erro(Player, "Você não está perto de um poste!");
						return;
					}
					else
					{
						foreach (Poste pst in Main.Instance.Eletricista_postes)
						{
							if (pst.nome == command[1])
							{
								try
								{
									Main.Instance.Eletricista_postes.Remove(pst);
									Notificator.sucesso(Player, "Poste apagado com sucesso!");
									return;
								}
								catch (Exception ex)
								{
									Notificator.erro(Player);
									Rocket.Core.Logging.Logger.Log(ex);
									return;
								}
							}
						}
					}

					Notificator.erro(Player, "Poste nao encontrado!");
					return;
				}




				//CARTEIROS==============================================================

				else if (command[0] == "c-caixa" || command[0] == "cc")
				{
					if (Main.Instance.Entregador_caixascorreios.Count > 0)
					{
						foreach (CaixaCorreio cc in Main.Instance.Entregador_caixascorreios)
						{
							if(cc.nome == command[1])
							{
								Notificator.erro(Player, "Já possui uma caixa de correio com esse endereço!");
								return;
							}
							else if (Vector3.Distance(Player.Position, new Vector3(cc.x, cc.y, cc.z)) <= Main.Instance.Configuration.Instance.Entregador_Range)
							{
								Notificator.erro(Player, "Muito próximo a outra caixa! Escolha outro lugar.");
								return;
							}
						}
					}
					try
					{
						Main.Instance.Entregador_caixascorreios.Add(new CaixaCorreio(Player.Position.x, Player.Position.y, Player.Position.z, command[1]));
						Notificator.sucesso(Player, "Caixa de Correio criado com sucesso!");
						return;
					}
					catch (Exception ex)
					{
						Notificator.erro(Player);
						Rocket.Core.Logging.Logger.Log(ex);
						return;
					}
				}
				else if (command[0] == "d-caixa" || command[0] == "cd")
				{
					foreach (CaixaCorreio cx in Main.Instance.Entregador_caixascorreios)
					{
						if (cx.nome == command[1])
						{
							try
							{
								Main.Instance.Entregador_caixascorreios.Remove(cx);
								Notificator.sucesso(Player, "Caixa de Correio apagada com sucesso!");
								return;
							}
							catch (Exception ex)
							{
								Notificator.erro(Player);
								Rocket.Core.Logging.Logger.Log(ex);
								return;
							}
						}
					}

					Notificator.erro(Player, "Caixa de Correio não encontrada!");
					return;
				}
				else
				{
					Notificator.erro(Player, "Erro de Sintaxe!");
				}
			}
		}
	}
}
