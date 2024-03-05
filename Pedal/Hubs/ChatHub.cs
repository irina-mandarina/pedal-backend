using Microsoft.AspNetCore.SignalR;
using Pedal.Models;
<<<<<<< Updated upstream
using System.Text.RegularExpressions;
=======
>>>>>>> Stashed changes

namespace Pedal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;

        public ChatHub()
        {
            _botUser = "MyChat Bot";
        }
<<<<<<< Updated upstream
        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser,
                $"{userConnection.Name} has joined {userConnection.Room}");

=======
        public async Task NewMessage(string SenderID, string ReceiverID, string Text, DateTime Timestamp)
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            //await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser,
            //$"{userConnection.User} has joined {userConnection.Room}");
            await Clients.All.SendAsync("ReceiveNewMessage",
                SenderID, ReceiverID, Text, Timestamp);
>>>>>>> Stashed changes
        }
    }
}
