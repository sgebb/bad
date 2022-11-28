Hva gjør applikasjonen:
api for å lagre strings og tall i en database, og å lese dem ut
kan kun legge inn nye strings om dagen

takeaway general
unit tests should be specific so that it's easy to see what is broken
static can't be mocked
classes need interfaces to be mocked
things need to be injected to be mocked
integration-tests can still be valuable

takeaways numbers

eveyrything in one class, hard to write isolated tests (if something fails then I'd like to know what it is)
instantiating dbcontext makes it hard to mock out the database
everything in the controller means test-code has to know the framework

takeaways strings

sometimes you have to wrap stuff like Random or DateTimeOffset.Now so that it can be controlled
Injecting BadDomain (and not an interface) means you can't mock it (without making the methods virtual) (is that worse than adding an interface?)
I want to test my business-logic without making a complex claims-principal object

further reading
law of demeter
avoid singletons or globals with state




ha egne repo/brancher per vanskelighetsgrad
intro først: litt om test, og hvordan skrive testbar kode
etter X min: går gjennom numbers, og introdusere strings
ha en lettlest eksempeltest som funker
gå gjennom koden kjapt
hva er mocking og hva kan ikke mockes
hver test tester bare en ting