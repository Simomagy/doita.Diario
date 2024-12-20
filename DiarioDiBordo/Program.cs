using _04_Utility;
using DiarioDiBordo;

// Creaiamo il menu utente
// Scelta 1: Scrivi una nuova pagina
// Scelta 2: Correggi una pagina
// Scelta 3: Strappa una pagina
// Scelta 4: Leggi il diario => 1. Leggi per luogo, 2. Leggi per intervallo di date, 3. Leggi per descrizione

Console.WriteLine("Trovi un diario polveroso... vuoi aprirlo? (s/n)");
var choice = Console.ReadLine() ?? string.Empty;
var menu = new Menu();
menu.MenuPrincipale(choice);
