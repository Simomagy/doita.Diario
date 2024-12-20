using _04_Utility;
using DiarioDiBordo;

// Creaiamo il menu utente
// Scelta 1: Scrivi una nuova pagina
// Scelta 2: Correggi una pagina
// Scelta 3: Strappa una pagina
// Scelta 4: Leggi il diario => 1. Leggi per luogo, 2. Leggi per intervallo di date, 3. Leggi per descrizione

Console.WriteLine("Trovi un diario polveroso... vuoi aprirlo? (s/n)");

static void MenuPrincipale()
{
    var choice = Console.ReadLine() ?? string.Empty;
    switch(choice.ToLower())
    {
        case "n":
            Console.WriteLine("Il diario ti guarda con disprezzo e ti chiede di andartene.");
            Environment.Exit(0);
            break;
        case "s":
            Console.WriteLine("Le pagine si aprono con uno sbuffo di polvere e maliconia...");
            Console.WriteLine("Cosa vuoi fare?");
            Console.WriteLine("1. Leggi il diario \n 2. Scrivi una nuova pagina \n 3. Correggi una pagina \n 4. Strappa una pagina");
            choice = Console.ReadLine();
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
                default:
                    Console.WriteLine("Scelta non valida");
                    MenuPrincipale();
                    break;
            }
            break;
    }
}

static void LeggiDiario()
{
    Console.WriteLine(@"Come lo vuoi leggere?
                        1. Mi seggo e lo sfoglio tutto
                        2. Cerco un periodo che mi interessa
                        3. Sono interessato a...
                        4. Leggo tutte le entrate scritte in un posto preciso");
    var choice = Console.ReadLine();
    switch(choice)
    {
        case "1":
            var pagine = DAOPagina.GetInstance().GetRecords();
            // Converto la lista di entity in una lista di pagine
            List<Pagina> pagineDiario = [];
            foreach(Entity e in pagine)
            {
                pagineDiario.Add((Pagina) e);
            }
            // Ordino le pagine per data
            List<Pagina> pagineOrdinate = pagineDiario.OrderBy(p => p.DataScrittura).ToList();
            foreach(var pagina in pagineOrdinate)
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
                dataInizialeAnno = int.Parse(Console.ReadLine());
                Console.WriteLine("Mese:");
                dataInizialeMese = int.Parse(Console.ReadLine());
                Console.WriteLine("Giorno:");
                dataInizialeGiorno = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Inserisci la data finale");
                Console.WriteLine("Anno:");
                dataFinaleAnno = int.Parse(Console.ReadLine());
                Console.WriteLine("Mese:");
                dataFinaleMese = int.Parse(Console.ReadLine());
                Console.WriteLine("Giorno:");
                dataFinaleGiorno = int.Parse(Console.ReadLine());
            } catch(Exception e)
            {
                Console.WriteLine("Errore nella formattazione della data");
                LeggiDiario();
            } finally
            {
                DateTime dataIniziale = new DateTime(dataInizialeAnno, dataInizialeMese, dataInizialeGiorno);
                DateTime dataFinale = new DateTime(dataFinaleAnno, dataFinaleMese, dataFinaleGiorno);
                List<Pagina> pagineIntervallo = DAOPagina.GetInstance().RicercaPerTempo(dataIniziale, dataFinale);
                foreach (var pagina in pagineIntervallo)
                {
                   Console.WriteLine(pagina.ToString()); 
                }
            }
            
            break;
        case "3":
            Console.WriteLine("A cosa sei interessato?");
            var descrizione = Console.ReadLine().ToLower();
            List<Pagina> risultato = DAOPagina.GetInstance().RicercaPerDescrizione(descrizione);
            var risultatoOrdinato = risultato.OrderBy(p => p.DataScrittura).ToList();
            foreach(var p in risultatoOrdinato)
            {
                Console.WriteLine(p.ToString());
            }
            break;
        case "4":
            Console.WriteLine("E' davvero stato in...?");
            var luogo = Console.ReadLine();
            var listaPagine = DAOPagina.GetInstance().RicercaPerLuogo(luogo).OrderBy(p => p.DataScrittura).ToList();
            foreach (var pagina in listaPagine)
            {
                Console.WriteLine(pagina.ToString());
            }
            break;
        default:
            Console.WriteLine("Scelta non valida");
            LeggiDiario();
            break;
    }
}

static void ScriviPagina()
{
    var pagina = new Pagina();
    Console.WriteLine("Scrivi la pagina");
    Console.WriteLine("Anno:");
    var anno = int.Parse(Console.ReadLine());
    Console.WriteLine("Mese:");
    var mese = int.Parse(Console.ReadLine());
    Console.WriteLine("Giorno:");
    var giorno = int.Parse(Console.ReadLine());
    pagina.DataScrittura = new DateTime(anno, mese, giorno);
    Console.WriteLine("Inserisci le coordinate del luogo");
    Console.WriteLine("X:");
    pagina.X = float.Parse(Console.ReadLine());
    Console.WriteLine("Y:");
    pagina.Y = float.Parse(Console.ReadLine());
    Console.WriteLine("Inserisci il luogo");
    pagina.Luogo = Console.ReadLine();
    Console.WriteLine("Inserisci la descrizione");
    pagina.Descrizione = Console.ReadLine();
    DAOPagina.GetInstance().CreateRecord(pagina);
    MenuPrincipale();
}

static void ModificaPagina()
{
    Console.WriteLine("Inserisci il numero della pagina che vuoi modificare");
    var id = int.Parse(Console.ReadLine());
    var entity = DAOPagina.GetInstance().FindRecord(id);
    var pagina = (Pagina) entity;
    Console.WriteLine(pagina.ToString());
    Console.WriteLine("Cosa vuoi modificare?");
    Console.WriteLine("1. Data di scrittura \n 2. Coordinate \n 3. Luogo \n 4. Descrizione");
    var choice = Console.ReadLine();
    switch(choice)
    {
        case "1":
            Console.WriteLine("Inserisci la nuova data di scrittura");
            Console.WriteLine("Anno:");
            var anno = int.Parse(Console.ReadLine());
            Console.WriteLine("Mese:");
            var mese = int.Parse(Console.ReadLine());
            Console.WriteLine("Giorno:");
            var giorno = int.Parse(Console.ReadLine());
            pagina.DataScrittura = new DateTime(anno, mese, giorno);
            break;
        case "2":
            Console.WriteLine("Inserisci le nuove coordinate");
            Console.WriteLine("X:");
            pagina.X = float.Parse(Console.ReadLine());
            Console.WriteLine("Y:");
            pagina.Y = float.Parse(Console.ReadLine());
            break;
        case "3":
            Console.WriteLine("Inserisci il nuovo luogo");
            pagina.Luogo = Console.ReadLine();
            break;
        case "4":
            Console.WriteLine("Inserisci la nuova descrizione");
            pagina.Descrizione = Console.ReadLine();
            break;
        default:
            Console.WriteLine("Scelta non valida");
            ModificaPagina();
            break;
    }
    DAOPagina.GetInstance().UpdateRecord(pagina);
    MenuPrincipale();
}

static void StrappaPagina()
{
    Console.WriteLine("Inserisci il numero della pagina che vuoi strappare");
    var id = int.Parse(Console.ReadLine());
    DAOPagina.GetInstance().DeleteRecord(id);
    MenuPrincipale();
}