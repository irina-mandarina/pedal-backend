using Microsoft.AspNetCore.Http.HttpResults;
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

        public List<Car> GetCars()
        {
            var cars = carRepository.GetAsync().Result;

            if (cars == null || !cars.Any())
            {
                throw new InvalidOperationException("No cars found");
            }

            return cars;
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

        public async Task<Car> SignUp(string email, string password, string brand, string model,
            int yearOfProd, EngineType engineType, TransmissionType transmissionType,
            int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs)
        {
            Car car = new()
            {
                Email = email,
                Password = password,
                Brand = brand,
                Model = model,
                YearOfProduction = yearOfProd,
                Engine = engineType,
                Transmission = transmissionType,
                Mileage = mileage,
                Horsepower = horsepower,
                Passions = passions,
                CarCultures = carCultures,
                PictureURLs = pictureURLs
            };

            return await carRepository.CreateCarAsync(car);
        }


        public Car LogIn(string email, string encryptedPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<Car> UpdateCarInfo(string id, Car updatedCar)
        {
            _ = GetCarById(id);
            await carRepository.UpdateAsync(id, updatedCar);
            return updatedCar;
        }

        public async Task<Car> DeleteCar(string id)
        {
            Car car = GetCarById(id);
            await carRepository.RemoveAsync(id);
            //what should i return my beloved
            return car;

        }
    }
}
