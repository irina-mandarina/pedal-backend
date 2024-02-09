using Pedal.Entities.Enums;

namespace Pedal.Models
{
    public class SwipeRequest
    {
        public SwipeDirection SwipeDirection { get; set; }
        public DateTime SwipeTime { get; set; }
        public string SwipedId { get; set; }
        public string SwiperId { get; set; }

        public string SwipeDirectionString { get => SwipeDirection.ToString().ToLower(); }
    }
}
