using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
	public class ObjectManager
	{
		public List<Aterro> Aterros;
		public List<CaixaCorreio> CaixasCorreios;
		public List<LataDeLixo> LatasDeLixo;
		public List<PontoOnibus> PontosDeOnibus;
		public List<Poste> Postes;

		public ObjectManager(List<Aterro> aterros, List<CaixaCorreio> caixasCorreios, List<LataDeLixo> latasDeLixo, List<PontoOnibus> pontosDeOnibus, List<Poste> postes)
		{
			Aterros = aterros;
			CaixasCorreios = caixasCorreios;
			LatasDeLixo = latasDeLixo;
			PontosDeOnibus = pontosDeOnibus;
			Postes = postes;
		}
	}
}
