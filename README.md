# pizzeria-crud-refactoring
Esercitazione per prendere prativa con MVC + entity framework con relazioni !:N e N:N
Aggiunta di login e la registrazione

Bloccare l’accesso A PizzaController :
        -utenti con role USER possano accedere solo alla pagina con l’elenco delle pizze e ai dettagli della singola pizza. 
        -utenti con role ADMIN devono poter creare, modificare e cancellare le pizze.
BONUS:
Provate a fare in modo che un utente quando si registra per la prima volta gli venga associato il ruolo di default come "USER"
