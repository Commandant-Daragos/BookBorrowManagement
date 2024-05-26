Úloha: Implementácia API endpointov pre správu kníh v knižnici

Vašou úlohou je vytvoriť API endpointy pre správu vypožičaných kníh.

Požiadavky:

1. Vytvorte ASP.NET Core aplikáciu pre správu vypožičaných kníh.
2. Použite databázové modely pre používateľov a knihy a dátovú vrstvu, ktorá umožní komunikáciu s databázou (napr. použitie Entity Framework Core).
3. Endpointy by mali podporovať:
    -Vytvorenie novej knihy
    -Získanie detailov existujúcej knihy podľa ID
    -Aktualizáciu existujúcej knihy
    -Odstránenie knihy
    -Vytvorenie novej zápožičky
    -Potvrdenie o vrátení vypožičanej knihy
4. Zabezpečte validáciu vstupných údajov.

Bonusové úlohy:

Pokrytie kódu testami (unit testy, integračné testy).
Implementuje funkcionalitu automatickeho odoslania pripomienky používateľovi deň pred uplynutím termínu na vrátenie knihy. (odoslanie emailu nafejkujte)
