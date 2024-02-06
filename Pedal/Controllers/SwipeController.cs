using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedal.Entities;
using Pedal.Services;

namespace Pedal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwipeController : ControllerBase
    {
        public SwipeService _swipesService;

        public SwipeController(SwipeService swipesService) =>
            _swipesService = swipesService;

        //not implemented
        [HttpGet]
        public async Task<List<Swipe>> Get()
        {
            return new List<Swipe>();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Swipe> Get(string id)
        {
            //var car = _swipesService.GetSwipeById(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //return car;
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Post(Swipe newSwipe)
        {
            //await _swipesService.CreateAsync(newSwipe);

            //return CreatedAtAction(nameof(Get), new { id = newSwipe.Id }, newSwipe);
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Swipe updatedSwipe)
        {
            //var car = await _swipesService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //updatedSwipe.Id = car.Id;

            //await _swipesService.UpdateAsync(id, updatedSwipe);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            //var car = await _swipesService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //await _swipesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
