namespace Furvana.API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string PetName { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string OwnPets { get; set; }
        public string HomeStatus { get; set; }
        public string HaveYard { get; set; }
        public string LandlordPolicy { get; set; }

        public string Reference1Name { get; set; }
        public string Reference1Email { get; set; }
        public string Reference1Phone { get; set; }

        public string Children { get; set; }
        public string HoursAlone { get; set; }
        public string TravelPlans { get; set; }
        public string BehaviorConcerns { get; set; }
        public string SurrenderedBefore { get; set; }
        public string CrimeHistory { get; set; }

        public bool AgreeProcess { get; set; }
        public bool AgreeReferences { get; set; }
        public bool AgreeDonation { get; set; }
        public bool AgreeSurrender { get; set; }
        public bool InfoTrue { get; set; }
    }

}
