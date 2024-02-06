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
        public CarService _carsService;

        public CarController(CarService carsService) =>
            _carsService = carsService;

        //not implemented
        [HttpGet]
        public async Task<List<Car>> Get()
        {
            return new List<Car>();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Car> Get(string id)
        {
            //var car = _carsService.GetCarById(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //return car;
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Post(Car newCar)
        {
            //await _carsService.CreateAsync(newCar);

            //return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            //var car = await _carsService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //updatedCar.Id = car.Id;

            //await _carsService.UpdateAsync(id, updatedCar);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            //var car = await _carsService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //await _carsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
