using Pedal.Entities;
using Pedal.Entities.Enums;
using Pedal.Repositories;

namespace Pedal.Services
{
    public class CarService
    {
        public CarRepository carRepository;

        public CarService(CarRepository carRepository)
        { 
            this.carRepository = carRepository;
        }

        public Car GetCarById(int id)
        {
            throw new NotImplementedException();
        }

        public Car SignUp(string email, string password, string brand, string model, int yearOdProd, EngineType engineType, TransmissionType transmissionType, int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs) { throw new NotImplementedException(); }

        public Car LogIn(string email, string encryptedPassword) { throw new NotImplementedException(); }
    }
}
