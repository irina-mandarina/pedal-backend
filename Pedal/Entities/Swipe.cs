namespace Pedal.Entities
{
    public class Swipe
    {
        public int Id { get; set; }
        public bool SwipeDirection { get; set; }
        public DateTime SwipeTime { get; set; }
        public int SwipedId { get; set; }
        public int SwiperId { get; set;}
    }
}
