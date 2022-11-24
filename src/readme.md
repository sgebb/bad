Hva gjør applikasjonen:
api for å lagre strings og tall i en database, og å lese dem ut
kan kun legge inn nye strings om dagen

singleton med state for å sjekke om man har lov til å skrive tall

lyst til å teste noe i controlleren, men får ikke mocket ut 

mangel på ioc, instansering av avhengigheter overalt (hard wired dependency)

global state - feks singleton med state som modifiseres og brukes

ekstern avhengighet som statisk klasse? singleton? cache?

vanskelig å mocke ut config
bruk av dbcontext direkte i controlleren - skriv en test på lagring av noe. betyr den at dbcontexten er godt testa? går det an å bruke den feil?
businesslogikk i controller-laget

mye stuff i constructor?

service-locator - tvinger oss til å bruke samme dependency-injection opplegg

mer avansert:
validering på at noe kun er lov om dagen - validerer dette med DateTime.Now()

statisk klasse eller at man instanserer opp noe som gjør masse beregninger

koble til database, connectionstring i config

én testklasse per ting som skal testes - testene laget men bare Assert.Pass og ferdig. Description sier hva som skal testes
implementasjonen kan ha kommentar som sier noe om hvorfor det er vanskelig

avhengig av klassen (ikke interface ) - får ikke mocket det ut med mindre de er virtual. 

test må håndtere både entity og dto