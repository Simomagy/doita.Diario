using _04_Utility;
using System;
using System.Text.RegularExpressions;

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
            var pattern = @"\.";
            var replacement = ".\n";
            Regex rgx = new(pattern);
            var descrizioneFormattata = rgx.Replace(Descrizione, replacement);
            
            return  $@"
            Pagina {base.Id}
                    {Luogo}
                    Data: {DataScrittura:dddd, dd/MMMM/yy}
                    Cooordinate: X: {X}, Y: {Y} 
                    ---------------------------
                    '{descrizioneFormattata}'
                    ---------------------------";
        }

    }
}
