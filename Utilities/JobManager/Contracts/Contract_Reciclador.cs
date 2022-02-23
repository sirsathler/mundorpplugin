using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoRP
{
    public class Contract_Reciclador : JobContract
    {
        int garbageCollected;
        public Contract_Reciclador(string workerSteamId)
        {
            this.workerSteamId = workerSteamId;
            garbageCollected = 0;
        }
    }
}
