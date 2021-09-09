using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace MundoRP
{
	public class Poste
	{
		[XmlElement(ElementName = "nome")]
		public string nome { get; set; }

		[XmlElement(ElementName = "x")]
		public float x { get; set; }

		[XmlElement(ElementName = "y")]
		public float y { get; set; }

		[XmlElement(ElementName = "z")]
		public float z { get; set; }


		public Poste(float X, float Y, float Z, string Nome)
		{
			nome = Nome;
			x = X;
			y = Y;
			z = Z;
		}

		private Poste() { }
	}
}
