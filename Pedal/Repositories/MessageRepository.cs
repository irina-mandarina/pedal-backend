using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pedal.Entities;
using Pedal.Models;

namespace Pedal.Repositories
{
    public class MessageRepository
    {
        private readonly IMongoCollection<Message> _messagesCollection;

        public MessageRepository(
            IOptions<StoreDatabaseSettings> messageStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                messageStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                messageStoreDatabaseSettings.Value.DatabaseName);

            _messagesCollection = mongoDatabase.GetCollection<Message>(
                messageStoreDatabaseSettings.Value.MessagesCollectionName);
        }

        public async Task<List<Message>> GetAsync() =>
            await _messagesCollection.Find(_ => true).ToListAsync();

        public async Task<Message?> GetAsync(string id) =>
            await _messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Message newMessage) =>
            await _messagesCollection.InsertOneAsync(newMessage);

        public async Task UpdateAsync(string id, Message updatedMessage) =>
            await _messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedMessage);

        public async Task RemoveAsync(string id) =>
            await _messagesCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<Message> CreateMessageAsync(Message message)
        {
            await this.CreateAsync(message);
            return message;
        }

        public async Task<Message> UpdateMessageAsync(Message updatedMessage)
        {
            await _messagesCollection.ReplaceOneAsync(x => x.Id == updatedMessage.Id, updatedMessage);
            return updatedMessage;
        }

        public async Task<Message?> GetBySenderAndReceiverAsync(string senderId, string receiverId)
        {
            return await _messagesCollection.Find(x => x.SenderID == senderId && x.ReceiverID == receiverId).FirstOrDefaultAsync();
        }

        public async Task<Message[]> GetMessagesBetweenUsersAsync(string senderId, string receiverId)
        {
            var messagesSent = await _messagesCollection.Find(x => x.SenderID == senderId && x.ReceiverID == receiverId).ToListAsync();
            var messagesReceived = await _messagesCollection.Find(x => x.SenderID == receiverId && x.ReceiverID == senderId).ToListAsync();
            var messages = messagesSent.Concat(messagesReceived).ToArray();
            return messages;

        }
    }
}
