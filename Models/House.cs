﻿using System.ComponentModel.DataAnnotations;
using System.Net;

namespace HouseRental.Models
{
    public class House
    {
        public int HouseId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address is too long")]
        [Display(Name = "House Address")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rooms is required")]
        [Range(1, 10, ErrorMessage = "Rooms must be between 1 and 10")]
        public int Rooms { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Availability is required")]
        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; } = true;

        public int OwnerId { get; set; }

        public virtual Owner? Owner { get; set; } // Fremmednøkkel for forhold til Eier
        public virtual List<LeaseAgreement>? LeaseAgreements { get; set; } // En-til-mange-forhold til Leieavtaler

        // 🔥 Ny en-til-mange relasjon: Et hus kan ha flere bilder
        public virtual List<HouseImage> Images { get; set; } = new List<HouseImage>();


        // 🔥 Nytt: Beskrivelse, fasiliteter, og nabolagsdetaljer
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; } = string.Empty;

        [Display(Name = "Fasiliteter")]
        public string Fasiliteter { get; set; } = string.Empty;

        [Display(Name = "Nabolags Info")]
        public string Nabolagsinfo { get; set; } = string.Empty;

    }
}
