using Microsoft.AspNetCore.SignalR;
using Pedal.Models;
using Pedal.Services;
using System.Text.RegularExpressions;

namespace Pedal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageService messageService;

        public ChatHub(MessageService messageService)
        {
            this.messageService = messageService;
        }
        public async Task NewMessage(MessageRequest message)
        {
            await messageService.AddMessageAsync(message);


            await Clients.All.SendAsync("ReceiveNewMessage",
                message);
        }
    }
}
