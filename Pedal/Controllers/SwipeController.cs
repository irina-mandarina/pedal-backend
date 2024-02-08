using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedal.Entities;
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

        //[HttpGet("{id:length(24)}")]
        //public async Task<ActionResult<Swipe>> GetAsync(string id)
        //{

        //    return null;

        //}

        [HttpGet("cars/{id}/swipes/received/left")]

        public async Task<ActionResult<Swipe>> GetLeftSwipesForCar(string id)
        {
            //add swipe direction
            //await swipeService.GetSwipesForIdAsync(id, swipeDirection = false);
            return Ok();
        }

        [HttpGet("cars/{id}/swipes/received/right")]
        public async Task<ActionResult<Swipe>> GetRightSwipesForCar(string id)
        {
            //add swipe direction
            //await swipeService.GetSwipesForIdAsync(id, swipeDirection = true);
            return Ok();
        }

        [HttpGet("cars/{id}/swipes/sent/left")]
        public async Task<ActionResult<Swipe>> GetLeftSwipesByCar(string id)
        {
            //add swipe direction
            //await swipeService.GetSwipesByIdAsync(id, swipeDirection = false);
            return Ok();
        }

        [HttpGet("cars/{id}/swipes/sent/right")]
        public async Task<ActionResult<Swipe>> GetRightSwipesByCar(string id)
        {
            //add swipe direction
            //await swipeService.GetSwipesByIdAsync(id, swipeDirection = true);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> Post(Swipe newSwipe)
        {
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Swipe updatedSwipe)
        {
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            return NoContent();
        }
    }
}
