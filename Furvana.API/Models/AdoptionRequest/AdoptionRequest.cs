namespace Furvana.API.Models
{
    using System;

    public class AdoptionRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PetName { get; set; }
        public string ReasonForAdoption { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
