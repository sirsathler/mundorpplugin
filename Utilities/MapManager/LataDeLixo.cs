using System;
using System.Xml.Serialization;
using UnityEngine;

namespace MundoRP
{
	public class LataDeLixo
	{
		public DateTime Cooldown;

		public int id;
		public float x;
		public float y;
		public float z;

		public LataDeLixo(float X, float Y, float Z)
		{
			id = Main.Instance.Reciclador_latasdelixo.Count + 1;
			x = X;
			y = Y;
			z = Z;
		}

		private LataDeLixo() { }
	}
}
