using _04_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDiBordo
{
    internal interface IDAOPagina
    {
        public List<Pagina> RicercaPerTempo(DateTime dataIniziale, DateTime dataFinale);
        public List<Pagina> RicercaPerLuogo(string luogo);
        public List<Pagina> RicercaPerDescrizione(string descrizione);
    }
}
