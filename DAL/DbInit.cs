using Microsoft.EntityFrameworkCore;

namespace HouseRental.Models
{
    public static class DbInit
    {
        public static void Seed(IApplicationBuilder app)                                            //Først har vi metoden Seed som tar en IApplicationBuilder som parameter. Denne metoden blir kalt for å utføre DB-seeding når applikasjonen starter.
        {


            using var serviceScope = app.ApplicationServices.CreateScope();                         //Vi oppretter en omfang (using var serviceScope) for å få tilgang til tjenester og deretter får vi tak i ItemDbContext, som er databasekonteksten vår.
            HouseDbContext context = serviceScope.ServiceProvider.GetRequiredService<HouseDbContext>();
            //context.Database.EnsureDeleted();                                                              //Vi sletter databasen (context.Database.EnsureDeleted()) og oppretter den på nytt (context.Database.EnsureCreated()) for å sikre at den er tom og klar for seeding.  
            context.Database.EnsureCreated();

            

            if (!context.Houses.Any())                                                               //Vi sjekker om det allerede finnes data i databasen. Hvis ikke, legger vi til data for Items, Customers, Orders, og OrderItems. Dette kan omfatte produkter, kunder, bestillinger og elementer i bestillinger.
            {
                var houses = new List<House>
            {
                new House
                {
                    
                    Address = "Drammen",
                    Rooms = 4,
                    Price = 150,
                    IsAvailable = "true",
                    ImageUrl = "/images/1.jpg",
                    OwnerId = 1
                },
                new House
                {
                    
                    Address = "789 Oak Road",
                    Rooms = 5,
                    Price = 400000,
                    IsAvailable = "true",
                    ImageUrl = "/images/2.jpg",
                    OwnerId = 1
                    
                },
                new House
                {
                    
                    Address = "101 Pine Lane",
                    Rooms = 3,
                    Price = 220000,
                    IsAvailable = "true",
                    ImageUrl = "/images/3.jpg",
                    OwnerId = 1
                },
                new House
                {
                    
                    Address = "202 Cedar Street",
                    Rooms = 4,
                    Price = 300000,
                    IsAvailable = "false",
                    ImageUrl = "/images/4.jpg",
                    OwnerId = 1
                },
                new House
                {
                    
                    Address = "303 Maple Avenue",
                    Rooms = 3,
                    Price = 240000,
                    IsAvailable = "true",
                    ImageUrl = "/Images/5.jpg",
                    OwnerId = 1
                },
                new House
                {
                    
                    Address = "404 Birch Road",
                    Rooms = 4,
                    Price = 310000,
                    IsAvailable = "true",
                    ImageUrl = "/Images/6.jpg",
                    OwnerId = 1
                },
                new House
                {   
                    Address = "505 Spruce Lane",
                    Rooms = 4,
                    Price = 280000,
                    IsAvailable = "true",
                    ImageUrl = "/Images/7.jpg",
                    OwnerId = 1
                },
                new House
                {   
                    Address = "606 Fir Street",
                    Rooms = 3,
                    Price = 230000,
                    IsAvailable = "false",
                    ImageUrl = "/Images/8.jpg",
                    OwnerId = 1
                },
            };
                context.AddRange(houses);
                context.SaveChanges();                           //Etter å ha lagt til dataene, lagrer vi endringene i databasen ved å kalle context.SaveChanges().
            }
            
            if (!context.Owners.Any())
            {
                var owners = new List<Owner>
                {
                    new Owner { Name = "Alice Hansen", Address = "osloveien 1"},
                    new Owner { Name = "Bob Johansen", Address = "Oslemet gata 2" },
                };
                context.AddRange(owners);
                context.SaveChanges();
            }

            
            if (!context.LeaseAgreements.Any())
            {
                var leaseAgreements = new List<LeaseAgreement>
                {
                    new LeaseAgreement
                    {
                        StartDate = DateTime.Today,  // Legg til startdatoen for leieavtalen
                        EndDate = DateTime.Today.AddMonths(12),  // Legg til sluttdatoen (for eksempel 1 år senere)
                        HouseId = 1,  // Legg til riktig hus-ID
                        RenterId = 1,  // Legg til riktig leietaker-ID
                    },
                    new LeaseAgreement
                    {
                        StartDate = DateTime.Today,  // Legg til startdatoen for leieavtalen
                        EndDate = DateTime.Today.AddMonths(6),  // Legg til sluttdatoen (for eksempel 6 måneder senere)
                        HouseId = 2,  // Legg til riktig hus-ID
                        RenterId = 2,  // Legg til riktig leietaker-ID},
                    },
                };

                context.AddRange(leaseAgreements);
                context.SaveChanges();
            }

            if (!context.Renters.Any())
            {
                var renters = new List<Renter>
                {
                    new Renter
                    {
                        RenterId = 2,
                        Name = "John Doe",
                        Address = "123 Main Street",
                        HouseId = 2, // Legg til riktig hus-ID
                    },
                    new Renter
                    {
                        RenterId = 3,
                        Name = "Jane Smith",
                        Address = "456 Elm Avenue",
                        HouseId = 3, // Legg til riktig hus-ID
                    },

                };
                context.AddRange(renters);
                context.SaveChanges();


            }
            

        }
    }
}
