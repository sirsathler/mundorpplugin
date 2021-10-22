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
		public ModalBeacon(Vector3 pos, float ran)
		{
			position = pos;
			range = ran;
		}
	}
}
