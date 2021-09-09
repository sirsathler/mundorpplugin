using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MundoRP
{
	public class CaixaCorreio
	{
		[XmlAttribute("nome")]
		public string nome;

		[XmlAttribute("x")]
		public float x;
		
		[XmlAttribute("y")]
		public float y;
		
		[XmlAttribute("z")]
		public float z;



		private CaixaCorreio() { }
		public CaixaCorreio(float X, float Y, float Z, string Nome)
		{
			nome = Nome;
			x = X;
			y = Y;
			z = Z;
		}
	}
}
