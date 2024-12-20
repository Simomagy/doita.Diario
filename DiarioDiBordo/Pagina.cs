using _04_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDiBordo
{
    internal class Pagina : Entity
    {
        public DateTime DataScrittura { get; set; }
        public float X {  get; set; }
        public float Y { get; set; }
        public string Luogo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public override string ToString()
        {
            return base.ToString() + $@"
                        {Luogo}
                        Data: {DataScrittura:dddd,dd/MMMM/yy}
                        Cooordinate: {X},{Y}
                        ---------------------------
                        '{Descrizione}'";
        }

    }
}
