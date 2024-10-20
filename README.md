# House Rental Project

## Prosjektoversikt
House Rental Project er et hobbyprosjekt utviklet for å vise mine ferdigheter innen fullstack-utvikling. Prosjektet bruker **ASP.NET Core** på back-end med **Entity Framework** for databasehåndtering og **Razor Pages** for front-end. Systemet gir brukere muligheten til å registrere seg, logge inn, og administrere utleieboliger. Dette prosjektet er laget ved hjelp av **Microsoft Visual Studio**.

## Funksjonalitet
- **Brukerregistrering og pålogging**: Implementert med **Microsoft Identity** for å håndtere autentisering, der brukere kan registrere seg og logge inn.
- **Boligadministrasjon**: Brukere kan legge til, endre, og slette boliger i systemet.
- **Rollebasert tilgang**: Forskjellige roller som brukere og administratorer med tilgang til ulike funksjoner.
- **Sikkerhet**: Bruker sikkerhetsfunksjoner som passordhashing og autentisering via **Microsoft Identity**.

## Teknologier brukt

### Back-end
- **ASP.NET Core**: For back-end utvikling og serverhåndtering.
- **Microsoft Identity**: For autentisering og brukerhåndtering.
- **Entity Framework**: For databaseoperasjoner (CRUD).
- **SQLite**: Brukt som database.
- **MVC-arkitektur**: Strukturen i prosjektet som organiserer modeller, visninger og kontroller.

### Front-end
- **JavaScript**: For dynamisk funksjonalitet i brukergrensesnittet.
- **Bootstrap**: For responsiv design og brukergrensesnitt.
- **CSS**: For tilpasset styling og forbedring av brukeropplevelse.
- **Razor Pages**: For dynamisk rendering av HTML, kombinert med C# på serveren.

## Installasjon og kjøring
1. Klon repositoriet:  
   `git clone https://github.com/HiwaAbdolahi/HouseRentalProject.git`
2. Åpne prosjektet i **Microsoft Visual Studio**.
3. Kjør migrasjoner for å opprette databasen:  
   `Update-Database`
4. Start prosjektet ved å bruke **Visual Studio's IIS Express** eller annen metode.

## Brukerhistorier
- **Bruker**: Registrere konto, logge inn, søke og administrere leieobjekter.
- **Administrator**: Håndtere brukere, oppdatere informasjon om boliger, og fjerne oppføringer.

## Fremtidige forbedringer
- Forbedre UI-design for en mer moderne og intuitiv brukeropplevelse ved hjelp av **JavaScript** og **CSS**.
- Legge til e-postbekreftelse ved registrering.
- Implementere API for mobilapplikasjon integrasjon.

## Om meg
Dette prosjektet er et hobbyprosjekt for å vise mine ferdigheter innen fullstack-utvikling, samt bruk av teknologier som **ASP.NET Core**, **Entity Framework**, **Microsoft Identity**, **JavaScript**, **Bootstrap**, og **CSS**. Prosjektet ble utviklet for å demonstrere min kompetanse innen webutvikling, autentisering, sikkerhet, databaseadministrasjon, og moderne front-end utvikling.
