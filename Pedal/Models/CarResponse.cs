using Pedal.Entities.Enums;
using Pedal.Entities;

public class CarResponse
{
    public string Email { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int YearOfProduction { get; set; }
    public EngineType Engine { get; set; }
    public TransmissionType Transmission { get; set; }
    public int Mileage { get; set; }
    public int Horsepower { get; set; }
    public List<Passions> Passions { get; set; }
    public List<CarCulture> CarCultures { get; set; }
    public List<string> PictureURLs { get; set; }

    public CarResponse()
    {
    }

    public CarResponse(Car car)
    {
        Email = car.Email;
        Brand = car.Brand;
        Model = car.Model;
        YearOfProduction = car.YearOfProduction;
        Engine = car.Engine;
        Transmission = car.Transmission;
        Mileage = car.Mileage;
        Horsepower = car.Horsepower;
        Passions = car.Passions;
        CarCultures = car.CarCultures;
        PictureURLs = car.PictureURLs;
    }
}
