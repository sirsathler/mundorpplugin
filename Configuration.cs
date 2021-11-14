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
		
		[XmlElement(ElementName = "EffectID_Garage")]
		public short EffectID_Garage;

		[XmlElement(ElementName = "EffectID_Park")]
		public short EffectID_Park;
		
		[XmlElement(ElementName = "EffectID_HUD")]
		public short EffectID_HUD;

		[XmlElement(ElementName = "EffectID_Sucess")]
		public short EffectID_Sucess;
		
		[XmlElement(ElementName = "EffectID_Alert")]
		public short EffectID_Alert;
		
		[XmlElement(ElementName = "EffectID_Error")]
		public short EffectID_Error;
		
		[XmlElement(ElementName = "EffectID_Level")]
		public short EffectID_Level;
				
		[XmlElement(ElementName = "EffectID_Exp")]
		public short EffectID_Exp;	

		[XmlElement(ElementName = "Interaction_Range")]
		public float Interaction_Range;	

		[XmlElement(ElementName = "Recicladores_Cooldown")]
		public float Recicladores_Cooldown;	



		[XmlElement(ElementName = "VehicleManager_Cooldown")]
		public double VehicleManager_Cooldown;

		[XmlArray(ElementName = "MundoVehicle_Garages")]
		public List<Garage> MundoVehicle_Garages = new List<Garage>() { };

		[XmlArray(ElementName = "NPC_List")]
		public List<WorkNPC> NPC_List = new List<WorkNPC>();

		public void LoadDefaults()
		{
			EffectID_Error = 14001;
			EffectID_Alert = 14002;
			EffectID_Sucess = 14003;
			EffectID_Exp = 14004;
			EffectID_Level = 14005;

			EffectID_HUD = 14006;

			EffectID_Garage = 14010;
			EffectID_Park = 14011;
			EffectID_NewWorkModal = 14020;
			EffectID_Hint = 14021;
			
			Interaction_Range = 2f;
			Recicladores_Cooldown = 5;
		}
	}
}
