using System;
using System.Xml.Serialization;
using UnityEngine;

namespace MundoRP
{
	public class Garbage
	{
		public DateTime Cooldown;

		public int id;
		public float x;
		public float y;
		public float z;

		public Garbage(float X, float Y, float Z)
		{
			id = Main.Instance.ObjList_Garbages.Count + 1;
			x = Convert.ToInt32(X);
			y = Convert.ToInt32(Y);
			z = Convert.ToInt32(Z);
		}

		private Garbage() { }
	}
}
