using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Pedal.Entities.Enums;

namespace Pedal.Entities
{
    public class Swipe
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public SwipeDirection SwipeDirection { get; set; }
        public DateTime SwipeTime { get; set; }
        public string SwipedId { get; set; }
        public string SwiperId { get; set; }

        public string SwipeDirectionString { get => SwipeDirection.ToString().ToLower();}
    }
}
