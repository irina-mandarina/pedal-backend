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
        //make async when neck hurts less
        public async Task<List<Car>> Get() =>
            _carsService.GetCars();

        [HttpGet("{id:length(24)}")]
        //make async
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
            int yearOfProd, EngineType engineType, TransmissionType transmissionType,
            int mileage, int horsepower, List<Passions> passions, List<CarCulture> carCultures, List<string> pictureURLs)
        {

            Car newCar = await _carsService.SignUp(email, password, brand, model, yearOfProd, engineType, transmissionType, mileage, horsepower, passions, carCultures, pictureURLs);

            return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            Car newCar = await _carsService.UpdateCarInfo(id, updatedCar);

            return Ok(updatedCar);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {

            await _carsService.DeleteCar(id);
            return Ok();
        }
    }
}
