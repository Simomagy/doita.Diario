
using _04_Utility;

namespace DiarioDiBordo
{
    // ReSharper disable once InconsistentNaming
    internal class DAOPagina : IDAO, IDAOPagina
    {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
        /// <summary>
        /// Inizializzazione dell'istanza di <see cref="DAOPagina"/> come <see langword="null"/>
        /// </summary>
        private static DAOPagina? _instance;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
        /// <summary>
        /// Inizializzazione dell'istanza di <see cref="IDatabase"/> come <see cref="Database"/>
        /// </summary>
        private readonly IDatabase _db;
        /// <summary>
        /// Nome della tabella nel database utilizzata nelle funzioni DAO
        /// </summary>
        private const string TableName = "Diario";
        /// <summary>
        /// Costruttore privato per l'istanza di <see cref="DAOPagina"/>
        /// Inizializza un nuovo oggetto <see cref="Database"/> con i parametri:
        /// <list type="number">
        /// <item>
        /// <term>Nome del database</term>
        /// <description>tipo <see langword="string"/></description>
        /// </item>
        /// <item>
        /// <term>Nome del server</term>
        /// <description>tipo <see langword="string"/></description>
        /// </item>
        /// </list>
        /// </summary>
        private DAOPagina()
        {
            _db = new Database("DiarioDiBordo", "MSSTU");
        }

