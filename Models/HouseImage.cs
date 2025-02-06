using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace HouseRental.Models
{
    public class HouseImage
    {
        [Key]
        public int ImageId { get; set; } // 📌 Unik ID for hvert bilde

        [Required]
        public string ImageUrl { get; set; } = string.Empty; // 📌 Filstien til bildet

        public DateTime UploadedAt { get; set; } = DateTime.Now; // 📌 Automatisk setter opplastingsdato

        // 📌 Fremmednøkkel – kobler bildet til et spesifikt hus
        public int HouseId { get; set; }
        [ForeignKey("HouseId")]
        public virtual House? House { get; set; } // 📌 Navigasjonsfelt for relasjon
    }
}
