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
		public string name;

		public int x;

		public int y;

		public int z;

		public PontoOnibus(string Name, float X, float Y, float Z)
		{
			name = Name;
			x = Convert.ToInt32(X);
			y = Convert.ToInt32(Y);
			z = Convert.ToInt32(Z);
		}

		private PontoOnibus() { }
	}
}
