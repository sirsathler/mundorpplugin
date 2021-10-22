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
		public string nome;

		public float x;

		public float y;

		public float z;

		public PontoOnibus(string Nome, float X, float Y, float Z)
		{
			nome = Nome;
			x = X;
			y = Y;
			z = Z;
		}

		private PontoOnibus() { }
	}
}
