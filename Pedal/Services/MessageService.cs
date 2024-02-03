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
    }
}
