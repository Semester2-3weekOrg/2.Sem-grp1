# The movies
The Movies er en biografkæde beliggende i det vestlige Jylland med fokus på dokumentarer og nichefilm. På nuværende tidspunkt har The Movies fire biografer, en i henholdsvis Hjerm, Videbæk, Thorsminde og Ræhr, og som det er lige nu benytter The Movies en lettere forældet fremgangsmåde til at have et overblik over film, biografer og solgte billetter.

# Team Struktur


# High Level Design
I har nu lavet et plan og skal i gang med første sprint og udarbejdelse af The Movies analyseartefakter.

**Krav til HLD-fasen:**
 - Beskriv systemet med de nødvendige artefakter inden for HLD.
 - Lav dem kun på det skridt, du er i gang med

**Low Level Design:**
- Lav de nødvendige artefakter inden for LLD.

**Krav til designfasen:**
- Beskriv en lagdelt arkitektur via et pakkediagram (kun med lag, klassenavne og afhængigheder), hvor der er krav om, at MVVM-arkitekturmønstret skal anvendes i en WPF-applikation.
- Udarbejd en DCD med de klasser (med klassenavne, attributter og operationer), som relevante for use casen. Undgå at have alle domæneklasser med, men overhold et krav om, at DCD’et skal indeholde mindst én View-klasse, mindst én ViewModel-klasse, mindst én Repository-klasse og mindst én Model-klasse, så alle typer er repræsenteret.
- Udarbejd en SD for en udvalgt operationskontrakt, hvor kravet er, at SD’et tydeligt skal vise, hvordan den i SD’et beskrevne operation ændrer systemets tilstand fra kontraktens præ-betingelse (en: precondition) til dets post-betingelse (en: postcondition).
- Planlæg persistens i CSV-tekstfiler med relevant syntaks. Det skal være en Repository-klasse, som har ansvaret for at persistere. Sørg for enten, at I kan læse data fra en CSV-fil ind i it-systemet eller skrive data til en CSV-fil; gerne begge dele, hvis der er tid. Husk at holde det simpelt og vælg det letteste først. Benyt gerne data fra den udleverede CSV-fil.
- Design en WPF-brugergrænseflade med ét hovedvindue samt en dialogboks til indtastning af data; igen hold det simpelt via XAML. Der må gerne bruges databinding mellem View-laget og ViewModel-laget.

# Programmering
Brugergrænsefladen.

Grænsefladen bør implementeres som en WPF-applikation. 
Anvend Model-View-ViewModel (MVVM). 
MVVM er et designmønster, der er særligt velegnet til WPF-applikationer. 
Det adskiller logikken fra brugergrænsefladen og gør det nemmere at teste, vedligeholde og udvide din applikation.

**Minimumskrav til et MVVM-projekt:**

- Model:
  - Simpel klasse, der repræsenterer data.
  - Inkluderer properties for de data, der skal vises i view.
  - Implementerer INotifyPropertyChanged for at notificere view, når data ændres.
- View:
  - Defineres i XAML.
  - Binder til properties i ViewModel via data binding.
  - Indeholder ikke kompleks logik.
- ViewModel:
  - Fungerer som mellemmand mellem Model og View.
  - Eksponerer properties, der kan bindes til View.
  - Implementerer kommandoer (ICommand), der kan udløses fra View.
  - Indeholder al applikationslogik.

## Scenarie 1
Ejeren af The Movies, Jens Peter Hansen (Movie Jens Peter i folkemunde), modtager løbende tilbud fra forskellige filmselskaber om film, der kan vises i hans biografer. Når han beslutter sig for at tage en film på plakaten, noterer han den ned i et Word dokument, hvor han kopierer følgende oplysninger fra mailen:

Titel
varighed
filmgenre

### Det nye system
Start med at lave et system, hvor Jens Peter kan registrere sine film

## Scenarie 2
Hver måned inden den 23. går ejeren Jens Peter Hansen (Movie Jens Peter i folkemunde) ind og laver et Excel-ark til hver biograf med de forskellige film, spilletider, filmgenre, varighed på filmene, filminstruktør, premieredato og biografsal.
Excel-arket gælder for den kommende måned. Når Jens Peter udregner, hvor lang tid en forestilling tager, tillægger han 15 minutter til reklamer og 15 minutter til rengøring.

### Det nye system
Udbyg jeres system, så Jens Peter kan lave programmet for biograferne i det nye system.
Husk at I stadig skal igennem alle 4 fokusområder før I er færdige med dette scenarie.

## Scenarie 3
Den 24. i hver måned, når de ansatte i biografen har modtaget Excel-arket, åbner telefonerne op for booking af tider til næste måned. Bookingen foregår ved, at en kunde ringer ind og får at vide, hvornår en film bliver fremvist, og vælger hvilken tid, han gerne vil reservere. Bookinger skrives ind i biografens røde ringbind med bookinger. Hver gang en kunde bestiller tid til en film, finder den ansatte booking-arket i ringbindet, hvor den angivne film, tidspunkt og biografsal er udfyldt. På det specifikke ark skriver den ansatte kundens billetantal, mail og telefonnummer i en række under hinanden. Hvis der endnu ikke er nogen bookinger på den angivne film, tager den ansatte et nyt ark, udfylder film og tidsplan, tjekker at det stemmer overens med tiderne i Jens Peters Excel-ark, indskriver kundens oplysninger og placerer arket det rigtige sted i ringbindet.

### Det nye system
Udbyg det nye system, så 
De ansatte kan registrere reservation af billetter
De ansatte ikke kan komme til at sælge for mange biletter til en forestilling
Husk at I stadig skal nedbryde så meget som muligt og at I skal igennem alle 4 fokusområder før I er færdige med dette scenarie.

# Kvalitetssikring
Det er nu tid til at kigge på kvaliteten af jeres system. Inden I beder andre om feedback, er det en god ide selv at kigge systemet efter i sømmene. Brug kriterier I har i jeres guidelines.
