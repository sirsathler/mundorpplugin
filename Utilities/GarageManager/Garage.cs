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

		[XmlElement(ElementName = "ang")]
		public Vector3 ang;

		[XmlElement(ElementName = "nome")]
		public string nome;

		public Garage(string Nome, float X, float Y, float Z, Vector3 Ang) //Garage PESSOAL
		{
			nome = Nome;
			x = X;
			y = Y;
			z = Z;
			ang = Ang;
		}
		private Garage() { }
	}
}
