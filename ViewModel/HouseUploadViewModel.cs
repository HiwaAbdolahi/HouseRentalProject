using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HouseRental.Helpers;
using HouseRental.Models;
using Microsoft.AspNetCore.Http;


namespace HouseRental.ViewModel
{
    public class HouseUploadViewModel
    {
        // 🏠 Husinformasjon
        public int HouseId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address is too long")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Rooms are required")]
        [Range(1, 10, ErrorMessage = "Rooms must be between 1 and 10")]
        public int Rooms { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required(ErrorMessage = "Owner is required")]
        public int OwnerId { get; set; }


        // 📷 Bildeopplasting med validering
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public List<IFormFile>? Images { get; set; }  // Validering for opplastede filer

        // 🖼️ Eksisterende bilder som skal vises
        public List<HouseImage>? ExistingImages { get; set; }





        // 🔥 Legg til de nye feltene
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; } = string.Empty;

        [Display(Name = "Fasiliteter")]
        public string Fasiliteter { get; set; } = string.Empty;

        [Display(Name = "Nabolagsinfo")]
        public string Nabolagsinfo { get; set; } = string.Empty;
    }
}