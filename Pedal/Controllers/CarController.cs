using Microsoft.AspNetCore.Mvc;
using Pedal.Services;
using Pedal.Entities;
using Pedal.Models;

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
        public async Task<Car[]> Get() =>
            await _carsService.GetCarsAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await _carsService.GetCarByIdAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarRequest car)
        {

            await _carsService.SignUpAsync(car);

            return Ok(car);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromBody] Car updatedCar)
        {
            var newCar = await _carsService.UpdateCarInfoAsync(updatedCar);

            return Ok(updatedCar);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {

            await _carsService.DeleteCarAsync(id);
            return Ok();
        }
    }
}
