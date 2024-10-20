using System.ComponentModel.DataAnnotations;

namespace HouseRental.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Owner's Name")]
        public string Name { get; set; } = string.Empty;

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ., \-]{2,100}", ErrorMessage = "The address must be alphanumeric characters and may include ., - and spaces, between 2 and 100 characters.")]
        [Display(Name = "Owner's Address")]
        public string Address { get; set; } = string.Empty;

        public virtual List<House>? Houses { get; set; } // En-til-mange-forhold til Hus owner kan ha flere huser
    }
}
