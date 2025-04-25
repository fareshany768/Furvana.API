namespace Furvana.API.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }  // Dog, Cat, Bird...
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Size { get; set; }
        public string HealthStatus { get; set; }
        public string GoodWith { get; set; }
        public string ImageUrl { get; set; }
    }
}

