using Pedal.Entities;
using Pedal.Entities.Enums;
using Pedal.Repositories;
using BCrypt;

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
            if (!ValidationService.IsValidEmail(email))
            {
                throw new InvalidDataException("Invalid email.");
            }
            if (CarWithEmailExists(email))
            {
                throw new InvalidOperationException($"Email: {email} already registered.");
            }
            if (!ValidationService.IsValidPassword(password))
            {
                throw new InvalidDataException("Passwords must contain at least 8 symbols.");
            }
            Car car = new()
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
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

        public Car LogIn(string email, string password)
        {
            if (!CarWithEmailExists(email))
            {
                throw new InvalidOperationException($"Car with email: {email} does not exist.");
            }
            var car = carRepository.GetCarByEmailAsync(email).Result;
            if (BCrypt.Net.BCrypt.Verify(password, car.Password))
            {
                return car;
            }
            else throw new InvalidDataException("Wrong password.");
        }

        public Car UpdateCarInfo(Car car)
        {
            if (!CarWithIdExists(car.Id))
            {
                throw new InvalidOperationException($"Car with id: {car.Id} does not exist.");
            }
            return carRepository.UpdateCarAsync(car).Result;

        }

        public void DeleteCar(Car car)
        {
            if (!CarWithIdExists(car.Id)) 
            {
                throw new InvalidOperationException($"Car with id: {car.Id} does not exist.");
            }
            carRepository.RemoveAsync(car.Id).Wait();
        }

        private bool CarWithIdExists(string carId)
        {
            var car = carRepository.GetAsync(carId).Result;
            return car != null;
        }

        private bool CarWithEmailExists(string email)
        {
            var car = carRepository.GetCarByEmailAsync(email).Result;
            return car != null;
        }
    }
}
