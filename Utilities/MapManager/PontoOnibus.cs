using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Rocket.API;
using Rocket.Core.Plugins;
using UnityEngine;

namespace MundoRP
{
	public class PontoOnibus
	{
		[XmlElement(ElementName = "nome")]
		public string nome;

		[XmlElement(ElementName = "x")]
		public float x;

		[XmlElement(ElementName = "y")]
		public float y;

		[XmlElement(ElementName = "z")]
		public float z;

		[XmlElement(ElementName = "tipo")]
		public string tipo;

		public PontoOnibus(string Nome, string Tipo, float X, float Y, float Z)
		{
			nome = Nome;
			tipo = Tipo;
			x = X;
			y = Y;
			z = Z;
		}

		private PontoOnibus() { }
	}
}
