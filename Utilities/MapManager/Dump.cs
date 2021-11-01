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
	public class Dump
	{
		public string name;
		public float x;
		public float y;
		public float z;


		public Dump(string Name, float X, float Y, float Z)
		{
			name = Name;
			x = Convert.ToInt32(X);
			y = Convert.ToInt32(Y);
			z = Convert.ToInt32(Z);
		}

		private Dump() { }
	}
}
