using _04_Utility;

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
            return  $@"
            Pagina {base.Id}
                    {Luogo}
                    Data: {DataScrittura:dddd,dd/MMMM/yy}
                    Cooordinate: X: {X}, Y: {Y} 
                    ---------------------------
                    '{Descrizione}'
                    ---------------------------";
        }

    }
}
