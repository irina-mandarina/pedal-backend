using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedal.Entities;
using Pedal.Services;

namespace Pedal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageService messageService;

        public MessageController(MessageService messagesService) =>
            messageService = messagesService;

        [HttpGet("cars/{senderId}/messages/{receiverId}")]
        public async Task<Message[]> GetMessagesBetweenUsers(string senderId, string receiverId)
        {
            return(await messageService.GetMessagesBetweenUsersAsync(senderId, receiverId));
        }

        [HttpGet("cars/{senderId}/messages")]
        public async Task<Message[]> GetMessagesByUser(string senderId)
        {
            return(await messageService.GetMessagesByUserIdAsync(senderId));
        }

        //not implemented
        [HttpGet]
        public async Task<List<Message>> Get()
        {
            return new List<Message>();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Message> Get(string id)
        {
            //var car = _messagesService.GetMessageById(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //return car;
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Post(Message newMessage)
        {
            //await _messagesService.CreateAsync(newMessage);

            //return CreatedAtAction(nameof(Get), new { id = newMessage.Id }, newMessage);
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Message updatedMessage)
        {
            //var car = await _messagesService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //updatedMessage.Id = car.Id;

            //await _messagesService.UpdateAsync(id, updatedMessage);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            //var car = await _messagesService.GetAsync(id);

            //if (car is null)
            //{
            //    return NotFound();
            //}

            //await _messagesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
