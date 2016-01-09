# DentalClinic

Zasady logiki biznesowej przy dodawaniu nowego urlopu lub zwolnienia:

1. Zablokowana możliwosc dodawania urlopu/zwolnienia dla użytkownika który w podanym terminie przebywa już na urlopie/zwolnieniu.

2. Zablokowana możliwosc dodawania urlopu/zwolnienia dla użytkownika który w podanym terminie przebywa na zastepstwie.

3. Zablokowana możliwosc dodawania urlopu/zwolnienia w przypadku gdy aplikacja nie znajdzie zastepstwa.

4. Zastepca musi byc uzytkownik o takiej samej roli jak uzytkownik dla ktorego dodawany jest wpis.

5. Zastepca musi byc wolny w terminie ktory obejmuje nowy wpis (nie moze byc na swoim zwolnieniu/urlopie ani na zastepstwie).
