using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pedal.Entities.Enums;
using Pedal.Models;

namespace Pedal.Entities
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

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
        public required List<string> PictureURLs { get; set;}


        //public Car()
        //{

        //}
        //public Car(CarRequest carRequest)
        //{
        //    this.Id = "";
        //    this.Email = carRequest.Email;
        //    this.Password = carRequest.Password;
        //    this.Brand = carRequest.Brand;
        //    this.Model = carRequest.Model;
        //    this.YearOfProduction = carRequest.YearOfProduction;
        //    this.Engine = carRequest.Engine;
        //    this.Transmission = carRequest.Transmission;
        //    this.Mileage = carRequest.Mileage;
        //    this.Horsepower = carRequest.Horsepower;
        //    this.Passions = carRequest.Passions;
        //    this.CarCultures = carRequest.CarCultures;
        //    this.PictureURLs = carRequest.PictureURLs;
        //}




    }
}
