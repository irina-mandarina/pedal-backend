using Pedal.Entities.Enums;
using Pedal.Entities;

namespace Pedal.Models
{
    public class CarRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int YearOfProduction { get; set; }
        public EngineType Engine { get; set; }
        public TransmissionType Transmission { get; set; }
        public int Mileage { get; set; }
        public int Horsepower { get; set; }
        public List<Passions> Passions { get; set; } = new List<Passions>();
        public List<CarCulture> CarCultures { get; set; } = new List<CarCulture>();
        public required List<string> PictureURLs { get; set; }
    }
}
