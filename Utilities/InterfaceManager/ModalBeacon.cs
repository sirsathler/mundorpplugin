using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MundoRP
{
	public class ModalBeacon
	{
		public Vector3 position;
		public float range;
		public ushort id;
		public ModalBeacon(Vector3 pos, float rang, ushort identificator)
		{
			position = pos;
			range = rang;
			id = identificator;
		}
	}
}
