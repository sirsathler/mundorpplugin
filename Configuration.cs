using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;
using System;

namespace MundoRP
{
	public class Configuration : IRocketPluginConfiguration
	{
		[XmlElement(ElementName = "GarageBeacon_Range")]
		public float GarageBeacon_Range;
		
		[XmlElement(ElementName = "Interaction_Range")]
		public float Interaction_Range;



		[XmlElement(ElementName = "VehicleManager_Cooldown")]
		public double VehicleManager_Cooldown;

		[XmlArray(ElementName = "VehicleManager_Garagens")]
		public List<Garage> VehicleManager_Garagens = new List<Garage>() { };

		[XmlArray(ElementName = "Job_List")]
		public List<Job> JobsList = new List<Job>();

		public void LoadDefaults()
		{
			GarageBeacon_Range = 4;
			Interaction_Range = 5f;
		}
	}
}
