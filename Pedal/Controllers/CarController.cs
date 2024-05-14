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
using Microsoft.AspNetCore.Http.HttpResults;

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

        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<CarResponse[]>> Get()
        {
            var result =  await carService.GetCarsAsync();
            var carResponses = result.Select(car => new CarResponse(car));
            return Ok(new { Cars = carResponses });
        }

        [HttpGet("{id:length(24)?}")]
        public async Task<ActionResult<CarResponse>> Get(string? id = null)
        {
            Car car;
            if(id is null)
            {
                var carId = HttpContext.Items["CarId"]!.ToString();
                car = await carService.GetCarByIdAsync(carId!);
            }
            else
            {
                car = await carService.GetCarByIdAsync(id);
            }
            if (car is null)
            {
                return NotFound();
            }

            var carResponse = new CarResponse(car);
            return Ok(new { Car = carResponse });
        }

        [AllowAnonymous]
        [HttpPost("/api/signup/check")]

        //generate a SignUp Check method to check if email is already in use and if password is valid using the CarLoginRequest model
        public async Task<IActionResult> SignUpCheck([FromBody] CarLoginRequest loginRequest)
        {
            try
            {
                if (!ValidationService.IsValidEmail(loginRequest.Email))
                {
                    throw new InvalidDataException("Invalid email.");
                }
                if (await carService.CarWithEmailExistsAsync(loginRequest.Email))
                {
                    throw new InvalidOperationException($"Email: {loginRequest.Email} already registered.");
                }
                if (!ValidationService.IsValidPassword(loginRequest.Password))
                {
                    throw new InvalidDataException("Passwords must contain at least 8 symbols.");
                }

                return Ok();
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("/api/signup")]
        public async Task<IActionResult> Signup([FromBody] CarRequest signUpRequest)
        {

            var car = await carService.SignUpAsync(signUpRequest);

            var carResponse = new CarResponse(car);

            return Ok(new { Car = carResponse });
        }

        [AllowAnonymous]
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] CarLoginRequest loginRequest)
        {
            try
            {
                var car = await carService.LogInAsync(loginRequest.Email, loginRequest.Password);

                var carResponse = new CarResponse(car);

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

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromBody] Car updatedCar)
        {
            var newCar = await carService.UpdateCarInfoAsync(updatedCar);

            return Ok(new { Car = updatedCar });
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {

            await carService.DeleteCarAsync(id);
            return Ok(new { CarId = id });
        }
    }
}