        /// <summary>
        /// Crea un record nel database con i dati della pagina contenuti nell'oggetto <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">
        /// Oggetto <see cref="Entity"/> contenente i dati della pagina da inserire nel database
        /// </param>
        /// <returns>
        /// <see langword="true"/> se l'inserimento è andato a buon fine, <see langword="false"/> altrimenti
        /// </returns>
        public bool CreateRecord(Entity entity)
        {
            var dataScrittura = ((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd");
            var x = ((Pagina)entity).X;
            var y = ((Pagina)entity).Y;
            var luogo = ((Pagina)entity).Luogo.Replace("'", "''");
            var descrizione = ((Pagina)entity).Descrizione.Replace("'", "''");

            return _db.UpdateDb($"INSERT INTO {TableName} (DataScrittura,X,Y,Luogo,Descrizione)" +
                $"VALUES" +
                $"('{dataScrittura}'," +
                $"  {x}," +
                $"  {y}," +
                $" '{luogo}'," +
                $" '{descrizione}');");
        }

        /// <summary>
        /// Cancella un record dal database in base all'ID
        /// </summary>
        /// <remarks>
        /// Utilizza il metodo <see cref="IDatabase.UpdateDb(string)"/> per cancellare il record dal database
        /// </remarks>
        /// <param name="recordId">
        /// ID del record da cancellare
        /// </param>
        /// <returns>
        /// <see langword="true"/> se la cancellazione è andata a buon fine, <see langword="false"/> altrimenti
        /// </returns>
        public bool DeleteRecord(int recordId)
        {
            return _db.UpdateDb($"DELETE FROM {TableName} WHERE Id = {recordId};");
        }

        /// <summary>
        /// Cerca un record nel database in base all'ID
        /// </summary>
        /// <remarks>
        /// Utilizza il metodo <see cref="IDatabase.ReadOneDb(string)"/> per leggere il record dal database
        /// </remarks>
        /// <param name="recordId">
        /// ID del record da cercare
        /// </param>
        /// <returns>
        /// L'oggetto <see cref="Entity"/> contenente i dati del record cercato, <see langword="null"/> se non è stato trovato
        /// </returns>
        public Entity? FindRecord(int recordId)
        {
            var riga = _db.ReadOneDb($"SELECT * FROM {TableName} WHERE Id = {recordId};");
            if (riga == null)
                return null;
            Entity e = new Pagina();
            e.TypeSort(riga);
            return e;
        }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        /// <summary>
        /// Restituisce tutti i record presenti nel database sotto forma di <see cref="List{T}"/> di oggetti <see cref="Entity"/>
        /// </summary>
        /// <remarks>
        /// Utilizza il metodo <see cref="IDatabase.ReadDb(string)"/> per leggere i record dal database
        /// </remarks>
        /// <returns>
        /// <see cref="List{T}"/> di oggetti <see cref="Entity"/> contenenti i dati dei record presenti nel database, <see langword="null"/> se non ci sono record
        /// </returns>
        public List<Entity>? GetRecords()
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            List<Entity> records = [];
            var righe = _db.ReadDb($"SELECT * FROM {TableName};");
            if (righe == null)
                return null;
            foreach (var riga in righe)
            {
                Entity e = new Pagina();
                e.TypeSort(riga);
                records.Add(e);
            }
            return records;
        }

        /// <summary>
        /// Aggiorna un record nel database con i dati della pagina contenuti nell'oggetto <see cref="Entity"/>
        /// </summary>
        /// <remarks>
        /// Utilizza il metodo <see cref="IDatabase.UpdateDb(string)"/> per aggiornare il record nel database
        /// </remarks>
        /// <param name="entity">
        /// Oggetto <see cref="Entity"/> contenente i dati della pagina da aggiornare nel database
        /// </param>
        /// <returns>
        /// <see langword="true"/> se l'aggiornamento è andato a buon fine, <see langword="false"/> altrimenti
        /// </returns>
        public bool UpdateRecord(Entity entity)
        {
            var dataScrittura = ((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd");
            var x = ((Pagina)entity).X;
            var y = ((Pagina)entity).Y;
            var luogo = ((Pagina)entity).Luogo.Replace("'", "''");
            var descrizione = ((Pagina)entity).Descrizione.Replace("'", "''");

            return _db.UpdateDb($"UPDATE {TableName} SET " +
                $"DataScrittura = '{dataScrittura}'," +
                $"X = {x}," +
                $"Y = {y}," +
                $"Luogo = '{luogo}'," +
                $"Descrizione = '{descrizione}'" +
                $"WHERE Id = {entity.Id};");
        }

        /// <summary>
        /// Cerca le pagine nel diario in base all'intervallo di tempo specificato
        /// </summary>
        /// <remarks>
        /// Utilizza il metodo <see cref="GetRecords"/> per ottenere tutti i record presenti nel database
        /// </remarks>
        /// <param name="dataIniziale">
        /// Data iniziale dell'intervallo di tempo di tipo <see cref="DateTime"/>
        /// </param>
        /// <param name="dataFinale">
        /// Data finale dell'intervallo di tempo di tipo <see cref="DateTime"/>
        /// </param>
        /// <returns>
        /// <see cref="List{T}"/> di oggetti <see cref="Pagina"/> contenenti i dati delle pagine trovate, <see langword="null"/> se non ci sono pagine
        /// </returns>
        public List<Pagina> RicercaPerTempo(DateTime dataIniziale, DateTime dataFinale)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            if (pagine == null)
                return ris;
            foreach (var entity in pagine)
            {
                var p = (Pagina)entity;
                if (p.DataScrittura >= dataIniziale && p.DataScrittura <= dataFinale)
                    ris.Add(p);

            }
            return ris;
        }

        public List<Pagina> RicercaPerLuogo(string luogo)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            if (pagine == null)
                return ris;
            foreach (var entity in pagine)
            {
                var p = (Pagina)entity;
                if (p.Luogo == luogo)
                    ris.Add(p);

            }
            return ris;
        }

        public List<Pagina> RicercaPerDescrizione(string descrizione)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            if (pagine == null)
                return ris;
            foreach (var entity in pagine)
            {
                var p = (Pagina)entity;
                if (p.Descrizione.ToLower().Contains(descrizione))
                    ris.Add(p);

            }
            return ris;
        }
        public static DAOPagina GetInstance()
        {
            return _instance ?? new DAOPagina();
        }
    }
}
