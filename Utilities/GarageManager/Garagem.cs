using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;


namespace MundoRP
{
	public class Garagem
	{
		[XmlIgnore]
		public DateTime Cooldown;

		[XmlElement(ElementName = "x")]
		public float x;

		[XmlElement(ElementName = "y")]
		public float y;
		
		[XmlElement(ElementName = "z")]
		public float z;

		[XmlElement(ElementName = "ang")]
		public Vector3 ang;

		[XmlElement(ElementName = "ticketPosX")]
		public float ticketPosX;

		[XmlElement(ElementName = "ticketPosY")]
		public float ticketPosY;

		[XmlElement(ElementName = "ticketPosZ")]
		public float ticketPosZ;

		[XmlElement(ElementName = "nome")]
		public string nome;

		[XmlElement(ElementName = "type")]
		public int type; //1- Trabalho | 2- Pessoal

		[XmlElement(ElementName = "vehicleId")]
		public ushort vehicleId; //Se type == 2

		[XmlElement(ElementName = "job")]
		public string job; //Se type == 2

		public Garagem(string Nome, float X, float Y, float Z, Vector3 Ang) //GARAGEM PESSOAL
		{
			nome = Nome;
			x = X;
			y = Y;
			z = Z;
			ang = Ang;
			type = 1;
		}
		private Garagem() { }
	}
}
