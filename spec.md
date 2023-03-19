# CLI Chat App

## Motivace

K dokončení předmětu NPRG035 a NPRG038 na MFF UK je potřeba vytvořit 
zápočtový program.

## Cíl

Cílem je vytvořit dvě aplikace klienta a servr. Klient bude zajišťovat 
uživatelské rozhraní a komunikaci se servrem pomocí socketů a TCP 
připojení. Servr bude zpracovávat zprávy od klientů a buď vracet
výsledek příkazu nebo je přeposílat klientům ve stejným vlákně opět 
přes sockety a TCP připojení. Společně tyto aplikace tvoří jednoduchý 
komunikační kanál.

## Popis funkčnosti

 - připojení až 100 uživatelů na jeden servr
 - připojení na servr podmíněné heslem
 - vytváření veřejných vláken
 - vytváření privátních vláken
 - zobrazení dostupných vláken
 - přidávání klientů do privátního vlákna
 - odebírání klientů z privátního vlákna
 - odesílání textových zpráv do konkrétního vlákna
 - přijmání textových zpráv z konkrétního vlákna
 - logování servru

## Uživatelské rozhraní

Celá aplikace poběží v příkazové řádce. Veškerá konfigurace 
(např.: adresa servru) bude předaná při spuštění v podobě argumentů. 
Po spuštění bude uživatel moct psát zprávy pro odeslání nebo pomocí 
speciálního znaku příkazy pro aplikaci (např.: změnu/vytvoření vlákna).
