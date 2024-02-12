using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedal.Entities;
using Pedal.Entities.Enums;
using Pedal.Models;
using Pedal.Services;
using System.IO.Pipes;

namespace Pedal.Controllers
{
    [Route("api/swipes")]
    [ApiController]
    public class SwipeController : ControllerBase
    {
        private readonly SwipeService swipeService;

        public SwipeController(SwipeService swipesService) =>
            swipeService = swipesService;

        [HttpGet("cars/{id}/swipes/received/left")]
        public async Task<ActionResult<Swipe>> GetLeftSwipesForCar(string id)
        {
            //returns all the cars that didn't like a ceratin car
            return Ok(await swipeService.GetSwipesForIdAsync(id, SwipeDirection.LEFT));
        }

        [HttpGet("cars/{id}/swipes/received/right")]
        public async Task<ActionResult<Swipe>> GetRightSwipesForCar(string id)
        {
            //returns all the cars that liked a ceratin car
            return Ok(await swipeService.GetSwipesForIdAsync(id, SwipeDirection.RIGHT));
        }

        [HttpGet("cars/{id}/swipes/sent/left")]
        public async Task<ActionResult<Swipe>> GetLeftSwipesByCar(string id)
        {
            //returns all the cars that weren't liked by a ceratin car
            return Ok(await swipeService.GetCarsSwipedOnByAsync(id, SwipeDirection.LEFT));
        }

        [HttpGet("cars/{id}/swipes/sent/right")]
        public async Task<ActionResult<Swipe>> GetRightSwipesByCar(string id)
        {
            //returns all the cars that were liked by a ceratin car
            return Ok(await swipeService.GetCarsSwipedOnByAsync(id, SwipeDirection.RIGHT));
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SwipeRequest newSwipe)
        {
            await swipeService.AddSwipeAsync(newSwipe);
            return Ok(newSwipe);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromBody] Swipe updatedSwipe)
        {
            var newSwipe = await swipeService.UpdateSwipeAsync(updatedSwipe);

            return Ok(updatedSwipe);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await swipeService.DeleteSwipeAsync(id);
            return Ok();
        }
    }
}
