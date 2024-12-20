//@formatter: on

using _04_Utility;
using System.Text.RegularExpressions;

namespace DiarioDiBordo
{
    internal class Pagina : Entity
    {
        public DateTime DataScrittura { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Luogo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public override string ToString()
        {
            const string pattern = @"\.";
            const string replacement = ".\n";
            var rgx = new Regex(pattern);
            var descrizioneFormattata = rgx.Replace(Descrizione, replacement);

            return $"""
                    Pagina {Id}
                            {Luogo}
                            Data: {DataScrittura:dddd, dd/MMMM/yy}
                            Cooordinate: X: {X}, Y: {Y} 
                            ---------------------------
                            '{descrizioneFormattata}'
                            ---------------------------
                    """;
        }
    }
}
