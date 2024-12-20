# Dovete gestire un diario di bordo.

#### Ogni 'pagina' del diario avrà la data del giorno, le coordinate X e Y di dove ci si trova, il nome del luogo in cui ci si trova e una descrizione di cosa è successo.

---
Scrivere un programma che legga le pagine dal DB e permetta tramite menu su console, di aggiungere delle voci,

- Modificarle, eliminarle oppure leggere l'intero diario.

Voglio inoltre che siano possibili alcune tipologia di ricerca che parte dall'utente, ad esempio:

- Ricerca in base a un periodo di tempo (Esempio: tra 01-01-2019 e 01-01-2020)
- Ricerca in base al luogo
- Ricerca in base a una parte della descrizione

---
Tabella diario:

- ID (INT PRIMARY KEY IDENTITY(1,1))
- Data (DATE)
- X (FLOAT)
- Y (FLOAT)
- Luogo (VARCHAR 255)
- Descrizione (TEXT)

Metodi CRUD
Metodo RicercaPerTempo
Metodo RicercaPerLuogo
Metodo RicercaPerDescrizione
Classe Modello
