# Assignment TODOs & Reflections

## TODO List

- [x] Implement `Card` and `GameStats` models ([Card.cs](Model/Card.cs))  
- [x] Implement `IGameStatsRepository` interface ([IGameStatsRepository.cs](Data/Interfaces/IGameStatsRepository.cs))  
- [x] Create `FileGameStatsRepository` with CSV read/write ([FileGameStatsRepository.cs](Data/FileRepo/FileGameStatsRepository.cs))  
- [ ] Setup `GameViewModel` and MVVM bindings ([GameViewModel.cs](ViewModel/GameViewModel.cs))  
- [ ] Implement game logic in ViewModel  
- [ ] Build WPF UI  
- [ ] Create High Scores view  
- [ ] Write unit tests ([Tests/](../YourProjectFolder.Tests))  
- [ ] Polish UI and fix bugs  

## Reflections & Notes

### Opgave 1

Et kort skal have et unikt ID, så man kan spore hvilke kort det matcher sammen, for at funktionaliteten kan fungere.
Symbol, er for at give noget grafisk til brugeren, så de kan se hvad der er hvad.
IsFlipped spores som en boolean værdi, for at kunne se om kortet er vendt om eller ej.
IsMatched spores som en boolean værdi, for at kunne se om kortet er matchet med et andet kort.
Det er ikke nødvendigt med flere properties, da det opfylder de grundlæggende principper for at kunne fungere.

### Hvad skal gemmes om spillet?

PlayerName er en god ide, for at kunne se, hvem der har lavet en HighsScore.
Moves skal håndtere ALLE træk, rigtige som forkerte, for at kunne se hvor mange træk der er brugt i alt.
GameTime det kan gemmes som TimeSpan for at give endnu en HS variabel.
CompletedAt gemmes i dato og klokkeslet format, men som shortstring.
Man kan også gemme præcision, men det er ikke nødvendigt for grundspillet, det er mere nice-to-have.

### Opgave 2


### Card and GameStats models  
Describe your approach, challenges, and solutions here.

### Repository Pattern  
Notes on implementing `IGameStatsRepository` and file storage.

### MVVM Setup  
Thoughts on how you structured your ViewModel and bindings.

### Unit Testing  
Summary of testing strategies, what worked and what didn’t.