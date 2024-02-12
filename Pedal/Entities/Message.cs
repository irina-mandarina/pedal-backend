using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pedal.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public required string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
    }
}
