using System.ComponentModel.DataAnnotations;
using System;



namespace HouseRental.Models
{
    public class LeaseAgreement : IValidatableObject
    {
        public int LeaseAgreementId { get; set; }       // Unik ID for hver leieavtale


        [Required]
        public DateTime StartDate { get; set; }         // Startdato for leieavtalen (påkrevd)

        [Required]
        public DateTime EndDate { get; set; }          // Sluttdato for leieavtalen (påkreved)

        public int HouseId { get; set; }                // ID for huset som leies ut

        [Required]
        public int RenterId { get; set; }               // ID for leietakeren


        public virtual House? House { get; set; } // Fremmednøkkel for forhold til Hus
        public virtual Renter? Renter { get; set; } // Fremmednøkkel for forhold til Leietaker

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)  
        {
            if (StartDate >= EndDate)
            {
                yield return new ValidationResult("The Start Date must be before the End Date.", new[] { "StartDate", "EndDate" });
            }
        }

    }
}
