using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedal.Services;
using Pedal.Entities;
using Pedal.Models;
using Pedal.Repositories;
using Pedal.Entities.Enums;

namespace Pedal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carsService;

        public CarController(CarService carsService) =>
            _carsService = carsService;

        [HttpGet]
        public async Task<List<Car>> Get() =>
            await _carsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = _carsService.GetCarById(id);

            if (car is null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string email, string password, string brand, string model,
            int yearOdProd, EngineType engineType, TransmissionType transmissionType,
            int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs)
        {
            _carsService.SignUp(newCar);

            return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            var car = await _carsService.GetAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            updatedCar.Id = car.Id;

            await _carsService.UpdateAsync(id, updatedCar);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var car = await _carsService.GetAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            await _carsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
