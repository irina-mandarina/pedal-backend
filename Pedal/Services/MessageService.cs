using Microsoft.AspNetCore.Cors.Infrastructure;
using Pedal.Entities;
using Pedal.Models;
using Pedal.Repositories;

namespace Pedal.Services
{
    public class MessageService
    {
        public MessageRepository messageRepository;

        public MessageService(MessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<Message> AddMessageAsync(MessageRequest messageRequest)
        {
            if (string.IsNullOrEmpty(messageRequest.SenderID) ||
                string.IsNullOrEmpty(messageRequest.ReceiverID) ||
                string.IsNullOrEmpty(messageRequest.Text))
            {
                throw new ArgumentException("Sender ID, receiver ID, or text cannot be empty.");
            }
            return await messageRepository.CreateMessageAsync(new Message()
            {
                Id = "",
                Text = messageRequest.Text,
                SenderID = messageRequest.SenderID,
                ReceiverID = messageRequest.ReceiverID,
                Timestamp = messageRequest.Timestamp
            });
        }

        public async Task<Message[]> GetMessagesBetweenUsersAsync(string senderId, string receiverId)
        {
            return(await messageRepository.GetMessagesBetweenUsersAsync(senderId, receiverId)).ToArray();
        }

        public async Task<bool> MessageExists(string senderId, string receiverId)
        {
            return(await messageRepository.GetBySenderAndReceiverAsync(senderId, receiverId)) != null;
        }

        public async Task<Message[]> GetMessagesByUserIdAsync(string senderId)
        {
            return(await messageRepository.GetMessagesByUser(senderId)).ToArray();
        }
    }
}
