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
        public async Task NewMessage(string SenderID, string ReceiverID, string Text, DateTime Timestamp)
        {
            var messageRequest = new MessageRequest
            {
                Text = Text,
                Timestamp = Timestamp,
                SenderID = SenderID,
                ReceiverID = ReceiverID
            };

            await messageService.AddMessageAsync(messageRequest);


            await Clients.All.SendAsync("ReceiveNewMessage",
                SenderID, ReceiverID, Text, Timestamp);
        }
    }
}
