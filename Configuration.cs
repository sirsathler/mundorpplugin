using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;
using System;

namespace MundoRP
{
	public class Configuration : IRocketPluginConfiguration
	{
		[XmlElement(ElementName = "EffectID_NewWorkModal")]
		public short EffectID_NewWorkModal;

		[XmlElement(ElementName = "EffectID_Hint")]
		public short EffectID_Hint;

		[XmlElement(ElementName = "EffectID_Notification")]
		public short EffectID_Notification;
		
		[XmlElement(ElementName = "EffectID_Garage")]
		public short EffectID_Garage;

		[XmlElement(ElementName = "EffectID_Park")]
		public short EffectID_Park;
		
		[XmlElement(ElementName = "EffectID_HUD")]
		public short EffectID_HUD;

		[XmlElement(ElementName = "Interaction_Range")]
		public float Interaction_Range;	

		[XmlElement(ElementName = "Recicladores_Cooldown")]
		public float Recicladores_Cooldown;	


		[XmlElement(ElementName = "Jobs_RecicladorGarbageLimit")]
		public double Jobs_RecicladorGarbageLimit;




		[XmlElement(ElementName = "VehicleManager_Cooldown")]
		public double VehicleManager_Cooldown;

		[XmlArray(ElementName = "MundoVehicle_Garages")]
		public List<Garage> MundoVehicle_Garages = new List<Garage>() { };

		[XmlArray(ElementName = "NPC_List")]
		public List<WorkNPC> NPC_List = new List<WorkNPC>();

		public void LoadDefaults()
		{
			EffectID_Notification = 14022;
			EffectID_NewWorkModal = 14023;

			EffectID_Garage = 14010;
			EffectID_Park = 14011;
			EffectID_Hint = 14021;

			Interaction_Range = 1f;
			Recicladores_Cooldown = 5;

			//Jobs
			Jobs_RecicladorGarbageLimit = 5;



		}
	}
}
