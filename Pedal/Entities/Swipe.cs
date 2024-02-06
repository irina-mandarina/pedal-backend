using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pedal.Entities
{
    public class Swipe
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool SwipeDirection { get; set; }
        public DateTime SwipeTime { get; set; }
        public string SwipedId { get; set; }
        public string SwiperId { get; set; }

        public string SwipeDirectionString { get => SwipeDirection ? "Right" : "Left";
}
    }
}
