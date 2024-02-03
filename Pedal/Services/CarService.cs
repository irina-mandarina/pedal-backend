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

        public Car GetCarById(string id)
        {
            var car = carRepository.GetAsync(id).Result;
            if (car == null)
            {
                throw new ArgumentNullException(id);
            }
            return car;
        }

        public Car SignUp(string email, string password, string brand, string model, 
            int yearOdProd, EngineType engineType, TransmissionType transmissionType,
            int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs) 
        {
            Car car = new()
            {
                Email = email,
                Password = password,
                Brand = brand,
                Model = model,
                YearOfProduction = yearOdProd,
                Engine = engineType,
                Transmission = transmissionType,
                Mileage = mileage,
                Horsepower = horsepower,
                Passions = passions,
                CarCultures = carCultures,
                PictureURLs = pictureURLs
            };

            return carRepository.CreateCarAsync(car).Result;
        }

        public Car LogIn(string email, string encryptedPassword)
        {
            throw new NotImplementedException();
        }

        public Car UpdateCarInfo(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
