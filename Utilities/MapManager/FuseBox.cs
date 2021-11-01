using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace MundoRP
{
	public class FuseBox
	{
		public string name { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public int z { get; set; }


		public FuseBox(string Name, float X, float Y, float Z)
		{
			name = Name;
			x = Convert.ToInt32(X);
			y = Convert.ToInt32(Y);
			z = Convert.ToInt32(Z);
		}

		private FuseBox() { }
	}
}
