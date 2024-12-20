using _04_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDiBordo
{
    internal class DAOPagina : IDAO
    {
        private IDatabase db;
        private DAOPagina()
        {
            db = new Database("DiarioDiBordo", "DESKTOP-4QKQO49");
        }
        private static DAOPagina instance = null;
        public static DAOPagina GetInstance()
        {
            if (instance == null)
                instance = new DAOPagina();
            return instance;
        }
        public bool CreateRecord(Entity entity)
        {
            return db.UpdateDb($"INSERT INTO Diario (DataScrittura,X,Y,Luogo,Descrizione)" +
                $"VALUES" +
                $"('{((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd")}'," +
                $"  {((Pagina)entity).X}," +
                $"  {((Pagina)entity).Y}," +
                $" '{((Pagina)entity).Luogo.Replace("'", "''")}'," +
                $" '{((Pagina)entity).Descrizione.Replace("'", "''")});");
        }

        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM Diario WHERE Id = {Id};");
        }

        public Entity? FindRecord(int recordId)
        {
            var riga = db.ReadOneDb($"SELECT * FROM Diario WHERE Id = {Id};");
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
            var righe = db.ReadDb("SELECT * FROM Diario;");
            foreach(var riga in righe)
            {
                Entity e = new Pagina();
                e.TypeSort(riga);
                records.Add(e);
            }
            return records;
        }

        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Diario SET " +
                $"DataScrittura = '{((Pagina)entity).DataScrittura.ToString("yyyy-MM-dd")}'," +
                $"X = {((Pagina)entity).X}'," +
                $"Y = {((Pagina)entity).Y}'," +
                $"Luogo = '{((Pagina)entity).Luogo.Replace("'","''")}'," +
                $"Descrizione = '{((Pagina)entity).Descrizione.Replace("'","''")}'" +
                $"WHERE Id = {entity.Id};");
        }
    }
}
