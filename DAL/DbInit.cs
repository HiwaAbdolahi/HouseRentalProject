using Microsoft.EntityFrameworkCore;

namespace HouseRental.Models
{
    public static class DbInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            HouseDbContext context = serviceScope.ServiceProvider.GetRequiredService<HouseDbContext>();

            // Sørg for at databasen er opprettet
            context.Database.EnsureCreated();

            // **1️⃣ Legg til Owners først**
            if (!context.Owners.Any())
            {
                var owners = new List<Owner>
                {
                    new Owner { Name = "Alice Hansen", Address = "Osloveien 1"},
                    new Owner { Name = "Bob Johansen", Address = "Oslogata 2" },
                };
                context.AddRange(owners);
                context.SaveChanges(); // 🔹 Lagre eiere først
            }

            // **2️⃣ Legg til Houses etter at Owners eksisterer**
            if (!context.Houses.Any())
            {
                var firstOwner = context.Owners.FirstOrDefault();
                if (firstOwner != null)  // Sjekk at vi har en eier før vi legger til hus
                {
                    var houses = new List<House>
                    {
                        new House { Address = "Drammen", Rooms = 4, Price = 150, IsAvailable = true, ImageUrl = "/images/1.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "789 Oak Road", Rooms = 5, Price = 400000, IsAvailable = true, ImageUrl = "/images/2.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "101 Pine Lane", Rooms = 3, Price = 220000, IsAvailable = true, ImageUrl = "/images/3.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "202 Cedar Street", Rooms = 4, Price = 300000, IsAvailable = false, ImageUrl = "/images/4.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "303 Maple Avenue", Rooms = 3, Price = 240000, IsAvailable = true, ImageUrl = "/Images/5.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "404 Birch Road", Rooms = 4, Price = 310000, IsAvailable = true, ImageUrl = "/Images/6.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "505 Spruce Lane", Rooms = 4, Price = 280000, IsAvailable = true, ImageUrl = "/Images/7.jpg", OwnerId = firstOwner.OwnerId },
                        new House { Address = "606 Fir Street", Rooms = 3, Price = 230000, IsAvailable = false, ImageUrl = "/Images/8.jpg", OwnerId = firstOwner.OwnerId }
                    };
                    context.AddRange(houses);
                    context.SaveChanges(); // 🔹 Lagre husene etter at eiere er lagt til
                }
            }

            // **3️⃣ Legg til Renters etter Houses**
            if (!context.Renters.Any())
            {
                var housesList = context.Houses.ToList(); // Henter listen over hus

                var renters = new List<Renter>
                {
                    new Renter { Name = "John Doe", Address = "123 Main Street", HouseId = housesList.ElementAtOrDefault(1)?.HouseId ?? 1 },
                    new Renter { Name = "Jane Smith", Address = "456 Elm Avenue", HouseId = housesList.ElementAtOrDefault(2)?.HouseId ?? 1 },
                };
                context.AddRange(renters);
                context.SaveChanges();
            }

            // **4️⃣ Legg til LeaseAgreements til slutt**
            if (!context.LeaseAgreements.Any())
            {
                var house1 = context.Houses.FirstOrDefault(h => h.Address == "Drammen");
                var house2 = context.Houses.FirstOrDefault(h => h.Address == "789 Oak Road");
                var renter1 = context.Renters.FirstOrDefault(r => r.Name == "John Doe");
                var renter2 = context.Renters.FirstOrDefault(r => r.Name == "Jane Smith");

                if (house1 != null && house2 != null && renter1 != null && renter2 != null)
                {
                    var leaseAgreements = new List<LeaseAgreement>
                    {
                        new LeaseAgreement { StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(12), HouseId = house1.HouseId, RenterId = renter1.RenterId },
                        new LeaseAgreement { StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(6), HouseId = house2.HouseId, RenterId = renter2.RenterId },
                    };
                    context.AddRange(leaseAgreements);
                    context.SaveChanges();
                }
            }
        }
    }
}
