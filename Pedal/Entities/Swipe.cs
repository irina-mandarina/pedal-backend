namespace Pedal.Entities
{
    public class Swipe
    {
        public string Id { get; set; }
        public bool SwipeDirection { get; set; }
        public DateTime SwipeTime { get; set; }
        public int SwipedId { get; set; }

        public int SwiperId { get; set; }

        public string SwipeDirectionString { get => SwipeDirection ? "Right" : "Left";
}
    }
}
