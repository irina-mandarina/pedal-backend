using Microsoft.AspNetCore.SignalR;
using Pedal.Models;
using System.Text.RegularExpressions;

namespace Pedal.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(string SenderID, string ReceiverID, string Text, DateTime Timestamp)
        {
            await Clients.All.SendAsync("ReceiveNewMessage",
                SenderID, ReceiverID, Text, Timestamp);
        }
    }
}
