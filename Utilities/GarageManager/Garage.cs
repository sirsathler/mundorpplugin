using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;


namespace MundoRP
{
	public class Garage
	{
		[XmlIgnore]
		public DateTime Cooldown;

		[XmlElement(ElementName = "x")]
		public float x;

		[XmlElement(ElementName = "y")]
		public float y;
		
		[XmlElement(ElementName = "z")]
		public float z;

		[XmlElement(ElementName = "angle")]
		public Vector3 angle;

		[XmlElement(ElementName = "nome")]
		public string name;

		public Garage(string Name, float X, float Y, float Z, Vector3 Angle)
		{
			name = Name;
			x = X;
			y = Y;
			z = Z;
			angle = Angle;
		}
		private Garage() { }
	}
}
