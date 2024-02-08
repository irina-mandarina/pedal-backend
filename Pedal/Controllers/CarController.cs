using Microsoft.AspNetCore.Mvc;
using Pedal.Services;
using Pedal.Entities;
using Pedal.Models;

namespace Pedal.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly CarService carService;

        public CarController(CarService carsService) =>
            carService = carsService;

        [HttpGet]
        public async Task<Car[]> Get() =>
            await carService.GetCarsAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarRequest car)
        {

            await carService.SignUpAsync(car);

            return Ok(car);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromBody] Car updatedCar)
        {
            var newCar = await carService.UpdateCarInfoAsync(updatedCar);

            return Ok(updatedCar);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {

            await carService.DeleteCarAsync(id);
            return Ok();
        }
    }
}
