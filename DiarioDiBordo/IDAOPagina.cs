namespace DiarioDiBordo
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Implementa i metodi per la ricerca di pagine nel diario
    /// </summary>
    internal interface IDAOPagina
    {
        public List<Pagina> RicercaPerTempo(DateTime dataIniziale, DateTime dataFinale);
        public List<Pagina> RicercaPerLuogo(string luogo);
        public List<Pagina> RicercaPerDescrizione(string descrizione);
    }
}
