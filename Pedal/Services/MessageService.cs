using Pedal.Entities;
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

        public async Task<Message[]> GetMessagesBetweenUsersAsync(string senderId, string receiverId)
        {
            return(await messageRepository.GetMessagesBetweenUsersAsync(senderId, receiverId)).ToArray();
        }
    }
}
