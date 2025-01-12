## Live Demo  
[Visit the site](https://houserental.azurewebsites.net)  

# House Rental Project  

## Prosjektoversikt  
House Rental Project er et hobbyprosjekt utviklet for å vise mine ferdigheter innen fullstack-utvikling. Prosjektet bruker **ASP.NET Core** på back-end med **Entity Framework** for databasehåndtering og **Razor Pages** for front-end. Systemet gir brukere muligheten til å registrere seg, logge inn, og administrere utleieboliger. Prosjektet er deployert til **Azure Web App** og benytter **Kudu** for administrasjon og vedlikehold.  

## Funksjonalitet  
- **Brukerregistrering og pålogging**: Implementert med **Microsoft Identity** for autentisering.  
- **Boligadministrasjon**: Brukere kan legge til, endre og slette boliger i systemet.  
- **Rollebasert tilgang**: Forskjellige roller gir tilgang til ulike funksjoner.  
- **Sikkerhet**: Implementerer passordhashing og autentisering via **Microsoft Identity**.  
- **Automatisk deployment**: Ved hjelp av GitHub Actions for CI/CD.  

## Teknologier brukt  

### Back-end  
- **ASP.NET Core**: For back-end utvikling og serverhåndtering.  
- **Microsoft Identity**: For autentisering og brukerhåndtering.  
- **Entity Framework**: For databaseoperasjoner (CRUD).  
- **SQLite**: Brukt som database.  
- **MVC-arkitektur**: Strukturering av modeller, visninger og kontroller.  

### Front-end  
- **JavaScript**: For dynamisk funksjonalitet i brukergrensesnittet.  
- **Bootstrap**: For responsiv design og UI.  
- **CSS**: For tilpasset styling.  
- **Razor Pages**: For dynamisk rendering av HTML kombinert med C#.  

### Deployment  
- **Azure Web App**: For produksjonsmiljø.  
- **Kudu**: For filadministrasjon og logging.  
- **GitHub Actions**: For kontinuerlig integrasjon og deployment (CI/CD).  

## Installasjon og kjøring  

1. Klon repositoriet:  
   ```bash  
   git clone https://github.com/HiwaAbdolahi/HouseRentalProject.git  

2. Åpne prosjektet i **Microsoft Visual Studio**.
3. Kjør migrasjoner for å opprette databasen:  
   `Update-Database`
4. Start prosjektet ved å bruke **Visual Studio's IIS Express** eller annen metode.



## Deployment med GitHub Actions  

Jeg bruker GitHub Actions for å automatisere bygging og deployering til Azure Web App:  
- **Build-jobb**: Kompilerer prosjektet og publiserer artefakter.  
- **Deploy-jobb**: Logger inn på Azure og deployerer til Web App.  

### YAML-konfigurasjon for CI/CD  

```yaml
name: Build and deploy ASP.Net Core app to Azure Web App - HouseRental  

on:  
  push:  
    branches:  
      - master  
  workflow_dispatch:  

jobs:  
  build:  
    runs-on: windows-latest  
    steps:  
      - uses: actions/checkout@v4  
      - name: Set up .NET Core  
        uses: actions/setup-dotnet@v4  
        with:  
          dotnet-version: '6.0.x'  
      - name: Build with dotnet  
        run: dotnet build --configuration Release  
      - name: dotnet publish  
        run: dotnet publish -c Release -o "${{env.DOTNET_ROOT}}/myapp"  
      - name: Upload artifact for deployment job  
        uses: actions/upload-artifact@v4  
        with:  
          name: .net-app  
          path: ${{env.DOTNET_ROOT}}/myapp  

  deploy:  
    runs-on: windows-latest  
    needs: build  
    environment:  
      name: 'Production'  
    steps:  
      - name: Download artifact from build job  
        uses: actions/download-artifact@v4  
        with:  
          name: .net-app  
      - name: Login to Azure  
        uses: azure/login@v2  
        with:  
          client-id: ${{ secrets.AZUREAPPSERVICE_CLIENTID }}  
          tenant-id: ${{ secrets.AZUREAPPSERVICE_TENANTID }}  
          subscription-id: ${{ secrets.AZUREAPPSERVICE_SUBSCRIPTIONID }}  
      - name: Deploy to Azure Web App  
        uses: azure/webapps-deploy@v3  
        with:  
          app-name: 'HouseRental'  
          slot-name: 'Production'  
          package: 
```


## Brukerhistorier
- **Bruker**: Registrere konto, logge inn, søke og administrere leieobjekter.
- **Administrator**: Håndtere brukere, oppdatere informasjon om boliger, og fjerne oppføringer.

## Fremtidige forbedringer
- Forbedre UI-design for en mer moderne og intuitiv brukeropplevelse ved hjelp av **JavaScript** og **CSS**.
- Legge til e-postbekreftelse ved registrering.
- Implementere API for mobilapplikasjon integrasjon.


## Om meg  
Dette prosjektet demonstrerer mine ferdigheter innen fullstack webutvikling ved bruk av moderne teknologier og rammeverk. Jeg har implementert et komplett system for administrasjon av utleieboliger, der både brukerautentisering og rollebasert tilgangskontroll er integrert. Prosjektet viser følgende kompetanser:  

- **Back-end utvikling med ASP.NET Core**: Jeg har utviklet en robust og skalerbar serverapplikasjon, inkludert CRUD-operasjoner, ved bruk av Entity Framework for databasehåndtering.  
- **Databaseadministrasjon**: Jeg har designet og administrert SQLite-databasen, inkludert migrasjoner og seed-data for testing. Prosjektet inkluderer også integrasjon med Azure SQL Database for skybasert datalagring.  
- **Sikkerhet og autentisering**: Implementert sikkerhetsfunksjoner som passordhashing og autentisering ved hjelp av Microsoft Identity, med støtte for rollebasert tilgang.  
- **CI/CD og DevOps-praksis**: Jeg har satt opp en komplett CI/CD-pipeline ved hjelp av GitHub Actions, som automatiserer bygging, testing, og deployering til Azure Web App.  
- **Front-end utvikling**: Ved hjelp av Razor Pages, Bootstrap og JavaScript har jeg utviklet et responsivt og brukervennlig grensesnitt som gir en god brukeropplevelse.  
- **Azure-tjenester**: Erfaring med Azure App Service for hosting, Kudu for feilsøking og deployering, samt integrasjon av skybaserte tjenester.  

Dette prosjektet reflekterer min evne til å levere høy kvalitet på kode, følge beste praksis, og arbeide med fullstack-løsninger som inkluderer både front-end og back-end utvikling, samt skybasert deployering.