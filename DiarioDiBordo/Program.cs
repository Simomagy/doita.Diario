using DiarioDiBordo;

Console.WriteLine("Trovi un diario polveroso... vuoi aprirlo? (s/n)");
var choice = Console.ReadLine() ?? string.Empty;
var menu = new Menu();
menu.MenuPrincipale(choice);
