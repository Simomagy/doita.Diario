namespace DiarioDiBordo
{
    internal interface IDAOPagina
    {
        public List<Pagina> RicercaPerTempo(DateTime dataIniziale, DateTime dataFinale);
        public List<Pagina> RicercaPerLuogo(string luogo);
        public List<Pagina> RicercaPerDescrizione(string descrizione);
    }
}
