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
	public class Aterro
	{
		public int id;
		public float x;
		public float y;
		public float z;


		public Aterro(float X, float Y, float Z)
		{
			id = Main.Instance.Reciclador_aterros.Count + 1;

			x = X;
			y = Y;
			z = Z;
		}

		private Aterro() { }
	}
}
