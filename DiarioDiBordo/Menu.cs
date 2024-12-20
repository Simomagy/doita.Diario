using _04_Utility;

namespace DiarioDiBordo
{
    /// <summary>
    /// Implementa il menu utente per la gestione del diario
    /// </summary>
    public class Menu : Entity
    {
        /// <summary>
        /// Costruttore vuoto per l'istanza di <see cref="Menu"/>
        /// </summary>
        /// <param name="choice">
        /// Scelta dell'utente per l'apertura del diario
        /// </param>
        public void MenuPrincipale(string choice = "s")
        {

            switch(choice.ToLower())
            {
                case "n":
                    Console.WriteLine("Il diario ti guarda con disprezzo e ti chiede di andartene.");
                    Environment.Exit(0);
                    break;
                case "s":
                    Console.WriteLine("Le pagine si aprono con uno sbuffo di polvere e malinconia...");
                    Console.WriteLine("Cosa vuoi fare?");
                    Console.WriteLine(
                        "1. Leggi il diario \n2. Scrivi una nuova pagina \n3. Correggi una pagina \n4. Strappa una pagina\n\n0. Esci");
                    choice = Console.ReadLine()?.ToLower() ?? string.Empty;
                    switch(choice)
                    {
                        case "1":
                            LeggiDiario();
                            break;
                        case "2":
                            ScriviPagina();
                            break;
                        case "3":
                            ModificaPagina();
                            break;
                        case "4":
                            StrappaPagina();
                            break;
                        case "mi sono rotto i coglioni":
                            Console.WriteLine("Il diario esplode");
                            break;
                        case "0":
                            Console.WriteLine("Il diario si chiude con un tonfo");
                            break;
                        default:
                            Console.WriteLine("Scelta non valida");
                            MenuPrincipale();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Metodo contenente il menu per la lettura del diario <br/>
        /// Richiama le seguenti funzioni di <see cref="DAOPagina" />:
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="DAOPagina.GetRecords" /></term>
        /// <description>Restituisce una lista di pagine</description>
        /// </item>
        /// <item>
        /// <term><see cref="DAOPagina.RicercaPerTempo" /></term>
        /// <description>Restituisce una lista di pagine in un intervallo di tempo</description>
        /// </item>
        /// <item>
        /// <term><see cref="DAOPagina.RicercaPerDescrizione" /></term>
        /// <description>Restituisce una lista di pagine in base alla descrizione</description>
        /// </item>
        /// <item>
        /// <term><see cref="DAOPagina.RicercaPerLuogo" /></term>
        /// <description>Restituisce una lista di pagine in base al luogo</description>
        /// </item>
        /// </list>
        /// </summary>
        private static void LeggiDiario()
        {
            while (true)
            {
                Console.WriteLine("""
                                  Come lo vuoi leggere?
                                    1. Mi seggo e lo sfoglio tutto
                                    2. Cerco un periodo che mi interessa
                                    3. Sono interessato a...
                                    4. Leggo tutte le entrate scritte in un posto preciso
                                  """);
                var choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        var pagine = DAOPagina.GetInstance().GetRecords();
                        // Converto la lista di entity in una lista di pagine
                        List<Pagina> pagineDiario = [];
                        if (pagine != null)
                        {
                            foreach (var e in pagine)
                            {
                                pagineDiario.Add((Pagina)e);
                            }
                        }
                        // Ordino le pagine per data
                        var pagineOrdinate = pagineDiario.OrderBy(p => p.DataScrittura).ToList();
                        foreach (var pagina in pagineOrdinate)
                            Console.WriteLine(pagina.ToString());
                        break;
                    case "2":
                        var dataInizialeAnno = 0;
                        var dataInizialeMese = 0;
                        var dataInizialeGiorno = 0;

                        var dataFinaleAnno = 0;
                        var dataFinaleMese = 0;
                        var dataFinaleGiorno = 0;
                        try
                        {
                            Console.WriteLine("Inserisci la data iniziale");
                            Console.WriteLine("Anno:");
                            dataInizialeAnno = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.WriteLine("Mese:");
                            dataInizialeMese = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.WriteLine("Giorno:");
                            dataInizialeGiorno = int.Parse(Console.ReadLine() ?? string.Empty);

                            Console.WriteLine("Inserisci la data finale");
                            Console.WriteLine("Anno:");
                            dataFinaleAnno = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.WriteLine("Mese:");
                            dataFinaleMese = int.Parse(Console.ReadLine() ?? string.Empty);
                            Console.WriteLine("Giorno:");
                            dataFinaleGiorno = int.Parse(Console.ReadLine() ?? string.Empty);
                        } catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Errore nella formattazione della data");
                            LeggiDiario();
                        } finally
                        {
                            var dataIniziale = new DateTime(dataInizialeAnno, dataInizialeMese, dataInizialeGiorno);
                            var dataFinale = new DateTime(dataFinaleAnno, dataFinaleMese, dataFinaleGiorno);
                            List<Pagina> pagineIntervallo = DAOPagina.GetInstance().RicercaPerTempo(dataIniziale, dataFinale);
                            foreach (var pagina in pagineIntervallo)
                            {
                                Console.WriteLine(pagina.ToString());
                            }
                        }

                        break;
                    case "3":
                        Console.WriteLine("A cosa sei interessato?");
                        var descrizione = Console.ReadLine()?.ToLower();
                        if (descrizione != null)
                        {
                            var risultato = DAOPagina.GetInstance().RicercaPerDescrizione(descrizione);
                            if (risultato.Count == 0)
                            {
                                Console.WriteLine("Nessun risultato trovato");
                                LeggiDiario();
                            }
                            var risultatoOrdinato = risultato.OrderBy(p => p.DataScrittura).ToList();
                            foreach (var p in risultatoOrdinato)
                            {
                                Console.WriteLine(p.ToString());
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("E' davvero stato in...?");
                        var luogo = Console.ReadLine();
                        if (luogo != null)
                        {
                            var listaPagine = DAOPagina.GetInstance().RicercaPerLuogo(luogo).OrderBy(p => p.DataScrittura).ToList();
                            if (listaPagine.Count == 0)
                            {
                                Console.WriteLine("Qui non c'è mai stato nessuno");
                                LeggiDiario();
                            }
                            foreach (var pagina in listaPagine)
                            {
                                Console.WriteLine(pagina.ToString());
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        continue;
                }
                break;
            }
        }

        /// <summary>
        /// Metodo per la scrittura di una nuova pagina nel diario <br/>
        /// Richiama il metodo <see cref="DAOPagina.CreateRecord" />
        /// </summary>
        private void ScriviPagina()
        {
            var pagina = new Pagina();
            Console.WriteLine("Scrivi la pagina");
            Console.WriteLine("Anno:");
            var anno = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Mese:");
            var mese = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Giorno:");
            var giorno = int.Parse(Console.ReadLine() ?? string.Empty);
            pagina.DataScrittura = new DateTime(anno, mese, giorno);
            Console.WriteLine("Inserisci le coordinate del luogo");
            Console.WriteLine("X:");
            pagina.X = float.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Y:");
            pagina.Y = float.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Inserisci il luogo");
            pagina.Luogo = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Inserisci la descrizione");
            pagina.Descrizione = Console.ReadLine() ?? string.Empty;
            DAOPagina.GetInstance().CreateRecord(pagina);
            MenuPrincipale();
        }

        /// <summary>
        /// Metodo per la modifica di una pagina esistente nel diario <br/>
        /// Richiama il metodo <see cref="DAOPagina.UpdateRecord" />
        /// </summary>
        private void ModificaPagina()
        {
            Console.WriteLine("Inserisci il numero della pagina che vuoi modificare");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);
            var entity = DAOPagina.GetInstance().FindRecord(id);
            if (entity == null)
            {
                Console.WriteLine("Pagina non trovata");
                ModificaPagina();
            }
            var pagina = (Pagina?)entity;
            if (pagina == null)
            {
                Console.WriteLine("Pagina non trovata");
                ModificaPagina();
            }
            Console.WriteLine(pagina?.ToString());
            Console.WriteLine("Cosa vuoi modificare?");
            Console.WriteLine("1. Data di scrittura \n2. Coordinate \n3. Luogo \n4. Descrizione");
            var choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    Console.WriteLine("Inserisci la nuova data di scrittura");
                    Console.WriteLine("Anno:");
                    var anno = int.Parse(Console.ReadLine() ?? string.Empty);
                    Console.WriteLine("Mese:");
                    var mese = int.Parse(Console.ReadLine() ?? string.Empty);
                    Console.WriteLine("Giorno:");
                    var giorno = int.Parse(Console.ReadLine() ?? string.Empty);
                    if (pagina != null) pagina.DataScrittura = new DateTime(anno, mese, giorno);
                    break;
                case "2":
                    Console.WriteLine("Inserisci le nuove coordinate");
                    Console.WriteLine("X:");
                    if (pagina != null)
                    {
                        pagina.X = float.Parse(Console.ReadLine() ?? string.Empty);
                        Console.WriteLine("Y:");
                        pagina.Y = float.Parse(Console.ReadLine() ?? string.Empty);
                    }
                    else
                    {
                        Console.WriteLine("Pagina non trovata");
                        ModificaPagina();
                    }
                    break;
                case "3":
                    Console.WriteLine("Inserisci il nuovo luogo");
                    if (pagina != null)
                        pagina.Luogo = Console.ReadLine() ?? string.Empty;
                    else
                    {
                        Console.WriteLine("Pagina non trovata");
                        ModificaPagina();
                    }
                    break;
                case "4":
                    Console.WriteLine("Inserisci la nuova descrizione");
                    if (pagina != null)
                        pagina.Descrizione = Console.ReadLine() ?? string.Empty;
                    else
                    {
                        Console.WriteLine("Pagina non trovata");
                        ModificaPagina();
                    }
                    break;
                default:
                    Console.WriteLine("Scelta non valida");
                    ModificaPagina();
                    break;
            }
            if (pagina != null)
                DAOPagina.GetInstance().UpdateRecord(pagina);
            else ModificaPagina();
            MenuPrincipale();
        }

        /// <summary>
        /// Metodo per la rimozione di una pagina dal diario <br/>
        /// Richiama il metodo <see cref="DAOPagina.DeleteRecord" />
        /// </summary>
        private void StrappaPagina()
        {
            Console.WriteLine("Inserisci il numero della pagina che vuoi strappare");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);
            DAOPagina.GetInstance().DeleteRecord(id);
            MenuPrincipale();
        }
    }
}
