using Pedal.Entities;
using Pedal.Entities.Enums;
using Pedal.Repositories;
using BCrypt;
using Pedal.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Pedal.Services
{
    public class CarService
    {
        public CarRepository carRepository;

        public CarService(CarRepository carRepository)
        { 
            this.carRepository = carRepository;
        }

        public async Task<Car[]> GetCarsAsync()
        {
            return (await carRepository.GetAsync()).ToArray();
        }

        public async Task<Car?> GetCarByIdAsync(string id)
        {
            var car = await carRepository.GetAsync(id);
            if (car == null)
            {
                throw new ArgumentNullException(id);
            }
            return car;
        }

        public async Task<Car> SignUpAsync(string email, string password, string brand, string model, 
            int yearOdProd, EngineType engineType, TransmissionType transmissionType,
            int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs) 
        {
            if (!ValidationService.IsValidEmail(email))
            {
                throw new InvalidDataException("Invalid email.");
            }
            if (await CarWithEmailExistsAsync(email))
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

            return await carRepository.CreateCarAsync(car);
        }

        public async Task<Car> SignUpAsync(CarRequest carRequest)
        {
            if (!ValidationService.IsValidEmail(carRequest.Email))
            {
                throw new InvalidDataException("Invalid email.");
            }
            if (await CarWithEmailExistsAsync(carRequest.Email))
            {
                throw new InvalidOperationException($"Email: {carRequest.Email} already registered.");
            }
            if (!ValidationService.IsValidPassword(carRequest.Password))
            {
                throw new InvalidDataException("Passwords must contain at least 8 symbols.");
            }
            
            return await carRepository.CreateCarAsync(new Car()
                {
                Id = "",
                Email = carRequest.Email,
                Password = carRequest.Password,
                Brand = carRequest.Brand,
                Model = carRequest.Model,           
                YearOfProduction = carRequest.YearOfProduction,
                Engine = carRequest.Engine,
                Transmission = carRequest.Transmission,
                Mileage = carRequest.Mileage,
                Horsepower = carRequest.Horsepower,
                Passions = carRequest.Passions,
                CarCultures = carRequest.CarCultures,
                PictureURLs = carRequest.PictureURLs,
                } );
        }

        public async Task<Car> LogInAsync(string email, string password)
        {
            if (!await CarWithEmailExistsAsync(email))
            {
                throw new InvalidOperationException($"Car with email: {email} does not exist.");
            }
            var car = await carRepository.GetCarByEmailAsync(email);
            if (BCrypt.Net.BCrypt.Verify(password, car.Password))
            {
                return car;
            }
            else throw new InvalidDataException("Wrong password.");
        }

        public async Task<Car> UpdateCarInfoAsync(Car car)
        {
            if (!await CarWithIdExistsAsync(car.Id))
            {
                throw new InvalidOperationException($"Car with id: {car.Id} does not exist.");
            }
            return await carRepository.UpdateCarAsync(car);

        }

        public async Task DeleteCarAsync(Car car)
        {
            if (!(await CarWithIdExistsAsync(car.Id)))
            {
                throw new InvalidOperationException($"Car with id: {car.Id} does not exist.");
            }
            carRepository.RemoveAsync(car.Id).Wait();
        }

        public async Task DeleteCarAsync(string carId)
        {
            if (!(await CarWithIdExistsAsync(carId)))
            {
                throw new InvalidOperationException($"Car with id: {carId} does not exist.");
            }
            carRepository.RemoveAsync(carId).Wait();
        }

        public async Task<bool> CarWithIdExistsAsync(string carId)
        {
            var car = await carRepository.GetAsync(carId);
            return car != null;
        }

        private async Task<bool> CarWithEmailExistsAsync(string email)
        {
            var car = await carRepository.GetCarByEmailAsync(email);
            return car != null;
        }
    }
}
