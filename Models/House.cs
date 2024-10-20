using System.ComponentModel.DataAnnotations;
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
        public string IsAvailable { get; set; } = string.Empty;


        public int OwnerId { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public virtual Owner? Owner { get; set; } // Fremmednøkkel for forhold til Eier
        public virtual List<LeaseAgreement>? LeaseAgreements { get; set; } // En-til-mange-forhold til Leieavtaler
    }
}


