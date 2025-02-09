using Microsoft.EntityFrameworkCore;

namespace HouseRental.Models
{
    public static class DbInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            HouseDbContext context = serviceScope.ServiceProvider.GetRequiredService<HouseDbContext>();

            context.Database.EnsureCreated();

            // **1️⃣ Legg til Owners først**
            if (!context.Owners.Any())
            {
                var owners = new List<Owner>
                {
                    new Owner { Name = "Alice Hansen", Address = "Osloveien 1" },
                    new Owner { Name = "Bob Johansen", Address = "Oslogata 2" },
                };
                context.AddRange(owners);
                context.SaveChanges();
            }

            // **2️⃣ Legg til Houses uten ImageUrl**
            if (!context.Houses.Any())
            {
                var firstOwner = context.Owners.FirstOrDefault();
                if (firstOwner != null)
                {
                    var houses = new List<House>
                    {
                        new House { Address = "Drammen", Rooms = 4, Price = 150, IsAvailable = true, OwnerId = firstOwner.OwnerId,
                        Beskrivelse = "Moderne hus med romslige områder og flott utsikt.",
                        Fasiliteter = "Parkering, Hage, Peis",
                        Nabolagsinfo = "Rolig nabolag med kort vei til butikker og skoler" },

            new House { Address = "789 Oak Road", Rooms = 5, Price = 400000, IsAvailable = true, OwnerId = firstOwner.OwnerId,
                        Beskrivelse = "Romslig enebolig med stor hage.",
                        Fasiliteter = "Garasje, Terrasse, Kjeller",
                        Nabolagsinfo = "Familievennlig område med parker i nærheten" },

            new House { Address = "101 Pine Lane", Rooms = 3, Price = 220000, IsAvailable = true, OwnerId = firstOwner.OwnerId,
                        Beskrivelse = "Koselig hus perfekt for små familier.",
                        Fasiliteter = "Balkong, Moderne kjøkken",
                        Nabolagsinfo = "Tett på offentlig transport og shopping" },

            new House { Address = "202 Cedar Street", Rooms = 4, Price = 300000, IsAvailable = false, OwnerId = firstOwner.OwnerId,
                        Beskrivelse = "Nydelig renovert hus med åpen planløsning.",
                        Fasiliteter = "Peis, Privat hage, Aircondition",
                        Nabolagsinfo = "Nær skoler og restauranter" }
                    };
                    context.AddRange(houses);
                    context.SaveChanges();
                }
            }

            // **3️⃣ Legg til HouseImages for hvert hus**
            if (!context.HouseImages.Any())
            {
                var houses = context.Houses.ToList();
                var images = new List<HouseImage>();

                foreach (var house in houses)
                {
                    images.Add(new HouseImage { HouseId = house.HouseId, ImageUrl = $"/images/{house.HouseId}.jpg" });
                }

                context.AddRange(images);
                context.SaveChanges();
            }

            // **4️⃣ Legg til Renters etter Houses**
            if (!context.HouseImages.Any())
            {
                var houses = context.Houses.ToList();
                var images = new List<HouseImage>();

                foreach (var house in houses)
                {
                    images.Add(new HouseImage { HouseId = house.HouseId, ImageUrl = $"/images/{house.HouseId}.jpg" });
                }

                context.AddRange(images);
                context.SaveChanges();
            }


            // **5️⃣ Legg til LeaseAgreements til slutt**
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
