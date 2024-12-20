using _04_Utility;

namespace DiarioDiBordo
{
    internal class DAOPagina : IDAO, IDAOPagina
    {
        private readonly string tableName = "Diario";
        private readonly IDatabase db;
        private DAOPagina()
        {
            db = new Database("DiarioDiBordo", "MSSTU");
        }
        private static DAOPagina? instance;
        public static DAOPagina GetInstance()
        {
            return instance ?? new DAOPagina();
        }
        public bool CreateRecord(Entity entity)
        {
            var dataScrittura = ((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd");
            var x = ((Pagina)entity).X;
            var y = ((Pagina)entity).Y;
            var luogo = ((Pagina)entity).Luogo.Replace("'", "''");
            var descrizione = ((Pagina)entity).Descrizione.Replace("'", "''");

            return db.UpdateDb($"INSERT INTO {tableName} (DataScrittura,X,Y,Luogo,Descrizione)" +
                            $"VALUES" +
                            $"('{dataScrittura}'," +
                            $"  {x}," +
                            $"  {y}," +
                            $" '{luogo}'," +
                            $" '{descrizione}');");
        }

        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM {tableName} WHERE Id = {recordId};");
        }

        public Entity? FindRecord(int recordId)
        {
            var riga = db.ReadOneDb($"SELECT * FROM {tableName} WHERE Id = {recordId};");
            if(riga != null)
            {
                Entity e = new Pagina();
                e.TypeSort(riga);
                return e;
            }
            else
                return null;
        }

        public List<Entity> GetRecords()
        {
            List<Entity> records = new();
            var righe = db.ReadDb($"SELECT * FROM {tableName};");
            if (righe != null)
            {
                foreach (var riga in righe)
                {
                    Entity e = new Pagina();
                    e.TypeSort(riga);
                    records.Add(e);
                }
                return records;
            }
            else
                return null;
        }

        public bool UpdateRecord(Entity entity)
        {
            var dataScrittura = ((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd");
            var x = ((Pagina)entity).X;
            var y = ((Pagina)entity).Y;
            var luogo = ((Pagina)entity).Luogo.Replace("'", "''");
            var descrizione = ((Pagina)entity).Descrizione.Replace("'", "''");

            return db.UpdateDb($"UPDATE {tableName} SET " +
                $"DataScrittura = '{dataScrittura}'," +
                $"X = {x}," +
                $"Y = {y}," +
                $"Luogo = '{luogo}'," +
                $"Descrizione = '{descrizione}'" +
                $"WHERE Id = {entity.Id};");
        }

        public List<Pagina> RicercaPerTempo(DateTime dataIniziale, DateTime dataFinale)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            foreach (Pagina p in pagine)
            {
                if(p.DataScrittura >= dataIniziale && p.DataScrittura <= dataFinale)
                    ris.Add(p);
                
            }
            return ris;
        }

        public List<Pagina> RicercaPerLuogo(string luogo)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            foreach (Pagina p in pagine)
            {
                if (p.Luogo == luogo )
                    ris.Add(p);

            }
            return ris;
        }

        public List<Pagina> RicercaPerDescrizione(string descrizione)
        {
            var ris = new List<Pagina>();

            var pagine = GetRecords();

            foreach (Pagina p in pagine)
            {
                if (p.Descrizione.ToLower().Contains(descrizione))
                    ris.Add(p);

            }
            return ris;
        }
    }
}
