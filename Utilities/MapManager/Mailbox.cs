using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MundoRP
{
	public class Mailbox
	{
		public string name;

		public float x;
		
		public float y;
		
		public float z;



		private Mailbox() { }
		public Mailbox(string Name, float X, float Y, float Z)
		{
			name = Name;
			x = Convert.ToInt32(X);
			y = Convert.ToInt32(Y);
			z = Convert.ToInt32(Z);
		}
	}
}
