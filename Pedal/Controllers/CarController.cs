using Microsoft.AspNetCore.Mvc;
using Pedal.Services;
using Pedal.Entities;

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
        public async Task<IActionResult> Post(
            [FromBody] Car car)
        {

            var newCar = await _carsService.SignUpAsync(car);

            return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(Car updatedCar)
        {
            var newCar = await _carsService.UpdateCarInfoAsync( updatedCar);

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
