using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;
using System;

namespace MundoRP
{
	public class Configuration : IRocketPluginConfiguration
	{
		// VEHICLEMANAGER ============================================================= //
		[XmlElement(ElementName = "VehicleManager_MinRange")]
		public float VehicleManager_MinRange;

		[XmlElement(ElementName = "VehicleManager_Cooldown")]
		public double VehicleManager_Cooldown;

		[XmlArray(ElementName = "VehicleManager_Garagens")]
		public List<Garagem> VehicleManager_Garagens = new List<Garagem>() { };

		// MOTORISTA ================================================================== //
		[XmlElement(ElementName = "Motorista_MinRange")]
		public float Motorista_MinRange;

		[XmlElement(ElementName = "Motorista_Carro")]
		public ushort Motorista_Carro;

		// ENTREGADOR =================================================================== //
		[XmlElement("Entregador_Range")]
		public float Entregador_Range;

		[XmlElement("Entregador_CargasPorTrabalho")]
		public int Entregador_CargasPorTrabalho;
		
		[XmlElement("Entregador_Carro")]
		public ushort Entregador_Carro;

		// ELETRICISTA ================================================================ //
		[XmlElement("Eletricista_MinRange")]
		public float Eletricista_MinRange;

		[XmlElement("Eletricista_CargasPorTrabalho")]
		public int Eletricista_CargasPorTrabalho;

		[XmlElement("Eletricista_Carro")]
		public ushort Eletricista_Carro;

		// LIXEIRO ===================================================================== //
		[XmlElement("Reciclador_MinRange")]
		public float Reciclador_MinRange;

		[XmlElement("Reciclador_CargasPorTrabalho")]
		public int Reciclador_CargasPorTrabalho;

		[XmlElement("Reciclador_Cooldown")]
		public float Reciclador_Cooldown;

		[XmlElement("Reciclador_Carro")]
		public ushort Reciclador_Carro;

		public void LoadDefaults()
		{
			//MAPMANAGER

			//VEHICLEMANAGER
			VehicleManager_MinRange = 4;
			VehicleManager_Cooldown = 10; //Em Segundos!


			//ELETRICISTAS
			Eletricista_MinRange = 5;
			Eletricista_CargasPorTrabalho = 3;
			Eletricista_Carro = 36187;

			//ENTREGADORES
			Entregador_CargasPorTrabalho = 5;
			Entregador_Range = 3;
			Entregador_Carro = 36417;

			//LIXEIROS
			Reciclador_CargasPorTrabalho = 3;
			Reciclador_Cooldown = 10;
			Reciclador_MinRange = 5;
			Reciclador_Carro = 56193;

			//MOTORISTAS
			Motorista_MinRange = 10;
			Motorista_Carro = 47229;
		}
	}
}
