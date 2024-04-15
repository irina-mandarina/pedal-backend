using Microsoft.AspNetCore.Mvc;
using Pedal.Services;
using Pedal.Entities;
using Pedal.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;

namespace Pedal.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly CarService carService;
        private readonly JwtService jwtService;

        public CarController(CarService carsService, JwtService JwtService)
        {
            carService = carsService;
            //rename JwtService
            jwtService = JwtService;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("/api/signup")]
        public async Task<IActionResult> Signup([FromBody] CarRequest car)
        {

            await carService.SignUpAsync(car);

            var carResponse = new CarResponse
            {
                Email = car.Email,
                Brand = car.Brand,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Engine = car.Engine,
                Transmission = car.Transmission,
                Mileage = car.Mileage,
                Horsepower = car.Horsepower,
                Passions = car.Passions,
                CarCultures = car.CarCultures,
                PictureURLs = car.PictureURLs
            };

            return Ok(carResponse);
        }

        [AllowAnonymous]
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] CarLoginRequest loginRequest)
        {
            try
            {
                var car = await carService.LogInAsync(loginRequest.Email, loginRequest.Password);

                var carResponse = new CarResponse
                {
                    Email = car.Email,
                    Brand = car.Brand,
                    Model = car.Model,
                    YearOfProduction = car.YearOfProduction,
                    Engine = car.Engine,
                    Transmission = car.Transmission,
                    Mileage = car.Mileage,
                    Horsepower = car.Horsepower,
                    Passions = car.Passions,
                    CarCultures = car.CarCultures,
                    PictureURLs = car.PictureURLs
                };

                var tokenSignature = jwtService.IssueNew(car);

                return Ok(new { Car = carResponse, Token = tokenSignature });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while logging in.");
            }
        }

        //[HttpPost("car/isvalid")]

        //public async Task<IActionResult> IsValid(string token)
        //{
        //    var isValid = await jwtService.IsValidAsync(token);
        //    var isExpired = jwtService.IsExpired(token);

        //    if (isValid)
        //    {
        //        if (isExpired)
        //        {
        //            return Ok("Token is valid but expired.");
        //        }
        //        else
        //        {
        //            return Ok("Token is valid.");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Token is not valid.");
        //    }
        //}



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
