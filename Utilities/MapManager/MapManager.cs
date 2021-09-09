using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class MapManager
	{
        //VARIABLES
        Random rdn = new Random();

        //METHODS
        public Poste GetPoste(int id)
        {
            return Main.Instance.Eletricista_postes[id];
        }

        public Poste GetRandomPoste()
        {
            return Main.Instance.Eletricista_postes[rdn.Next(0, Main.Instance.Eletricista_postes.Count())];
        }

        public CaixaCorreio GetCaixa(int id)
        {
            return Main.Instance.Entregador_caixascorreios[id];
        }

        public CaixaCorreio GetRandomCaixa()
        {
            return Main.Instance.Entregador_caixascorreios[rdn.Next(0, Main.Instance.Entregador_caixascorreios.Count())];
        }

        public PontoOnibus GetRandomTerminal()
        {
            return Main.Instance.motorista_Terminais[rdn.Next(0, Main.Instance.motorista_Terminais.Count())];
        }
    }
}
