using HouseRental.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseRental.Models
{
    public class Renter
    {
        public int RenterId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The name must be numbers, letters, and may include ., - and spaces, between 2 and 20 characters.")]
        [Display(Name = "Renter's Name")]
        public string Name { get; set; } = string.Empty;

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ., \-]{2,100}", ErrorMessage = "The address must be alphanumeric characters and may include ., - and spaces, between 2 and 100 characters.")]
        [Display(Name = "Renter's Address")]
        public string Address { get; set; } = string.Empty;

        public int HouseId { get; set; }

        // 🔥 Oppdatert House-relasjon til å inkludere bilder
        public virtual House? House { get; set; }

        public virtual List<LeaseAgreement>? LeaseAgreements { get; set; }
    }
}



